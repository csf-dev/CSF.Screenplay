using System;
using CSF.Screenplay.Actions;

namespace CSF.Screenplay
{
  public static class ActionComposer
  {
    public static IActionExecutor<TAction> Perform<TAction>() where TAction : class,IAction
    {
      // TODO: Write this implementation
      throw new NotImplementedException();
    }
  }
}
