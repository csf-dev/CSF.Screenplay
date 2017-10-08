using System;
using System.Diagnostics;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Reporting.Trace
{
  /// <summary>
  /// Implementation of <see cref="IReporter"/> which writes report data to the <c>System.Diagnostics.TraceSource</c>
  /// held within the <see cref="TraceConstants"/> class.
  /// </summary>
  public class TraceReporter : NoOpReporter
  {
    /// <summary>
    /// Reports that an actor has gained an ability.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="ability">The ability.</param>
    protected override void GainAbility(INamed actor, IAbility ability, Guid scenarioIdentity)
    {
      var report = new AbilityReport(actor, ability);
      Trace(TraceConstants.GainAbility, report);
    }

    /// <summary>
    /// Reports that a performable item has begun.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    protected override void Begin(INamed actor, IPerformable performable, Guid scenarioIdentity)
    {
      var report = new BeginReport(actor, performable);
      Trace(TraceConstants.BeginPerformance, report);
    }

    /// <summary>
    /// Reports that a performable item has completed successfully.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    protected override void Success(INamed actor, IPerformable performable, Guid scenarioIdentity)
    {
      var report = new SuccessReport(actor, performable);
      Trace(TraceConstants.PerformanceSuccess, report);
    }

    /// <summary>
    /// Reports that a performable item has produced a result.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="result">The result produced.</param>
    protected override void Result(INamed actor, IPerformable performable, object result, Guid scenarioIdentity)
    {
      var report = new ResultReport(actor, performable, result);
      Trace(TraceConstants.PerformanceResult, report);
    }

    /// <summary>
    /// Reports that a performable item has failed and possible terminated early.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="exception">An exception encountered whilst attempting to perform the item.</param>
    protected override void Failure(INamed actor, IPerformable performable, Exception exception, Guid scenarioIdentity)
    {
      var report = new FailureReport(actor, performable, exception);
      Trace(TraceConstants.PerformanceFailure, report);
    }

    /// <summary>
    /// Reports that an actor has begun a 'given' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    protected override void BeginGiven(INamed actor, Guid scenarioIdentity)
    {
      var report = new ActorReport(actor);
      Trace(TraceConstants.BeginGiven, report);
    }

    /// <summary>
    /// Reports that an actor has ended the 'given' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    protected override void EndGiven(INamed actor, Guid scenarioIdentity)
    {
      var report = new ActorReport(actor);
      Trace(TraceConstants.EndGiven, report);
    }

    /// <summary>
    /// Reports that an actor has begun a 'when' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    protected override void BeginWhen(INamed actor, Guid scenarioIdentity)
    {
      var report = new ActorReport(actor);
      Trace(TraceConstants.BeginWhen, report);
    }
    /// <summary>
    /// Reports that an actor has ended the 'when' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    protected override void EndWhen(INamed actor, Guid scenarioIdentity)
    {
      var report = new ActorReport(actor);
      Trace(TraceConstants.EndWhen, report);
    }

    /// <summary>
    /// Reports that an actor has begun a 'then' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    protected override void BeginThen(INamed actor, Guid scenarioIdentity)
    {
      var report = new ActorReport(actor);
      Trace(TraceConstants.BeginThen, report);
    }

    /// <summary>
    /// Reports that an actor has ended the 'then' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    protected override void EndThen(INamed actor, Guid scenarioIdentity)
    {
      var report = new ActorReport(actor);
      Trace(TraceConstants.EndThen, report);
    }

    /// <summary>
    /// Indicates to the reporter that a new test-run has begun.
    /// </summary>
    protected override void BeginNewTestRun()
    {
      Trace(TraceConstants.BeginNewTestRun);
    }

    /// <summary>
    /// Indicates to the reporter that the test run has completed and that the report should be finalised.
    /// </summary>
    protected override void CompleteTestRun()
    {
      Trace(TraceConstants.CompleteTestRun);
    }

    /// <summary>
    /// Indicates to the reporter that a new scenario has begun.
    /// </summary>
    /// <param name="friendlyName">The friendly scenario name.</param>
    /// <param name="featureName">The feature name.</param>
    /// <param name="idName">The uniquely identifying name for the test.</param>
    /// <param name="featureId">The uniquely identifying name for the feature.</param>
    protected override void BeginNewScenario(string idName, string friendlyName, string featureName, string featureId, Guid scenarioIdentity)
    {
      var report = new BeginScenarioReport(idName, friendlyName, featureId, featureName);
      Trace(TraceConstants.BeginNewScenario, report);
    }

    /// <summary>
    /// Indicates to the reporter that a scenario has finished.
    /// </summary>
    /// <param name="success">
    /// <c>true</c> if the scenario was a success; <c>false</c> otherwise.</param>
    protected override void CompleteScenario(bool success, Guid scenarioIdentity)
    {
      var report = new CompleteScenarioReport(success);
      Trace(TraceConstants.CompleteScenario, report);
    }

    void Trace(TracableEvent tracableEvent, object report)
    {
      if(tracableEvent == null)
        throw new ArgumentNullException(nameof(tracableEvent));
      
      TraceConstants.Source.TraceData(tracableEvent.EventType, tracableEvent.EventId, report);
    }

    void Trace(TracableEvent tracableEvent)
    {
      if(tracableEvent == null)
        throw new ArgumentNullException(nameof(tracableEvent));

      TraceConstants.Source.TraceEvent(tracableEvent.EventType, tracableEvent.EventId);
    }

  }
}
