using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Actions
{
  public interface IActionExecutorWithResult<TAction> where TAction : IActionWithResult
  {
    object Execute(ICanPerformActions performer);
  }
}
