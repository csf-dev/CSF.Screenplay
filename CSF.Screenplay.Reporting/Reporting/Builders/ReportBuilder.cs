using System;
using System.Collections.Concurrent;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.ReportFormatting;
using CSF.Screenplay.ReportModel;

namespace CSF.Screenplay.Reporting.Builders
{
  /// <summary>
  /// Builder type which creates instances of <see cref="Report"/>.
  /// </summary>
  public class ReportBuilder : IBuildsReports
  {
    private readonly IFormatsObjectForReport objectFormatter;
    readonly IGetsReport reportFactory;
    readonly Func<Guid,IFormatsObjectForReport,IBuildsScenario> scenarioBuilderFactory;
    readonly ConcurrentDictionary<Guid, IBuildsScenario> scenarioBuilders;

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
                                 bool scenarioIdIsGenerated,
                                 bool featureIdIsGenerated)
    {
      var builder = scenarioBuilderFactory(scenarioId, objectFormatter);

      builder.ScenarioIdName = idName;
      builder.ScenarioFriendlyName = friendlyName;
      builder.ScenarioIdIsGenerated = scenarioIdIsGenerated;
      builder.FeatureIdName = featureId;
      builder.FeatureFriendlyName = featureName;
      builder.FeatureIdIsGenerated = featureIdIsGenerated;

      var success = scenarioBuilders.TryAdd(scenarioId, builder);
      if(!success)
        throw new ScenarioHasBegunAlreadyException(Resources.ExceptionFormats.DuplicateScenarioInReportBuilder);
    }

    /// <summary>
    /// Reports the end of a scenario.
    /// </summary>
    /// <param name="outcome">If set to <c>false</c> then the scenario is marked as a failure.</param>
    /// <param name="scenarioId">The screenplay scenario identity.</param>
    public void EndScenario(bool? outcome,
                            Guid scenarioId)
    {
      var scenario = GetScenarioBuilder(scenarioId);
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
      var scenario = GetScenarioBuilder(scenarioId);

      if(scenario.IsFinalised())
        throw new ScenarioIsFinalisedAlreadyException(Resources.ExceptionFormats.ScenarioAlreadyFinalised);
      
      scenario.BeginPerformance(actor, performable);
    }

    /// <summary>
    /// Begins reporting of a performance of a given type.
    /// </summary>
    /// <param name="performanceType">Performance type.</param>
    /// <param name="scenarioId">The screenplay scenario identity.</param>
    public void BeginPerformanceCategory(ReportableCategory performanceType,
                                     Guid scenarioId)
    {
      var scenario = GetScenarioBuilder(scenarioId);

      if(scenario.IsFinalised())
        throw new ScenarioIsFinalisedAlreadyException(Resources.ExceptionFormats.ScenarioAlreadyFinalised);
      
      scenario.BeginPerformanceCategory(performanceType);
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
      var scenario = GetScenarioBuilder(scenarioId);
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
      var scenario = GetScenarioBuilder(scenarioId);
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
      var scenario = GetScenarioBuilder(scenarioId);
      scenario.RecordSuccess(performable);
    }

    /// <summary>
    /// Ends the performance of the current type.
    /// </summary>
    public void EndPerformanceCategory(Guid scenarioId)
    {
      var scenario = GetScenarioBuilder(scenarioId);
      scenario.EndPerformanceCategory();
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
      var scenario = GetScenarioBuilder(scenarioId);
      scenario.GainAbility(actor, ability);
    }

    /// <summary>
    /// Builds and returns the report from the current instance.
    /// </summary>
    /// <returns>The report.</returns>
    public IReport GetReport()
    {
      return reportFactory.GetReport(scenarioBuilders.Values);
    }

    IBuildsScenario GetScenarioBuilder(Guid identity)
    {
      IBuildsScenario scenario;
      if(!scenarioBuilders.TryGetValue(identity, out scenario))
        throw new ScenarioHasNotBegunException(Resources.ExceptionFormats.NoMatchingScenarioInReportBuilder);

      return scenario;
    }

    /// <summary>
    /// Gets a finalised scenario, by its identifier.
    /// </summary>
    /// <returns>The finalised scenario.</returns>
    /// <param name="identity">Scenario identifier.</param>
    public Scenario GetFinalisedScenario(Guid identity)
    {
      var builder = GetScenarioBuilder(identity);

      if(!builder.IsFinalised())
      {
        string messageFormat = Resources.ExceptionFormats.ScenarioNotFinalisedYet;
        string message = String.Format(messageFormat, nameof(EndScenario));
        throw new ScenarioIsNotFinalisedException(message);
      }

      return builder.GetScenario();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportBuilder"/> class.
    /// </summary>
    public ReportBuilder(IFormatsObjectForReport objectFormatter) : this(objectFormatter, null, null) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportBuilder"/> class.
    /// </summary>
    /// <param name="reportFactory">Report factory</param>
    /// <param name="scenarioBuilderFactory">A factory function which creates scenario builders</param>
    /// <param name="objectFormatter">An object formatter implementation</param>
    public ReportBuilder(IFormatsObjectForReport objectFormatter,
                         IGetsReport reportFactory,
                         Func<Guid,IFormatsObjectForReport,IBuildsScenario> scenarioBuilderFactory)
    {
      if(objectFormatter == null)
        throw new ArgumentNullException(nameof(objectFormatter));
      
      this.objectFormatter = objectFormatter;
      this.reportFactory = reportFactory ?? new ReportFactory();

      if(scenarioBuilderFactory != null)
        this.scenarioBuilderFactory = scenarioBuilderFactory;
      else
        this.scenarioBuilderFactory = (guid,formatter) => new ScenarioBuilder(this.objectFormatter);

      scenarioBuilders = new ConcurrentDictionary<Guid, IBuildsScenario>();
    }
  }
}
