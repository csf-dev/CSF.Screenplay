using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Marks the moment at which an actor finishes performing a performable item.
  /// </summary>
  public interface IPerformanceEnd
  {
    /// <summary>
    /// Gets the performable.
    /// </summary>
    /// <value>The performable.</value>
    IPerformable Performable { get; }

    /// <summary>
    /// Gets the actor which is performing the <see cref="IPerformanceEnd.Performable"/>.
    /// </summary>
    /// <value>The actor.</value>
    INamed Actor { get; }
  }
}
