using System;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Integration
{
  /// <summary>
  /// Simple type which provides the test run events and notifies subscribers of the beginning/end of the run.
  /// </summary>
  class TestRunEvents : IProvidesTestRunEvents
  {
    public event EventHandler BeginTestRun;

    public event EventHandler CompleteTestRun;

    internal void NotifyBeginTestRun()
    {
      BeginTestRun?.Invoke(this, EventArgs.Empty);
    }

    internal void NotifyCompleteTestRun()
    {
      CompleteTestRun?.Invoke(this, EventArgs.Empty);
    }
  }
}
