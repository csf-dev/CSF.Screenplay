using System;
using CSF.Screenplay.Actions;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Represents an actor's ability to perform actions.
  /// </summary>
  public interface IPerformer
  {
    /// <summary>
    /// Gets the name of the actor.
    /// </summary>
    /// <value>The name.</value>
    string Name { get; }

    /// <summary>
    /// Gets a value which determines whether or not the current instance can perform actions of the indicated
    /// action type.
    /// </summary>
    /// <returns><c>true</c>, if action type is supported, <c>false</c> otherwise.</returns>
    /// <typeparam name="TAction">The desired action type.</typeparam>
    bool SupportsActionType<TAction>();

    /// <summary>
    /// Creates and returns an instance of the indicated action type, using the actors abilities.
    /// </summary>
    /// <returns>The action instance.</returns>
    /// <typeparam name="TAction">The desired action type.</typeparam>
    TAction GetAction<TAction>() where TAction : class;

    /// <summary>
    /// Performs an action, using the given parameters.
    /// </summary>
    /// <param name="action">The action instance to execute.</param>
    /// <param name="parameters">The parameters for the action.</param>
    /// <typeparam name="TParams">The action parameters type.</typeparam>
    void Perform<TParams>(IAction<TParams> action, TParams parameters);

    /// <summary>
    /// Performs an action, using the given parameters, getting a result value.
    /// </summary>
    /// <returns>The result of performing the action</returns>
    /// <param name="action">The action instance to execute.</param>
    /// <param name="parameters">The parameters for the action.</param>
    /// <typeparam name="TParams">The action parameters type.</typeparam>
    object Perform<TParams>(IActionWithResult<TParams> action, TParams parameters);

    /// <summary>
    /// Performs an action, using the given parameters, getting a result value.
    /// </summary>
    /// <returns>The result of performing the action</returns>
    /// <param name="action">The action instance to execute.</param>
    /// <param name="parameters">The parameters for the action.</param>
    /// <typeparam name="TParams">The action parameters type.</typeparam>
    /// <typeparam name="TResult">The action result type.</typeparam>
    TResult Perform<TParams,TResult>(IAction<TParams,TResult> action, TParams parameters);
  }
}
