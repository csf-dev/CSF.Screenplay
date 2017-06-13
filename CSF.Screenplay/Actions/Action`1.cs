using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Actions
{
  /// <summary>
  /// Base type for implementations of <see cref="T:IAction{TParams}"/>.
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
  public abstract class Action<TParams> : IAction<TParams>
  {
    /// <summary>
    /// Executes the current action instance, in the context of a given performer, using the provided parameters.
    /// </summary>
    /// <param name="performer">The performer (actor) performing this action.</param>
    /// <param name="parameters">Parameters for the current action.</param>
    protected abstract void Execute(IPerformer performer, TParams parameters);

    void IAction<TParams>.Execute(IPerformer performer, TParams parameters)
    {
      Execute(performer, parameters);
    }
  }
}
