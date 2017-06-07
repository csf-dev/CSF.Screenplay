using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Actions
{
  public class ActionExecuter<TAction>
    : ActionExecutorBase<TAction>, IActionExecutor<TAction>
    where TAction : class,IAction
  {
    public ActionExecuter<TAction> With(System.Action<TAction> configuration)
    {
      AddConfiguration(configuration);
      return this;
    }

    void IActionExecutor<TAction>.Execute(ICanPerformActions performer)
    {
      var action = GetAction(performer);
      action.Execute();
    }
  }
}
