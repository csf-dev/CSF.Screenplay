using System;
using CSF.Screenplay.Abilities;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Represents an actor's ability to receive new <see cref="IAbility"/> instances.
  /// </summary>
  public interface ICanReceiveAbilities
  {
    /// <summary>
    /// Adds an ability of the indicated type to the current instance.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This overload requires that the ability type has a public parameterless constructor.
    /// An instance of the ability type is created/instantiated as part of this operation.
    /// </para>
    /// </remarks>
    /// <typeparam name="TAbility">The desired ability type.</typeparam>
    void IsAbleTo<TAbility>() where TAbility : IAbility,new();

    /// <summary>
    /// Adds an ability object to the current instance.
    /// </summary>
    /// <param name="ability">The ability.</param>
    void IsAbleTo(IAbility ability);

    /// <summary>
    /// Adds an ability of the indicated type to the current instance.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This overload requires that the ability type has a public parameterless constructor.
    /// An instance of the ability type is created/instantiated as part of this operation.
    /// </para>
    /// </remarks>
    /// <param name="abilityType">The desired ability type.</param>
    void IsAbleTo(Type abilityType);
  }
}