using System;
namespace CSF.Screenplay.Context
{
  /// <summary>
  /// End-scenario event arguments.
  /// </summary>
  public class EndScenarioEventArgs : EventArgs
  {
    /// <summary>
    /// Gets or sets a value indicating whether the scenario was a success.
    /// </summary>
    /// <value><c>true</c> if the scenario was a success; otherwise, <c>false</c>.</value>
    public bool Success { get; set; }
  }
}
