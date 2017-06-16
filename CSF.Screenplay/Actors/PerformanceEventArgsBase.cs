using System;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Base type for event arguments relating to the performance of a task, action or question.
  /// </summary>
  public abstract class PerformanceEventArgsBase : EventArgs
  {
    /// <summary>
    /// Gets the performable item to which these event args relate.
    /// </summary>
    /// <value>The performable.</value>
    public IPerformable Performable { get; private set; }

    /// <summary>
    /// Gets the actor performing the <see cref="PerformanceEventArgsBase.Performable"/>.
    /// </summary>
    /// <value>The actor.</value>
    public INamed Actor { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PerformanceEventArgsBase"/> class.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable.</param>
    public PerformanceEventArgsBase(INamed actor, IPerformable performable)
    {
      if(performable == null)
        throw new ArgumentNullException(nameof(performable));
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));

      Performable = performable;
      Actor = actor;

    }
  }
}
