using System;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Event arguments relating to the beginning of the performance of an action, task or question.
  /// </summary>
  public class BeginPerformanceEventArgs : PerformanceEventArgsBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="BeginPerformanceEventArgs"/> class.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable.</param>
    public BeginPerformanceEventArgs(INamed actor, IPerformable performable) : base(actor, performable) {}
  }
}
