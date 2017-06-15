using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Represents a report that the performing of an item has failed, possibly due to an exception.
  /// </summary>
  public class FailureReport : PerformableReportBase
  {
    /// <summary>
    /// Gets the exception which is responsible for this failure, if applicable.
    /// </summary>
    /// <value>The exception.</value>
    public Exception Exception { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FailureReport"/> class.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable.</param>
    /// <param name="exception">The exception.</param>
    public FailureReport(INamed actor, IPerformable performable, Exception exception)
      : base(actor, performable)
    {
      Exception = exception;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FailureReport"/> class.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable.</param>
    public FailureReport(INamed actor, IPerformable performable) : this(actor, performable, null) {}
  }
}
