using System;
namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Marker interface for all of the capabilities of an actor.
  /// </summary>
  public interface IActor : IGivenActor, IWhenActor, IThenActor, ICanReceiveAbilities, IDisposable
  {
    /// <summary>
    /// Gets the name of the current actor.
    /// </summary>
    /// <value>The name.</value>
    string Name { get; }
  }
}
