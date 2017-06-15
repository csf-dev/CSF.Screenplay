using System;
namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// An actor is a persona who may participate in a testing scenario.
  /// Actors are given <see cref="Abilities.IAbility"/> abilities.  These abilities allow them to make use of
  /// <see cref="Performables.IPerformable"/> items, which may be tasks, actions or questions.
  /// </summary>
  public interface IActor : INamed, IPerformer, IGivenActor, IThenActor, IWhenActor, ICanReceiveAbilities, IDisposable
  {
  }
}
