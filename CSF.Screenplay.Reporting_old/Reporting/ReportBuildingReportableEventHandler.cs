using System;
using CSF.Screenplay.ReportFormatting;
using CSF.Screenplay.Reporting.Builders;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Implementation of <see cref="IHandlesReportableEvents"/> which builds and exposes an object model of the
  /// by also implementing <see cref="IGetsReportModel"/>. It also implements <see cref="IExposesCompletedScenarios"/>.
  /// </summary>
  public class ReportBuildingReportableEventHandler : IHandlesReportableEvents, IGetsReportModel, IExposesCompletedScenarios
  {
    ReportBuilder builder;
    private readonly IFormatsObjectForReport objectFormatter;

    /// <summary>
    /// Indicates to the reporter that a new test-run has begun.
    /// </summary>
    public void BeginNewTestRun()
    {
      BeginNewReport();
    }

    /// <summary>
    /// Indicates to the reporter that the test run has completed and that the report should be finalised.
    /// </summary>
    public void CompleteTestRun()
    {
      InvokeTestRunCompleted();
    }

    /// <summary>
    /// Indicates to the reporter that a new scenario has begun.
    /// </summary>
    /// <param name="scenarioName">The scenario name.</param>
    /// <param name="featureName">The feature name.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void BeginNewScenario(IdAndName scenarioName,
                                 IdAndName featureName,
                                 Guid scenarioIdentity)
    {
      builder.BeginNewScenario(scenarioName?.Identity,
                               scenarioName?.Name,
                               featureName?.Name,
                               featureName?.Identity,
                               scenarioIdentity,
                               (scenarioName?.IsIdentityGenerated).GetValueOrDefault(),
                               (featureName?.IsIdentityGenerated).GetValueOrDefault());
    }

    /// <summary>
    /// Indicates to the reporter that a scenario has finished.
    /// </summary>
    /// <param name="outcome"><c>true</c> if the scenario was a success; <c>false</c> otherwise.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void CompleteScenario(bool? outcome, Guid scenarioIdentity)
    {
      builder.EndScenario(outcome, scenarioIdentity);

      var scenario = builder.GetFinalisedScenario(scenarioIdentity);
      InvokeScenarioCompleted(scenario);
    }

    /// <summary>
    /// Gets the completed report model.
    /// </summary>
    /// <returns>The report.</returns>
    public virtual ReportModel.IReport GetReport()
    {
      return builder.GetReport();
    }

    /// <summary>
    /// Reports that an actor has begun a 'given' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void BeginGiven(Actors.INamed actor, Guid scenarioIdentity)
    {
      builder.BeginPerformanceCategory(ReportModel.ReportableCategory.Given, scenarioIdentity);
    }

    /// <summary>
    /// Reports that an actor has ended the 'given' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void EndGiven(Actors.INamed actor, Guid scenarioIdentity)
    {
      builder.EndPerformanceCategory(scenarioIdentity);
    }

    /// <summary>
    /// Reports that an actor has begun a 'when' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void BeginWhen(Actors.INamed actor, Guid scenarioIdentity)
    {
      builder.BeginPerformanceCategory(ReportModel.ReportableCategory.When, scenarioIdentity);
    }

    /// <summary>
    /// Reports that an actor has ended the 'when' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void EndWhen(Actors.INamed actor, Guid scenarioIdentity)
    {
      builder.EndPerformanceCategory(scenarioIdentity);
    }

    /// <summary>
    /// Reports that an actor has begun a 'then' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void BeginThen(Actors.INamed actor, Guid scenarioIdentity)
    {
      builder.BeginPerformanceCategory(ReportModel.ReportableCategory.Then, scenarioIdentity);
    }

    /// <summary>
    /// Reports that an actor has ended the 'then' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void EndThen(Actors.INamed actor, Guid scenarioIdentity)
    {
      builder.EndPerformanceCategory(scenarioIdentity);
    }

    /// <summary>
    /// Reports that an actor has gained an ability.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="ability">The ability.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void GainAbility(Actors.INamed actor, Abilities.IAbility ability, Guid scenarioIdentity)
    {
      builder.GainAbility(actor, ability, scenarioIdentity);
    }

    /// <summary>
    /// Reports that a performable item has begun.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void Begin(Actors.INamed actor, Performables.IPerformable performable, Guid scenarioIdentity)
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
    public void Result(Actors.INamed actor, Performables.IPerformable performable, object result, Guid scenarioIdentity)
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
    public void Failure(Actors.INamed actor, Performables.IPerformable performable, Exception exception, Guid scenarioIdentity)
    {
      builder.RecordFailure(performable, exception, scenarioIdentity);
    }

    /// <summary>
    /// Reports that a performable item has completed successfully.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void Success(Actors.INamed actor, Performables.IPerformable performable, Guid scenarioIdentity)
    {
      builder.RecordSuccess(performable, scenarioIdentity);
    }

    /// <summary>
    /// Occurs when a scenario has just completed.
    /// </summary>
    public event EventHandler ScenarioCompleted;

    /// <summary>
    /// Invokes the <see cref="ScenarioCompleted"/> event.
    /// </summary>
    /// <param name="scenario">The scenario.</param>
    protected virtual void InvokeScenarioCompleted(ReportModel.Scenario scenario)
    {
      var args = new ScenarioCompletedEventArgs { Scenario = scenario };
      ScenarioCompleted?.Invoke(this, args);
    }

    /// <summary>
    /// Occurs when a test run has just completed.
    /// </summary>
    public event EventHandler TestRunCompleted;

    /// <summary>
    /// Invokes the <see cref="TestRunCompleted"/> event.
    /// </summary>
    protected virtual void InvokeTestRunCompleted()
    {
      var args = new EventArgs();
      TestRunCompleted?.Invoke(this, args);
    }

    void BeginNewReport()
    {
      builder = new ReportBuilder(this.objectFormatter);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportBuildingReportableEventHandler"/> class.
    /// </summary>
    public ReportBuildingReportableEventHandler() : this(null) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportBuildingReportableEventHandler"/> class.
    /// </summary>
    /// <param name="objectFormatter">An object formatter</param>
    public ReportBuildingReportableEventHandler(IFormatsObjectForReport objectFormatter)
    {
      this.objectFormatter = objectFormatter ?? new DefaultObjectFormattingStrategy();
      BeginNewReport();
    }
  }
}
