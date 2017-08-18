using System;
using System.Diagnostics;

namespace CSF.Screenplay.Reporting.Trace
{
  /// <summary>
  /// Singleton which contains information about the trace source configured for this assembly.
  /// </summary>
  static class TraceConstants
  {
    /// <summary>
    /// Gets the name of the trace source.
    /// </summary>
    internal static readonly string SourceName = typeof(IReporter).Namespace;

    /// <summary>
    /// Gets the trace source itself.
    /// </summary>
    internal static readonly TraceSource Source = new TraceSource(SourceName);

    internal static readonly TracableEvent
      GainAbility           = new TracableEvent(EventTypes.GainAbility, EventIds.GainAbility),
      BeginPerformance      = new TracableEvent(EventTypes.BeginPerformance, EventIds.BeginPerformance),
      PerformanceSuccess    = new TracableEvent(EventTypes.PerformanceSuccess, EventIds.PerformanceSuccess),
      PerformanceResult     = new TracableEvent(EventTypes.PerformanceResult, EventIds.PerformanceResult),
      PerformanceFailure    = new TracableEvent(EventTypes.PerformanceFailure, EventIds.PerformanceFailure),
      BeginGiven            = new TracableEvent(EventTypes.BeginGiven, EventIds.BeginGiven),
      EndGiven              = new TracableEvent(EventTypes.EndGiven, EventIds.EndGiven),
      BeginWhen             = new TracableEvent(EventTypes.BeginWhen, EventIds.BeginWhen),
      EndWhen               = new TracableEvent(EventTypes.EndWhen, EventIds.EndWhen),
      BeginThen             = new TracableEvent(EventTypes.BeginThen, EventIds.BeginThen),
      EndThen               = new TracableEvent(EventTypes.EndThen, EventIds.EndThen),
      BeginNewTestRun       = new TracableEvent(EventTypes.BeginNewTestRun, EventIds.BeginNewTestRun),
      CompleteTestRun       = new TracableEvent(EventTypes.CompleteTestRun, EventIds.CompleteTestRun),
      BeginNewScenario      = new TracableEvent(EventTypes.BeginNewScenario, EventIds.BeginNewScenario),
      CompleteScenario      = new TracableEvent(EventTypes.CompleteScenario, EventIds.CompleteScenario);
      
  }

  /// <summary>
  /// Numeric identifiers for traceable events.
  /// </summary>
  class EventIds
  {
    internal const int
      GainAbility           = 1,
      BeginPerformance      = 2,
      PerformanceSuccess    = 3,
      PerformanceResult     = 4,
      PerformanceFailure    = 5,
      BeginGiven            = 6,
      EndGiven              = 7,
      BeginWhen             = 8,
      EndWhen               = 9,
      BeginThen             = 10,
      EndThen               = 11,
      BeginNewTestRun       = 12,
      CompleteTestRun       = 13,
      BeginNewScenario      = 14,
      CompleteScenario      = 15;
  }

  /// <summary>
  /// Trace event types for various traceable events.
  /// </summary>
  class EventTypes
  {
    internal const TraceEventType
      GainAbility           = TraceEventType.Verbose,
      BeginPerformance      = TraceEventType.Verbose,
      PerformanceSuccess    = TraceEventType.Information,
      PerformanceResult     = TraceEventType.Verbose,
      PerformanceFailure    = TraceEventType.Error,
      BeginGiven            = TraceEventType.Verbose,
      EndGiven              = TraceEventType.Verbose,
      BeginWhen             = TraceEventType.Verbose,
      EndWhen               = TraceEventType.Verbose,
      BeginThen             = TraceEventType.Verbose,
      EndThen               = TraceEventType.Verbose,
      BeginNewTestRun       = TraceEventType.Information,
      CompleteTestRun       = TraceEventType.Verbose,
      BeginNewScenario      = TraceEventType.Information,
      CompleteScenario      = TraceEventType.Verbose;
  }
}
