using System;
namespace CSF.Screenplay.ReportModel
{
  /// <summary>
  /// Enumerates the available types of performance.
  /// </summary>
  public enum ReportableCategory
  {
    /// <summary>
    /// No category applicable.
    /// </summary>
    None = 0,

    /// <summary>
    /// A given (arrange) performance.
    /// </summary>
    Given,

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
