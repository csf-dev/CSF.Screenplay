using System;
namespace CSF.Screenplay.Scenarios
{
  public class BeginScenarioEventArgs : EventArgs
  {
    public IdAndName FeatureId { get; set; }

    public IdAndName ScenarioId { get; set; }
  }
}
