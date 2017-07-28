using System;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Event arguments relating to the failure to execute a task, action or question, where an exception was encountered.
  /// </summary>
  public class PerformanceFailureEventArgs : PerformanceEventArgsBase
  {
    /// <summary>
    /// Gets an exception encountered whilst attempting to perform the task, action or question.
    /// </summary>
    /// <value>The exception.</value>
    public Exception Exception { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PerformanceFailureEventArgs"/> class.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable.</param>
    /// <param name="exception">The exception.</param>
    public PerformanceFailureEventArgs(INamed actor, IPerformable performable, Exception exception)
      : base(actor, performable)
    {
      if(exception == null)
        throw new ArgumentNullException(nameof(exception));

      Exception = exception;
    }
  }
}
