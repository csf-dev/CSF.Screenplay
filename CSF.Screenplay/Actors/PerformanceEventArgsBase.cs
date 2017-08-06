using System;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Base type for event arguments relating to the performance of a task, action or question.
  /// </summary>
  public abstract class PerformanceEventArgsBase : ActorEventArgs
  {
    /// <summary>
    /// Gets the performable item to which these event args relate.
    /// </summary>
    /// <value>The performable.</value>
    public IPerformable Performable { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PerformanceEventArgsBase"/> class.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable.</param>
    public PerformanceEventArgsBase(IActor actor, IPerformable performable) : base(actor)
    {
      if(performable == null)
        throw new ArgumentNullException(nameof(performable));

      Performable = performable;
    }
  }
}
