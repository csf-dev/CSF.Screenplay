using System;
namespace CSF.Screenplay.Actions
{
  public abstract class Action<TResult> : IAction<TResult>
  {
    protected abstract TResult Execute();

    TResult IAction<TResult>.Execute()
    {
      return Execute();
    }

    object IActionWithResult.Execute()
    {
      return Execute();
    }
  }
}
