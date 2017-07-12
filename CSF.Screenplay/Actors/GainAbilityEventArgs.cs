using System;
using CSF.Screenplay.Abilities;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Event arguments for an actor gaining an ability.
  /// </summary>
  public class GainAbilityEventArgs : ActorEventArgs
  {
    /// <summary>
    /// Gets the ability which was added to the actor.
    /// </summary>
    /// <value>The ability.</value>
    public IAbility Ability { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GainAbilityEventArgs"/> class.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="ability">The ability.</param>
    public GainAbilityEventArgs(INamed actor, IAbility ability) : base(actor)
    {
      if(ability == null)
        throw new ArgumentNullException(nameof(ability));

      Ability = ability;
    }
  }
}
