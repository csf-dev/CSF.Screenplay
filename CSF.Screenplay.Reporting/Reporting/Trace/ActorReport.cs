using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Reporting.Trace
{
  /// <summary>
  /// Represents a trace report relating to an actor.
  /// </summary>
  public class ActorReport
  {
    /// <summary>
    /// Gets the actor which is performing the item.
    /// </summary>
    /// <value>The actor.</value>
    public INamed Actor { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Trace.ActorReport"/> class.
    /// </summary>
    /// <param name="actor">Actor.</param>
    public ActorReport(INamed actor)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));

      Actor = actor;
    }
  }
}
