using System;
using CSF.Screenplay.Abilities;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Event arguments for an actor gaining an ability.
  /// </summary>
  public class GainAbilityEventArgs : EventArgs
  {
    /// <summary>
    /// Gets the ability which was added to the actor.
    /// </summary>
    /// <value>The ability.</value>
    public IAbility Ability { get; private set; }

    /// <summary>
    /// Gets the actor which gained the ability
    /// </summary>
    /// <value>The actor.</value>
    public INamed Actor { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GainAbilityEventArgs"/> class.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="ability">The ability.</param>
    public GainAbilityEventArgs(INamed actor, IAbility ability)
    {
      if(ability == null)
        throw new ArgumentNullException(nameof(ability));
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));

      Ability = ability;
      Actor = actor;
    }
  }
}
