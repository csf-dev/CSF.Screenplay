using System;
namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// End-scenario event arguments.
  /// </summary>
  public class EndScenarioEventArgs : EventArgs
  {
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

    /// <summary>
    /// Gets or sets a value indicating whether the scenario was a success.
    /// </summary>
    /// <value><c>true</c> if the scenario was a success; otherwise, <c>false</c>.</value>
    public bool ScenarioIsSuccess { get; set; }
  }
}
