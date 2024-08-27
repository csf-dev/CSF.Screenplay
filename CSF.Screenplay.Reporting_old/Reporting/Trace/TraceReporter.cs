﻿using System;
using System.Diagnostics;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Reporting.Trace
{
  /// <summary>
  /// Implementation of <see cref="IHandlesReportableEvents"/> which writes report data to the
  /// <c>System.Diagnostics.TraceSource</c> held within the <see cref="TraceConstants"/> class.
  /// </summary>
  public class TraceReporter : IHandlesReportableEvents
  {
    /// <summary>
    /// Reports that an actor has gained an ability.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="ability">The ability.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void GainAbility(INamed actor, IAbility ability, Guid scenarioIdentity)
    {
      var report = new AbilityReport(actor, ability);
      Trace(TraceConstants.GainAbility, report);
    }

    /// <summary>
    /// Reports that a performable item has begun.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void Begin(INamed actor, IPerformable performable, Guid scenarioIdentity)
    {
      var report = new BeginReport(actor, performable);
      Trace(TraceConstants.BeginPerformance, report);
    }

    /// <summary>
    /// Reports that a performable item has completed successfully.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void Success(INamed actor, IPerformable performable, Guid scenarioIdentity)
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
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void Result(INamed actor, IPerformable performable, object result, Guid scenarioIdentity)
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
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void Failure(INamed actor, IPerformable performable, Exception exception, Guid scenarioIdentity)
    {
      var report = new FailureReport(actor, performable, exception);
      Trace(TraceConstants.PerformanceFailure, report);
    }

    /// <summary>
    /// Reports that an actor has begun a 'given' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void BeginGiven(INamed actor, Guid scenarioIdentity)
    {
      var report = new ActorReport(actor);
      Trace(TraceConstants.BeginGiven, report);
    }

    /// <summary>
    /// Reports that an actor has ended the 'given' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void EndGiven(INamed actor, Guid scenarioIdentity)
    {
      var report = new ActorReport(actor);
      Trace(TraceConstants.EndGiven, report);
    }

    /// <summary>
    /// Reports that an actor has begun a 'when' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void BeginWhen(INamed actor, Guid scenarioIdentity)
    {
      var report = new ActorReport(actor);
      Trace(TraceConstants.BeginWhen, report);
    }
    /// <summary>
    /// Reports that an actor has ended the 'when' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void EndWhen(INamed actor, Guid scenarioIdentity)
    {
      var report = new ActorReport(actor);
      Trace(TraceConstants.EndWhen, report);
    }

    /// <summary>
    /// Reports that an actor has begun a 'then' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void BeginThen(INamed actor, Guid scenarioIdentity)
    {
      var report = new ActorReport(actor);
      Trace(TraceConstants.BeginThen, report);
    }

    /// <summary>
    /// Reports that an actor has ended the 'then' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void EndThen(INamed actor, Guid scenarioIdentity)
    {
      var report = new ActorReport(actor);
      Trace(TraceConstants.EndThen, report);
    }

    /// <summary>
    /// Indicates to the reporter that a new test-run has begun.
    /// </summary>
    public void BeginNewTestRun()
    {
      Trace(TraceConstants.BeginNewTestRun);
    }

    /// <summary>
    /// Indicates to the reporter that the test run has completed and that the report should be finalised.
    /// </summary>
    public void CompleteTestRun()
    {
      Trace(TraceConstants.CompleteTestRun);
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
      var report = new BeginScenarioReport(scenarioName?.Identity,
                                           scenarioName?.Name,
                                           featureName?.Identity,
                                           featureName?.Name);
      Trace(TraceConstants.BeginNewScenario, report);
    }

    /// <summary>
    /// Indicates to the reporter that a scenario has finished.
    /// </summary>
    /// <param name="outcome">
    /// <c>true</c> if the scenario was a success; <c>false</c> otherwise.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void CompleteScenario(bool? outcome, Guid scenarioIdentity)
    {
      var report = new CompleteScenarioReport(outcome);
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
