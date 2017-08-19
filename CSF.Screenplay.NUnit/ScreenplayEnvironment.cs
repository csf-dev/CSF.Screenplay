using System;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.NUnit
{
  internal class ScreenplayEnvironment : IProvidesTestRunEvents
  {
    public ServiceRegistry ServiceRegistry { get; set; }

    public IScenarioFactory GetScenarioFactory()
    {
      return new ScenarioFactory(ServiceRegistry);
    }

    public event EventHandler BeginTestRun;

    public event EventHandler CompleteTestRun;

    public void NotifyBeginTestRun()
    {
      BeginTestRun?.Invoke(this, EventArgs.Empty);
    }

    public void NotifyCompleteTestRun()
    {
      CompleteTestRun?.Invoke(this, EventArgs.Empty);
    }

    #region Singleton implementation

    static readonly ScreenplayEnvironment singleton;
    static ScreenplayEnvironment()
    {
      singleton = new ScreenplayEnvironment();
    }
    public static ScreenplayEnvironment Default => singleton;

    #endregion
  }
}
