using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Actions
{
  /// <summary>
  /// Represents an action which may be performed by an actor. This action returns a result/response value.
  /// </summary>
  public interface IActionWithResult<TParams>
  {
    /// <summary>
    /// Executes the current action instance, in the context of a given performer, using the provided parameters.
    /// </summary>
    /// <returns>A result object.</returns>
    /// <param name="performer">The performer (actor) performing this action.</param>
    /// <param name="parameters">Parameters for the current action.</param>
    object Execute(IPerformer performer, TParams parameters);
  }
}
