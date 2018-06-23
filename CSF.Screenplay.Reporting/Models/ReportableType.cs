using System;
namespace CSF.Screenplay.Reporting.Models
{
  /// <summary>
  /// Enumerates the possible outcomes of a performance.
  /// </summary>
  public enum ReportableType
  {
    /// <summary>
    /// The reportable was a performance which was a success.
    /// </summary>
    Success = 1,

    /// <summary>
    /// The reportable was a performance which was a success and yielded a result.
    /// </summary>
    SuccessWithResult,

    /// <summary>
    /// The reportable was a performance which was a failure.
    /// </summary>
    Failure,

    /// <summary>
    /// The reportable was a performance which was a failure and has an associated error report.
    /// </summary>
    FailureWithError,

    /// <summary>
    /// The reportable was the gaining of an ability.
    /// </summary>
    GainAbility,
  }
}
