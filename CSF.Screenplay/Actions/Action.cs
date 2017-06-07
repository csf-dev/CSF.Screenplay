using System;
namespace CSF.Screenplay.Actions
{
  public abstract class Action : IAction
  {
    protected abstract void Execute();

    void IAction.Execute()
    {
      Execute();
    }
  }
}
