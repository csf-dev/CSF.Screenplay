using System;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Reporting.Trace
{
  /// <summary>
  /// Represents a report upon an actor gaining an ability.
  /// </summary>
  public class AbilityReport
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
    /// Returns a <c>System.String</c> that represents the current <see cref="AbilityReport"/>.
    /// </summary>
    /// <returns>A <c>System.String</c> that represents the current <see cref="AbilityReport"/>.</returns>
    public override string ToString()
    {
      return $"ABILITY: {Ability.GetReport(Actor)}";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AbilityReport"/> class.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="ability">The ability.</param>
    public AbilityReport(INamed actor, IAbility ability)
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
