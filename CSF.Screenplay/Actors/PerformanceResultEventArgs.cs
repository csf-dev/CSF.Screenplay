using System;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Event arguments relating to retrieving a result value from a task, action or question.
  /// </summary>
  public class PerformanceResultEventArgs : PerformanceEventArgsBase
  {
    /// <summary>
    /// Gets an exception encountered whilst attempting to perform the task, action or question.
    /// </summary>
    /// <value>The exception.</value>
    public object Result { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PerformanceResultEventArgs"/> class.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable.</param>
    /// <param name="result">The result retrieved from the performable.</param>
    public PerformanceResultEventArgs(INamed actor, IPerformable performable, object result)
      : base(actor, performable)
    {
      Result = result;
    }
  }
}
