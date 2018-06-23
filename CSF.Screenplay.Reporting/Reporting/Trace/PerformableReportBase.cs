using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Reporting.Trace
{
  /// <summary>
  /// Base type for a report upon an actor's performance.
  /// </summary>
  public abstract class PerformableReportBase
  {
    /// <summary>
    /// Gets the performable item which is being reported-upon.
    /// </summary>
    /// <value>The performable.</value>
    public IPerformable Performable { get; private set; }

    /// <summary>
    /// Gets the actor which is performing the item.
    /// </summary>
    /// <value>The actor.</value>
    public INamed Actor { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PerformableReportBase"/> class.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable.</param>
    public PerformableReportBase(INamed actor, IPerformable performable)
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
