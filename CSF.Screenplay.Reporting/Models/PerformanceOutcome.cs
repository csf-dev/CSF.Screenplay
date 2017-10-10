using System;
namespace CSF.Screenplay.Reporting.Models
{
  /// <summary>
  /// Enumerates the possible outcomes of a performance.
  /// </summary>
  public enum PerformanceOutcome
  {
    /// <summary>
    /// The performance was a success.
    /// </summary>
    Success,

    /// <summary>
    /// The performance was a success and yielded a result.
    /// </summary>
    SuccessWithResult,

    /// <summary>
    /// The performance was a failure.
    /// </summary>
    Failure,

    /// <summary>
    /// The performance was a failure and has an associated exception.
    /// </summary>
    FailureWithException
  }
}
