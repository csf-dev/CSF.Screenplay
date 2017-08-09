using System;
namespace CSF.Screenplay.Context
{
  public class BeginScenarioEventArgs : EventArgs
  {
    public string FeatureId { get; set; }

    public string FeatureName { get; set; }

    public string ScenarioId { get; set; }

    public string ScenarioName { get; set; }
  }
}
