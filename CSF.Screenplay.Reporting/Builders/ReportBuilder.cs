using System;
using System.Collections.Generic;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Reporting.Models;

namespace CSF.Screenplay.Reporting.Builders
{
  /// <summary>
  /// Builder type which creates instances of <see cref="Report"/>.
  /// </summary>
  public class ReportBuilder
  {
    List<Scenario> scenarios;
    Scenario currentScenario;
    PerformanceType currentPerformanceType;
    Stack<PerformanceBuilder> builderStack;

    /// <summary>
    /// Begins reporting upon a new scenario.
    /// </summary>
    /// <param name="name">Name.</param>
    public void BeginNewScenario(string name)
    {
      currentScenario = new Scenario(name);
      scenarios.Add(currentScenario);
      currentPerformanceType = PerformanceType.Unspecified;
      builderStack.Clear();
    }

    /// <summary>
    /// Reports the end of a scenario.
    /// </summary>
    /// <param name="isSuccess">If set to <c>false</c> then the scenario is marked as a failure.</param>
    public void EndScenario(bool isSuccess)
    {
      if(!isSuccess)
        currentScenario.IsFailure = true;
    }

    /// <summary>
    /// Begins reporting of a new performable.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="performable">Performable.</param>
    public void BeginPerformance(INamed actor, Performables.IPerformable performable)
    {
      var builder = new PerformanceBuilder {
        Performable = performable,
        Actor = actor,
        PerformanceType = currentPerformanceType,
      };
      AddPerformanceBuilder(builder);
    }

    /// <summary>
    /// Records that the current performable has received a result.
    /// </summary>
    /// <param name="performable">Performable.</param>
    /// <param name="result">Result.</param>
    public void RecordResult(Performables.IPerformable performable, object result)
    {
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
      FinalisePerformance(performable);
    }

    /// <summary>
    /// Begins reporting of a performance of a given type.
    /// </summary>
    /// <param name="performanceType">Performance type.</param>
    public void BeginPerformance(PerformanceType performanceType)
    {
      performanceType.RequireDefinedValue(nameof(performanceType));
      currentPerformanceType = performanceType;
    }

    /// <summary>
    /// Ends the performance of the current type.
    /// </summary>
    public void EndPerformance()
    {
      currentPerformanceType = PerformanceType.Unspecified;
    }

    /// <summary>
    /// Reports that the given actor has gained the given ability.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="ability">Ability.</param>
    public void GainAbility(INamed actor, IAbility ability)
    {
      var item = new GainAbility(actor, Outcome.Success, ability, currentPerformanceType);
      AddReportable(item);
    }

    /// <summary>
    /// Builds and returns the report from the current instance.
    /// </summary>
    /// <returns>The report.</returns>
    public Report GetReport()
    {
      return new Report(scenarios.ToArray());
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

      if(currentScenario != null)
        return currentScenario.Reportables;

      throw new InvalidOperationException("Cannot get the current reportables, there must be either a current builder or a current scenario.");
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportBuilder"/> class.
    /// </summary>
    public ReportBuilder()
    {
      scenarios = new List<Scenario>();
      builderStack = new Stack<PerformanceBuilder>();
      currentPerformanceType = PerformanceType.Unspecified;
    }
  }
}
