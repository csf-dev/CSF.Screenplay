using System;
namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Begin-scenario event arguments.
  /// </summary>
  public class BeginScenarioEventArgs : EventArgs
  {
    /// <summary>
    /// Gets or sets the scenario identity.
    /// </summary>
    /// <value>The scenario identity.</value>
    public Guid ScenarioIdentity { get; set; }

    /// <summary>
    /// Gets or sets the feature identifier.
    /// </summary>
    /// <value>The feature identifier.</value>
    public IdAndName FeatureId { get; set; }

    /// <summary>
    /// Gets or sets the scenario identifier.
    /// </summary>
    /// <value>The scenario identifier.</value>
    public IdAndName ScenarioId { get; set; }
  }
}
