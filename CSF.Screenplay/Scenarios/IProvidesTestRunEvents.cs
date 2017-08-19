using System;
namespace CSF.Screenplay.Scenarios
{
  public interface IProvidesTestRunEvents
  {
    event EventHandler BeginTestRun;

    event EventHandler CompleteTestRun;
  }
}
