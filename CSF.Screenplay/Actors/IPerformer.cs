using System;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Represents an actor's ability to perform actions.
  /// </summary>
  public interface IPerformer : INamed
  {
    /// <summary>
    /// Determines whether or not the given performer has an ability or not.
    /// </summary>
    /// <returns><c>true</c>, if the performer has the ability, <c>false</c> otherwise.</returns>
    /// <typeparam name="TAbility">The desired ability type.</typeparam>
    bool HasAbility<TAbility>() where TAbility : IAbility;

    /// <summary>
    /// Gets an ability of the noted type.
    /// </summary>
    /// <returns>The ability.</returns>
    /// <typeparam name="TAbility">The desired ability type.</typeparam>
    TAbility GetAbility<TAbility>() where TAbility : IAbility;

    /// <summary>
    /// Performs an action or task.
    /// </summary>
    /// <param name="performable">The performable item to execute.</param>
    void Perform(IPerformable performable);

    /// <summary>
    /// Performs an action or task which has a public parameterless constructor.
    /// </summary>
    /// <typeparam name="TPerformable">The type of the performable item to execute.</typeparam>
    void Perform<TPerformable>() where TPerformable : IPerformable,new();

    /// <summary>
    /// Performs an action, task or asks a question which returns a result value.
    /// </summary>
    /// <returns>The result of performing the item</returns>
    /// <param name="performable">The performable item to execute.</param>
    /// <typeparam name="TResult">The result type.</typeparam>
    TResult Perform<TResult>(IPerformable<TResult> performable);
  }
}
