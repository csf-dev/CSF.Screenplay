using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Actions
{
  /// <summary>
  /// Base type for implementations of <see cref="T:IAction{TParams,TResult}"/>.
  /// </summary>
  /// <remarks>
  /// <para>
  /// Implementors have one mandatory method to override and implement.
  /// </para>
  /// <para>
  /// Implementors of <see cref="T:IAction{TParams}"/> are not required to derive from this type, it is
  /// offered only as a convenience.
  /// </para>
  /// </remarks>
  public abstract class Action<TParams,TResult> : IAction<TParams,TResult>
  {
    /// <summary>
    /// Executes the current action instance, in the context of a given performer, using the provided parameters.
    /// </summary>
    /// <returns>A result of the corresponding generic type.</returns>
    /// <param name="performer">The performer (actor) performing this action.</param>
    /// <param name="parameters">Parameters for the current action.</param>
    protected abstract TResult Execute(IPerformer performer, TParams parameters);

    TResult IAction<TParams,TResult>.Execute(IPerformer performer, TParams parameters)
    {
      return Execute(performer, parameters);
    }

    object IActionWithResult<TParams>.Execute(IPerformer performer, TParams parameters)
    {
      return Execute(performer, parameters);
    }
  }
}
