using System;
namespace CSF.Screenplay.Scenarios
{
  public interface IScenarioFactory
  {
    ScreenplayScenario GetScenario(IdAndName featureId, IdAndName scenarioId);
  }
}
