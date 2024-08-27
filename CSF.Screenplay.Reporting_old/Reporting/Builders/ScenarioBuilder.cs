using System;
using System.Collections.Generic;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.ReportFormatting;
using CSF.Screenplay.ReportModel;

namespace CSF.Screenplay.Reporting.Builders
{
  /// <summary>
  /// Builder type for the reporting on a single Screenplay Scenario.
  /// </summary>
  public class ScenarioBuilder : IBuildsScenario
  {
    readonly Scenario scenario;
    readonly Stack<ReportableBuilder> builderStack;
    readonly IFormatsObjectForReport objectFormatter;
    ReportableCategory currentPerformanceType;
    bool finalised;

    /// <summary>
    /// Gets or sets the scenario identifier.
    /// </summary>
    /// <value>The scenario identifier.</value>
    public string ScenarioIdName
    {
      get { return scenario.Name.Id; }
      set { scenario.Name.Id = value; }
    }

    /// <summary>
    /// Gets or sets the scenario friendly name.
    /// </summary>
    /// <value>The scenario friendly name.</value>
    public string ScenarioFriendlyName
    {
      get { return scenario.Name.Name; }
      set { scenario.Name.Name = value; }
    }

    /// <summary>
    /// Gets a value which indicates whether or not the <see cref="P:CSF.Screenplay.Reporting.Builders.IBuildsScenario.ScenarioIdName" /> was auto-generated (and therefore
    /// meaningless).
    /// </summary>
    /// <value>
    /// <c>true</c> if the scenario identifier is generated; otherwise, <c>false</c>.</value>
    public bool ScenarioIdIsGenerated
    {
      get { return scenario.Name.IsIdGenerated; }
      set { scenario.Name.IsIdGenerated = value; }
    }

    /// <summary>
    /// Gets or sets the feature identifier.
    /// </summary>
    /// <value>The feature identifier.</value>
    public string FeatureIdName
    {
      get { return scenario.Feature.Name.Id; }
      set { scenario.Feature.Name.Id = value; }
    }

    /// <summary>
    /// Gets or sets the feature friendly name.
    /// </summary>
    /// <value>The feature friendly name.</value>
    public string FeatureFriendlyName
    {
      get { return scenario.Feature.Name.Name; }
      set { scenario.Feature.Name.Name = value; }
    }

    /// <summary>
    /// Gets a value which indicates whether or not the <see cref="P:CSF.Screenplay.Reporting.Builders.IBuildsScenario.FeatureIdName" /> was auto-generated (and therefore
    /// meaningless).
    /// </summary>
    /// <value>
    /// <c>true</c> if the feature identifier is generated; otherwise, <c>false</c>.</value>
    public bool FeatureIdIsGenerated
    {
      get { return scenario.Feature.Name.IsIdGenerated; }
      set { scenario.Feature.Name.IsIdGenerated = value; }
    }

    /// <summary>
    /// Finalise the current scenario, recording whether or not it was a success.
    /// </summary>
    /// <param name="outcome">If set to <c>true</c> success.</param>
    public void Finalise(bool? outcome)
    {
      EnsureNotFinalised();
      finalised = true;
      scenario.Outcome = GetOutcome(outcome);
    }

    ScenarioOutcome GetOutcome(bool? booleanOutcome)
    {
      if(!booleanOutcome.HasValue) return ScenarioOutcome.Inconclusive;
      if(booleanOutcome.Value) return ScenarioOutcome.Success;
      return ScenarioOutcome.Failure;
    }

    /// <summary>
    /// Gets a value which indicates if the scenario is finalised or not.
    /// </summary>
    /// <returns>
    /// <c>true</c>, if the scenario is finalised, <c>false</c> otherwise.</returns>
    public bool IsFinalised() => finalised;

    /// <summary>
    /// Gets the underlying scenario report model.
    /// </summary>
    /// <returns>The scenario.</returns>
    public Scenario GetScenario() => scenario;

    /// <summary>
    /// Begins reporting of a new performable.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="performable">Performable.</param>
    public void BeginPerformance(INamed actor, Performables.IPerformable performable)
    {
      EnsureNotFinalised();
      var builder = new ReportableBuilder(objectFormatter) {
        Performable = performable,
        Actor = actor,
        PerformanceType = currentPerformanceType,
      };
      AddPerformanceBuilder(builder);
    }

    /// <summary>
    /// Begins reporting of a performance of a given type.
    /// </summary>
    /// <param name="performanceType">Performance type.</param>
    public void BeginPerformanceCategory(ReportableCategory performanceType)
    {
      EnsureNotFinalised();
      performanceType.RequireDefinedValue(nameof(performanceType));
      currentPerformanceType = performanceType;
    }

    /// <summary>
    /// Records that the current performable has received a result.
    /// </summary>
    /// <param name="performable">Performable.</param>
    /// <param name="result">Result.</param>
    public void RecordResult(Performables.IPerformable performable, object result)
    {
      EnsureNotFinalised();
      var builder = PeekCurrentBuilder(performable);

      builder.HasResult = true;
      builder.Result = result;
    }

    /// <summary>
    /// Records that the current performable has failed with an exception.
    /// </summary>
    /// <param name="performable">Performable.</param>
    /// <param name="exception">Exception.</param>
    public void RecordFailure(Performables.IPerformable performable, Exception exception)
    {
      EnsureNotFinalised();
      var builder = PeekCurrentBuilder(performable);

      builder.IsFailure = true;
      builder.Exception = exception;

      FinalisePerformance(performable);
    }

    /// <summary>
    /// Records that the current performable has completed successfully.
    /// </summary>
    /// <param name="performable">Performable.</param>
    public void RecordSuccess(Performables.IPerformable performable)
    {
      EnsureNotFinalised();
      FinalisePerformance(performable);
    }

    /// <summary>
    /// Ends the performance of the current type.
    /// </summary>
    public void EndPerformanceCategory()
    {
      EnsureNotFinalised();
      currentPerformanceType = ReportableCategory.None;
    }

    /// <summary>
    /// Reports that the given actor has gained the given ability.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="ability">Ability.</param>
    public void GainAbility(INamed actor, IAbility ability)
    {
      EnsureNotFinalised();

      AddReportable(new Reportable {
        ActorName = actor.Name,
        Type = ReportableType.GainAbility,
        Report = ability.GetReport(actor),
        Category = currentPerformanceType,
      });
    }

    void EnsureNotFinalised()
    {
      if(finalised)
        throw new InvalidOperationException(Resources.ExceptionFormats.ScenarioAlreadyFinalised);
    }

    void AddReportable(Reportable item)
    {
      if(item == null)
        throw new ArgumentNullException(nameof(item));

      var list = GetCurrentReportables();
      list.Add(item);
    }

    void FinalisePerformance(Performables.IPerformable performable)
    {
      var builder = PopCurrentBuilder(performable);
      var performance = builder.GetReportable();
      AddReportable(performance);
    }

    void AddPerformanceBuilder(ReportableBuilder builder)
    {
      if(builder == null)
        throw new ArgumentNullException(nameof(builder));

      builderStack.Push(builder);
    }

    ReportableBuilder PopCurrentBuilder(Performables.IPerformable expectedPerformable)
    {
      PeekCurrentBuilder(expectedPerformable);
      return builderStack.Pop();
    }

    ReportableBuilder PeekCurrentBuilder(Performables.IPerformable expectedPerformable = null)
    {
      if(builderStack.Count == 0 && expectedPerformable == null)
        return null;
      else if(builderStack.Count == 0)
      {
        var message = String.Format(Resources.ExceptionFormats.PerformableWasRequiredInBuilderStack,
                                    nameof(Performables.IPerformable),
                                    nameof(ReportableBuilder));
        throw new InvalidOperationException(message);
      }
        

      var current = builderStack.Peek();

      if(expectedPerformable == null)
        return current;

      if(!ReferenceEquals(expectedPerformable, current.Performable))
      {
        var message = String.Format(Resources.ExceptionFormats.PerformableDoesNotMatchExpectedPerformance,
                                    nameof(Performables.IPerformable),
                                    nameof(ReportableBuilder));
        throw new ArgumentException(message, nameof(expectedPerformable));
      }

      return current;
    }

    IList<Reportable> GetCurrentReportables()
    {
      var currentBuilder = PeekCurrentBuilder();
      if(currentBuilder != null)
        return currentBuilder.Reportables;

      return scenario.Reportables;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Builders.ScenarioBuilder"/> class.
    /// </summary>
    public ScenarioBuilder(IFormatsObjectForReport objectFormatter)
    {
      if(objectFormatter == null)
        throw new ArgumentNullException(nameof(objectFormatter));
      
      builderStack = new Stack<ReportableBuilder>();
      scenario = new Scenario();
      this.objectFormatter = objectFormatter;
    }
  }
}
