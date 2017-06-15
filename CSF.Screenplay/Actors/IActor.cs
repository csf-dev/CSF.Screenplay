using System;
namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Marker interface for all of the capabilities of an actor.
  /// </summary>
  public interface IActor : INamed, IPerformer, IGivenActor, IThenActor, IWhenActor, ICanReceiveAbilities, IDisposable
  {
  }
}
