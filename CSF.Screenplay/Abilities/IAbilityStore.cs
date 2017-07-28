using System;
namespace CSF.Screenplay.Abilities
{
  /// <summary>
  /// Container type responsible for organising the abilities owned by an <see cref="Actors.IActor"/>.
  /// </summary>
  public interface IAbilityStore : IDisposable
  {
    /// <summary>
    /// Determines whether or not the current instance contains an ability or not.
    /// </summary>
    /// <returns><c>true</c>, if the current instance has the ability, <c>false</c> otherwise.</returns>
    /// <typeparam name="TAbility">The desired ability type.</typeparam>
    bool HasAbility<TAbility>() where TAbility : IAbility;

    /// <summary>
    /// Gets an ability of the noted type.
    /// </summary>
    /// <returns>The ability.</returns>
    /// <typeparam name="TAbility">The desired ability type.</typeparam>
    TAbility GetAbility<TAbility>() where TAbility : IAbility;

    /// <summary>
    /// Adds an ability to the current instance.
    /// </summary>
    /// <param name="ability">The ability.</param>
    void Add(IAbility ability);

    /// <summary>
    /// Instantiates an ability of the given type, adds it to the current store instance and returns the created ability.
    /// </summary>
    /// <param name="abilityType">The ability type.</param>
    IAbility Add(Type abilityType);
  }
}
