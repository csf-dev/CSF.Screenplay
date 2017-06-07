using System;
using CSF.Screenplay.Actions;

namespace CSF.Screenplay.Actors
{
  public interface IPerformer
  {
    void AttemptsTo<TAction>(IActionExecutor<TAction> executor)
      where TAction : IAction;

    TResult AttemptsTo<TAction,TResult>(IActionExecutorWithResult<TAction> executor)
      where TAction : IAction<TResult>;
  }
}
