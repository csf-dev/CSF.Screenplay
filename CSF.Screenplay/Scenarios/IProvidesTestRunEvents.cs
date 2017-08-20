using System;
namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Indicates that the current instance provides the events which indicate the beginning/end of a test run.
  /// </summary>
  public interface IProvidesTestRunEvents
  {
    /// <summary>
    /// Occurs when a test run begins.
    /// </summary>
    event EventHandler BeginTestRun;

    /// <summary>
    /// Occurs when a test run is completed.
    /// </summary>
    event EventHandler CompleteTestRun;
  }
}
