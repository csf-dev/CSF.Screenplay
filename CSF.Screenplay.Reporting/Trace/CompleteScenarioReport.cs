using System;
namespace CSF.Screenplay.Reporting.Trace
{
  /// <summary>
  /// Reports the completion of a test scenario.
  /// </summary>
  public class CompleteScenarioReport
  {
    /// <summary>
    /// Gets a value indicating whether the scenario was a success.
    /// </summary>
    /// <value><c>true</c> if the scenario was a success; otherwise, <c>false</c>.</value>
    public bool Success { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Trace.CompleteScenarioReport"/> class.
    /// </summary>
    /// <param name="success">If set to <c>true</c> success.</param>
    public CompleteScenarioReport(bool success)
    {
      Success = success;
    }
  }
}
