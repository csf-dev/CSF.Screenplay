using System;
using System.Collections.Generic;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Reporting.Models;

namespace CSF.Screenplay.Reporting.Builders
{
  /// <summary>
  /// Builder type for the reporting on a single Screenplay Scenario.
  /// </summary>
  public class ScenarioBuilder
  {
    readonly Scenario scenario;
    readonly Stack<PerformanceBuilder> builderStack;
    PerformanceType currentPerformanceType;
    bool finalised;

    /// <summary>
    /// Finalise the current scenario, recording whether or not it was a success.
    /// </summary>
    /// <param name="success">If set to <c>true</c> success.</param>
    public void Finalise(bool success)
    {
      EnsureNotFinalised();
      finalised = true;
      scenario.IsFailure = !success;
    }

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
      var builder = new PerformanceBuilder {
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
    public void BeginPerformanceType(PerformanceType performanceType)
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
    public void EndPerformanceType()
    {
      EnsureNotFinalised();
      currentPerformanceType = PerformanceType.Unspecified;
    }

    /// <summary>
    /// Reports that the given actor has gained the given ability.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="ability">Ability.</param>
    public void GainAbility(INamed actor, IAbility ability)
    {
      EnsureNotFinalised();
      var item = new GainAbility(actor, Outcome.Success, ability, currentPerformanceType);
      AddReportable(item);
    }

    void EnsureNotFinalised()
    {
      if(finalised)
        throw new InvalidOperationException("The scenario has already been finalised");
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
      var performance = builder.GetPerformance();
      AddReportable(performance);
    }

    void AddPerformanceBuilder(PerformanceBuilder builder)
    {
      if(builder == null)
        throw new ArgumentNullException(nameof(builder));

      builderStack.Push(builder);
    }

    PerformanceBuilder PopCurrentBuilder(Performables.IPerformable expectedPerformable)
    {
      PeekCurrentBuilder(expectedPerformable);
      return builderStack.Pop();
    }

    PerformanceBuilder PeekCurrentBuilder(Performables.IPerformable expectedPerformable = null)
    {
      if(builderStack.Count == 0 && expectedPerformable == null)
        return null;
      else if(builderStack.Count == 0)
        throw new InvalidOperationException("An expected performable was specified but the builder stack was empty, this is not permitted.");

      var current = builderStack.Peek();

      if(expectedPerformable == null)
        return current;

      if(!ReferenceEquals(expectedPerformable, current.Performable))
      {
        throw new ArgumentException("The expected performable must be the same as the current builder.",
                                    nameof(expectedPerformable));
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
    /// <param name="idName">Identifier name.</param>
    /// <param name="friendlyName">Friendly name.</param>
    /// <param name="featureName">Feature name.</param>
    /// <param name="featureId">Feature identifier.</param>
    public ScenarioBuilder(string idName,
                           string friendlyName,
                           string featureName,
                           string featureId)
    {
      builderStack = new Stack<PerformanceBuilder>();
      scenario = new Scenario(idName, friendlyName, featureName, featureId);
    }
  }
}
