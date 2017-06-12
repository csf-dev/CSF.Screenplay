using System;
using CSF.Screenplay.Actions;

namespace CSF.Screenplay.Actors
{
  public interface IPerformer
  {
    bool SupportsActionType<TAction>();

    TAction GetAction<TAction>() where TAction : class;

    void Perform<TParams>(IAction<TParams> action, TParams parameters);

    object Perform<TParams>(IActionWithResult<TParams> action, TParams parameters);

    TResult Perform<TParams,TResult>(IAction<TParams,TResult> action, TParams parameters);
  }
}
