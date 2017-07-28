using System;
using CSF.Screenplay.Reporting.Builders;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Implementation of <see cref="IReporter"/> which builds a report model.  Does nothing on its own but may be
  /// subclassed to provide reporting functionality based upon the report model.
  /// </summary>
  public abstract class ReportBuildingReporter : NoOpReporter
  {
    ReportBuilder builder;

    /// <summary>
    /// Gets the report builder.
    /// </summary>
    /// <value>The report builder.</value>
    protected ReportBuilder ReportBuilder => builder;

    /// <summary>
    /// Indicates to the reporter that a new test-run has begun.
    /// </summary>
    public override void BeginNewTestRun()
    {
      BeginNewReport();
    }

    /// <summary>
    /// Indicates to the reporter that the test run has completed and that the report should be finalised.
    /// </summary>
    public abstract override void CompleteTestRun();

    /// <summary>
    /// Indicates to the reporter that a new scenario has begun.
    /// </summary>
    /// <param name="friendlyName">The friendly scenario name.</param>
    /// <param name="featureName">The feature name.</param>
    /// <param name="idName">The uniquely identifying name for the test.</param>
    public override void BeginNewScenario(string idName, string friendlyName, string featureName)
    {
      builder.BeginNewScenario(idName, friendlyName, featureName);
    }

    /// <summary>
    /// Indicates to the reporter that a scenario has finished.
    /// </summary>
    /// <param name="success"><c>true</c> if the scenario was a success; <c>false</c> otherwise.</param>
    public override void CompleteScenario(bool success)
    {
      builder.EndScenario(success);
    }

    /// <summary>
    /// Reports that an actor has begun a 'given' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    protected override void BeginGiven(Actors.INamed actor)
    {
      builder.BeginPerformance(Models.PerformanceType.Given);
    }

    /// <summary>
    /// Reports that an actor has ended the 'given' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    protected override void EndGiven(Actors.INamed actor)
    {
      builder.EndPerformance();
    }

    /// <summary>
    /// Reports that an actor has begun a 'when' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    protected override void BeginWhen(Actors.INamed actor)
    {
      builder.BeginPerformance(Models.PerformanceType.When);
    }

    /// <summary>
    /// Reports that an actor has ended the 'when' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    protected override void EndWhen(Actors.INamed actor)
    {
      builder.EndPerformance();
    }

    /// <summary>
    /// Reports that an actor has begun a 'then' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    protected override void BeginThen(Actors.INamed actor)
    {
      builder.BeginPerformance(Models.PerformanceType.Then);
    }

    /// <summary>
    /// Reports that an actor has ended the 'then' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    protected override void EndThen(Actors.INamed actor)
    {
      builder.EndPerformance();
    }

    /// <summary>
    /// Reports that an actor has gained an ability.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="ability">The ability.</param>
    protected override void GainAbility(Actors.INamed actor, Abilities.IAbility ability)
    {
      builder.GainAbility(actor, ability);
    }

    /// <summary>
    /// Reports that a performable item has begun.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    protected override void Begin(Actors.INamed actor, Performables.IPerformable performable)
    {
      builder.BeginPerformance(actor, performable);
    }

    /// <summary>
    /// Reports that a performable item has produced a result.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="result">The result produced.</param>
    protected override void Result(Actors.INamed actor, Performables.IPerformable performable, object result)
    {
      builder.RecordResult(performable, result);
    }

    /// <summary>
    /// Reports that a performable item has failed and possible terminated early.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="exception">An exception encountered whilst attempting to perform the item.</param>
    protected override void Failure(Actors.INamed actor, Performables.IPerformable performable, Exception exception)
    {
      builder.RecordFailure(performable, exception);
    }

    /// <summary>
    /// Reports that a performable item has completed successfully.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    protected override void Success(Actors.INamed actor, Performables.IPerformable performable)
    {
      builder.RecordSuccess(performable);
    }

    /// <summary>
    /// Gets the completed report model.
    /// </summary>
    /// <returns>The report.</returns>
    protected virtual Models.Report GetReportModel()
    {
      return builder.GetReport();
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
