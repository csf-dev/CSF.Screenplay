using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Actions
{
  public class ActionExecuterWithResult<TAction>
    : ActionExecutorBase<TAction>, IActionExecutorWithResult<TAction>
    where TAction : class,IActionWithResult
  {
    public ActionExecuterWithResult<TAction> With(System.Action<TAction> configuration)
    {
      AddConfiguration(configuration);
      return this;
    }

    object IActionExecutorWithResult<TAction>.Execute(ICanPerformActions performer)
    {
      var action = GetAction(performer);
      return action.Execute();
    }
  }
}
