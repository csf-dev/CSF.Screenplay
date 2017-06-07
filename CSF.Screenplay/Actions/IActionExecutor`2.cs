using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Actions
{
  public interface IActionExecutor<TAction,TResult> where TAction : IAction<TResult>
  {
    TResult Execute(ICanPerformActions performer);
  }
}
