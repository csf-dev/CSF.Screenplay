using System;
using System.Collections.Generic;

namespace CSF.Screenplay.Abilities
{
  /// <summary>
  /// An ability which can be held by an <see cref="Actors.ICanReceiveAbilities"/>.  This type may provide one or more
  /// actions, in the form of the factory methods <c>GetAction</c>.
  /// </summary>
  public interface IAbility : IDisposable
  {
    /// <summary>
    /// Indicates whether or not the current ability instance is able to create actions of the indicated type.
    /// </summary>
    /// <returns><c>true</c>, if the indicates action type may be created by this ability, <c>false</c> otherwise.</returns>
    /// <typeparam name="TAction">The desired action type.</typeparam>
    bool CanProvideAction<TAction>();

    /// <summary>
    /// Indicates whether or not the current ability instance is able to create actions of the indicated type.
    /// </summary>
    /// <returns><c>true</c>, if the indicates action type may be created by this ability, <c>false</c> otherwise.</returns>
    /// <param name="actionType">The desired action type.</param>
    bool CanProvideAction(Type actionType);

    /// <summary>
    /// Gets a collection indicating all of the <c>System.Type</c> of actions which may be created by the current
    /// ability instance.
    /// </summary>
    /// <returns>The action types.</returns>
    IEnumerable<Type> GetActionTypes();

    /// <summary>
    /// Gets and returns an action instance of the desired type.
    /// </summary>
    /// <returns>An action instance.</returns>
    /// <typeparam name="TAction">The desired action type.</typeparam>
    TAction GetAction<TAction>();

    /// <summary>
    /// Gets and returns an action instance of the desired type.
    /// </summary>
    /// <returns>An action instance.</returns>
    /// <param name="actionType">The desired action type.</param>
    object GetAction(Type actionType);
  }
}