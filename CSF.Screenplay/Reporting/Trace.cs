using System;
using System.Diagnostics;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Singleton which contains information about the trace source configured for this assembly.
  /// </summary>
  static class Trace
  {
    /// <summary>
    /// Gets the name of the trace source.
    /// </summary>
    internal const string SourceName = "CSF.Screenplay";

    /// <summary>
    /// Gets the trace source itself.
    /// </summary>
    internal static readonly TraceSource Source = new TraceSource(SourceName);

    /// <summary>
    /// The <c>TraceEventType</c> used for begin events.
    /// </summary>
    internal const TraceEventType BeginType = TraceEventType.Verbose;

    /// <summary>
    /// The numeric ID for begin events.
    /// </summary>
    internal const int BeginId = 1;

    /// <summary>
    /// The <c>TraceEventType</c> used for success events.
    /// </summary>
    internal const TraceEventType SuccessType = TraceEventType.Information;

    /// <summary>
    /// The numeric ID for success events.
    /// </summary>
    internal const int SuccessId = 2;

    /// <summary>
    /// The <c>TraceEventType</c> used for failure events.
    /// </summary>
    internal const TraceEventType FailureType = TraceEventType.Error;

    /// <summary>
    /// The numeric ID for failure events.
    /// </summary>
    internal const int FailureId = 3;

    /// <summary>
    /// The <c>TraceEventType</c> used for result events.
    /// </summary>
    internal const TraceEventType ResultType = TraceEventType.Verbose;

    /// <summary>
    /// The numeric ID for result events.
    /// </summary>
    internal const int ResultId = 4;

    /// <summary>
    /// The <c>TraceEventType</c> used for gain-ability events.
    /// </summary>
    internal const TraceEventType AbilityType = TraceEventType.Verbose;

    /// <summary>
    /// The numeric ID for gain-ability events.
    /// </summary>
    internal const int AbilityId = 5;
  }
}
