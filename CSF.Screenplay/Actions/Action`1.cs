using System;
namespace CSF.Screenplay.Actions
{
  public abstract class Action<TParams,TResult> : IAction<TParams,TResult>
  {
    protected abstract TResult Execute(TParams parameters);

    TResult IAction<TParams,TResult>.Execute(TParams parameters)
    {
      return Execute(parameters);
    }

    object IActionWithResult<TParams>.Execute(TParams parameters)
    {
      return Execute(parameters);
    }
  }
}
