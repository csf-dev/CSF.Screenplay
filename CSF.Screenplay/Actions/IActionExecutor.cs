using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Actions
{
  public interface IActionExecutor<TAction> where TAction : IAction
  {
    void Execute(ICanPerformActions performer);
  }
}
