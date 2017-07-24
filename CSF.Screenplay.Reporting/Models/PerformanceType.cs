using System;
namespace CSF.Screenplay.Reporting.Models
{
  /// <summary>
  /// Enumerates the available types of performance.
  /// </summary>
  public enum PerformanceType
  {
    /// <summary>
    /// An unspecified type of performance.
    /// </summary>
    Unspecified = 0,

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
