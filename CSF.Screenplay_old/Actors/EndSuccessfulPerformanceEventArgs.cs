using System;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Event arguments relating to the successful completion of the performance of an action, task or question.
  /// </summary>
  public class EndSuccessfulPerformanceEventArgs : PerformanceEventArgsBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="EndSuccessfulPerformanceEventArgs"/> class.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable.</param>
    public EndSuccessfulPerformanceEventArgs(IActor actor, IPerformable performable) : base(actor, performable) {}
  }
}
