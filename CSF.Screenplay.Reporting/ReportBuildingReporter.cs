using System;
using CSF.Screenplay.Reporting.Builders;
using CSF.Screenplay.Reporting.Models;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Implementation of <see cref="IReporter"/> which builds a report model.  Does nothing on its own but may be
  /// subclassed to provide reporting functionality based upon the report model.
  /// </summary>
  public class ReportBuildingReporter : NoOpReporter, IModelBuildingReporter
  {
    ReportBuilder builder;

    /// <summary>
    /// Gets the report builder.
    /// </summary>
    /// <value>The report builder.</value>
    protected ReportBuilder ReportBuilder => builder;

    /// <summary>
    /// Gets or sets a value indicating whether this reporter should mark scenario identifiers as auto-generated and
    /// thus meaningless in reports.
    /// </summary>
    /// <value><c>true</c> if the reporter should mark scenario identifiers as generated; otherwise, <c>false</c>.</value>
    public bool MarkScenarioIdsAsGenerated { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this reporter should mark feature identifiers as auto-generated and
    /// thus meaningless in reports.
    /// </summary>
    /// <value><c>true</c> if the reporter should mark feature identifiers as generated; otherwise, <c>false</c>.</value>
    public bool MarkFeatureIdsAsGenerated { get; set; }

    /// <summary>
    /// Indicates to the reporter that a new test-run has begun.
    /// </summary>
    protected override void BeginNewTestRun()
    {
      BeginNewReport();
    }

    /// <summary>
    /// Indicates to the reporter that a new scenario has begun.
    /// </summary>
    /// <param name="friendlyName">The friendly scenario name.</param>
    /// <param name="featureName">The feature name.</param>
    /// <param name="idName">The uniquely identifying name for the test.</param>
    /// <param name="featureId">The uniquely identifying name for the feature.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected override void BeginNewScenario(string idName, string friendlyName, string featureName, string featureId, Guid scenarioIdentity)
    {
      builder.BeginNewScenario(idName,
                               friendlyName,
                               featureName,
                               featureId,
                               scenarioIdentity,
                               MarkScenarioIdsAsGenerated,
                               MarkFeatureIdsAsGenerated);
    }

    /// <summary>
    /// Indicates to the reporter that a scenario has finished.
    /// </summary>
    /// <param name="outcome"><c>true</c> if the scenario was a success; <c>false</c> otherwise.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected override void CompleteScenario(bool? outcome, Guid scenarioIdentity)
    {
      builder.EndScenario(outcome, scenarioIdentity);
    }

    /// <summary>
    /// Gets the completed report model.
    /// </summary>
    /// <returns>The report.</returns>
    public virtual Report GetReport()
    {
      return builder.GetReport();
    }

    /// <summary>
    /// Reports that an actor has begun a 'given' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected override void BeginGiven(Actors.INamed actor, Guid scenarioIdentity)
    {
      builder.BeginPerformanceType(Models.PerformanceType.Given, scenarioIdentity);
    }

    /// <summary>
    /// Reports that an actor has ended the 'given' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected override void EndGiven(Actors.INamed actor, Guid scenarioIdentity)
    {
      builder.EndPerformanceType(scenarioIdentity);
    }

    /// <summary>
    /// Reports that an actor has begun a 'when' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected override void BeginWhen(Actors.INamed actor, Guid scenarioIdentity)
    {
      builder.BeginPerformanceType(Models.PerformanceType.When, scenarioIdentity);
    }

    /// <summary>
    /// Reports that an actor has ended the 'when' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected override void EndWhen(Actors.INamed actor, Guid scenarioIdentity)
    {
      builder.EndPerformanceType(scenarioIdentity);
    }

    /// <summary>
    /// Reports that an actor has begun a 'then' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected override void BeginThen(Actors.INamed actor, Guid scenarioIdentity)
    {
      builder.BeginPerformanceType(Models.PerformanceType.Then, scenarioIdentity);
    }

    /// <summary>
    /// Reports that an actor has ended the 'then' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected override void EndThen(Actors.INamed actor, Guid scenarioIdentity)
    {
      builder.EndPerformanceType(scenarioIdentity);
    }

    /// <summary>
    /// Reports that an actor has gained an ability.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="ability">The ability.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected override void GainAbility(Actors.INamed actor, Abilities.IAbility ability, Guid scenarioIdentity)
    {
      builder.GainAbility(actor, ability, scenarioIdentity);
    }

    /// <summary>
    /// Reports that a performable item has begun.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected override void Begin(Actors.INamed actor, Performables.IPerformable performable, Guid scenarioIdentity)
    {
      builder.BeginPerformance(actor, performable, scenarioIdentity);
    }

    /// <summary>
    /// Reports that a performable item has produced a result.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="result">The result produced.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected override void Result(Actors.INamed actor, Performables.IPerformable performable, object result, Guid scenarioIdentity)
    {
      builder.RecordResult(performable, result, scenarioIdentity);
    }

    /// <summary>
    /// Reports that a performable item has failed and possible terminated early.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="exception">An exception encountered whilst attempting to perform the item.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected override void Failure(Actors.INamed actor, Performables.IPerformable performable, Exception exception, Guid scenarioIdentity)
    {
      builder.RecordFailure(performable, exception, scenarioIdentity);
    }

    /// <summary>
    /// Reports that a performable item has completed successfully.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected override void Success(Actors.INamed actor, Performables.IPerformable performable, Guid scenarioIdentity)
    {
      builder.RecordSuccess(performable, scenarioIdentity);
    }

    void BeginNewReport()
    {
      builder = new ReportBuilder();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportBuildingReporter"/> class.
    /// </summary>
    public ReportBuildingReporter()
    {
      BeginNewReport();
    }
  }
}
