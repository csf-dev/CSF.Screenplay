using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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
    readonly IReportFactory reportFactory;
    readonly ConcurrentDictionary<Guid, ScenarioBuilder> scenarios;

    /// <summary>
    /// Begins reporting upon a new scenario.
    /// </summary>
    /// <param name="friendlyName">The friendly scenario name.</param>
    /// <param name="featureName">The feature name.</param>
    /// <param name="idName">The uniquely identifying name for the test.</param>
    /// <param name="featureId">The uniquely identifying name for the feature.</param>
    /// <param name="scenarioId">The screenplay scenario identity.</param>
    /// <param name="scenarioIdIsGenerated">Indicates whether <paramref name="idName"/> is auto-generated or not</param>
    /// <param name="featureIdIsGenerated">Indicates whether <paramref name="featureId"/> is auto-generated or not</param>
    public void BeginNewScenario(string idName,
                                 string friendlyName,
                                 string featureName,
                                 string featureId,
                                 Guid scenarioId,
                                 bool scenarioIdIsGenerated = false,
                                 bool featureIdIsGenerated = false)
    {
      var builder = new ScenarioBuilder(idName,
                                        friendlyName,
                                        featureName,
                                        featureId,
                                        scenarioIdIsGenerated,
                                        featureIdIsGenerated);
      
      var success = scenarios.TryAdd(scenarioId, builder);
      if(!success)
        throw new InvalidOperationException(Resources.ExceptionFormats.DuplicateScenarioInReportBuilder);
    }

    /// <summary>
    /// Reports the end of a scenario.
    /// </summary>
    /// <param name="outcome">If set to <c>false</c> then the scenario is marked as a failure.</param>
    /// <param name="scenarioId">The screenplay scenario identity.</param>
    public void EndScenario(bool? outcome,
                            Guid scenarioId)
    {
      var scenario = GetScenario(scenarioId);
      scenario.Finalise(outcome);
    }

    /// <summary>
    /// Begins reporting of a new performable.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="performable">Performable.</param>
    /// <param name="scenarioId">The screenplay scenario identity.</param>
    public void BeginPerformance(INamed actor, Performables.IPerformable performable,
                                 Guid scenarioId)
    {
      var scenario = GetScenario(scenarioId);
      scenario.BeginPerformance(actor, performable);
    }

    /// <summary>
    /// Begins reporting of a performance of a given type.
    /// </summary>
    /// <param name="performanceType">Performance type.</param>
    /// <param name="scenarioId">The screenplay scenario identity.</param>
    public void BeginPerformanceType(ReportableCategory performanceType,
                                     Guid scenarioId)
    {
      var scenario = GetScenario(scenarioId);
      scenario.BeginPerformanceType(performanceType);
    }

    /// <summary>
    /// Records that the current performable has received a result.
    /// </summary>
    /// <param name="performable">Performable.</param>
    /// <param name="result">Result.</param>
    /// <param name="scenarioId">The screenplay scenario identity.</param>
    public void RecordResult(Performables.IPerformable performable, object result,
                             Guid scenarioId)
    {
      var scenario = GetScenario(scenarioId);
      scenario.RecordResult(performable, result);
    }

    /// <summary>
    /// Records that the current performable has failed with an exception.
    /// </summary>
    /// <param name="performable">Performable.</param>
    /// <param name="exception">Exception.</param>
    /// <param name="scenarioId">The screenplay scenario identity.</param>
    public void RecordFailure(Performables.IPerformable performable, Exception exception,
                              Guid scenarioId)
    {
      var scenario = GetScenario(scenarioId);
      scenario.RecordFailure(performable, exception);
    }

    /// <summary>
    /// Records that the current performable has completed successfully.
    /// </summary>
    /// <param name="performable">Performable.</param>
    /// <param name="scenarioId">The screenplay scenario identity.</param>
    public void RecordSuccess(Performables.IPerformable performable,
                              Guid scenarioId)
    {
      var scenario = GetScenario(scenarioId);
      scenario.RecordSuccess(performable);
    }

    /// <summary>
    /// Ends the performance of the current type.
    /// </summary>
    public void EndPerformanceType(Guid scenarioId)
    {
      var scenario = GetScenario(scenarioId);
      scenario.EndPerformanceType();
    }

    /// <summary>
    /// Reports that the given actor has gained the given ability.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="ability">Ability.</param>
    /// <param name="scenarioId">The screenplay scenario identity.</param>
    public void GainAbility(INamed actor, IAbility ability,
                            Guid scenarioId)
    {
      var scenario = GetScenario(scenarioId);
      scenario.GainAbility(actor, ability);
    }

    /// <summary>
    /// Builds and returns the report from the current instance.
    /// </summary>
    /// <returns>The report.</returns>
    public Report GetReport()
    {
      var builtScenarios = scenarios.Values.Select(x => x.GetScenario());
      return reportFactory.GetReport(builtScenarios.ToArray());
    }

    ScenarioBuilder GetScenario(Guid identity)
    {
      ScenarioBuilder scenario;
      if(!scenarios.TryGetValue(identity, out scenario))
        throw new InvalidOperationException(Resources.ExceptionFormats.NoMatchingScenarioInReportBuilder);

      return scenario;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportBuilder"/> class.
    /// </summary>
    public ReportBuilder() : this(new ReportFactory()) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportBuilder"/> class.
    /// </summary>
    /// <param name="reportFactory">Report factory</param>
    public ReportBuilder(IReportFactory reportFactory)
    {
      if(reportFactory == null)
        throw new ArgumentNullException(nameof(reportFactory));

      this.reportFactory = reportFactory;
      scenarios = new ConcurrentDictionary<Guid, ScenarioBuilder>();
    }
  }
}
