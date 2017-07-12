using System;
namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Represents event arguments which include reference to an actor.
  /// </summary>
  public class ActorEventArgs : EventArgs
  {
    /// <summary>
    /// Gets the actor.
    /// </summary>
    /// <value>The actor.</value>
    public INamed Actor { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Actors.ActorEventArgs"/> class.
    /// </summary>
    /// <param name="actor">Actor.</param>
    public ActorEventArgs(INamed actor)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));

      Actor = actor;
    }
  }
}
