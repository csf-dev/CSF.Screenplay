using System;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Integration
{
  public interface IScreenplayIntegration
  {
    void EnsureServicesAreRegistered();
    void BeforeExecutingFirstScenario();
    void BeforeScenario(ScreenplayScenario scenario);
    void AfterScenario(ScreenplayScenario scenario, bool success);
    void AfterExecutedLastScenario();
    IScenarioFactory GetScenarioFactory();

  }
}
