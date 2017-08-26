using System;
namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// A service which provides identification information about a Screenplay feature and scenario.
  /// </summary>
  public interface IScenarioName
  {
    /// <summary>
    /// Gets identifying information about the current feature under test.
    /// </summary>
    /// <value>The feature identifier.</value>
    IdAndName FeatureId { get; }

    /// <summary>
    /// Gets identifying information about the current scenario which is being tested.
    /// </summary>
    /// <value>The scenario identifier.</value>
    IdAndName ScenarioId { get; }
  }
}
