using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Actions
{
  public abstract class Action<TParams,TResult> : IAction<TParams,TResult>
  {
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
