using System;
namespace CSF.Screenplay.Context
{
  /// <summary>
  /// Begin-scenario event arguments.
  /// </summary>
  public class BeginScenarioEventArgs : EventArgs
  {
    /// <summary>
    /// Gets or sets the feature identifier.
    /// </summary>
    /// <value>The feature identifier.</value>
    public string FeatureId { get; set; }

    /// <summary>
    /// Gets or sets the name of the feature.
    /// </summary>
    /// <value>The name of the feature.</value>
    public string FeatureName { get; set; }

    /// <summary>
    /// Gets or sets the scenario identifier.
    /// </summary>
    /// <value>The scenario identifier.</value>
    public string ScenarioId { get; set; }

    /// <summary>
    /// Gets or sets the name of the scenario.
    /// </summary>
    /// <value>The name of the scenario.</value>
    public string ScenarioName { get; set; }
  }
}
