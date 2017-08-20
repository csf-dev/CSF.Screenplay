using System;
namespace CSF.Screenplay.NUnit
{
  public interface IScenarioAdapter
  {
    string ScenarioName { get; }
    string ScenarioId { get; }
    string FeatureName { get; }
    string FeatureId { get; }
  }
}
