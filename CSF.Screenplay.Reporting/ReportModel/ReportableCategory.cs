using System;
namespace CSF.Screenplay.ReportModel
{
  /// <summary>
  /// Enumerates the available types of performance.
  /// </summary>
  public enum ReportableCategory
  {
    /// <summary>
    /// A given (arrange) performance.
    /// </summary>
    Given = 1,

    /// <summary>
    /// A when (act) performance.
    /// </summary>
    When,

    /// <summary>
    /// A then (assert) performance.
    /// </summary>
    Then
  }
}
