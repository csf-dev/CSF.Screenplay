using System;
namespace CSF.Screenplay.Scenarios
{
  public class EndScenarioEventArgs : EventArgs
  {
    public IdAndName FeatureId { get; set; }

    public IdAndName ScenarioId { get; set; }

    public bool ScenarioIsSuccess { get; set; }
  }
}
