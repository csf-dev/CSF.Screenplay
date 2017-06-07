using System;
using CSF.Screenplay.Actions;

namespace CSF.Screenplay
{
  public static class ActionComposer
  {
    public static ActionExecuter<TAction> Perform<TAction>() where TAction : class,IAction
    {
      return new ActionExecuter<TAction>();
    }

    public static ActionExecuterWithResult<TAction> PerformAndGetResult<TAction>() where TAction : class,IActionWithResult
    {
      return new ActionExecuterWithResult<TAction>();
    }
  }
}
