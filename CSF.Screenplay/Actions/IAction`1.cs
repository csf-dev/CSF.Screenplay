using System;
namespace CSF.Screenplay.Actions
{
  public interface IAction<TResult> : IActionWithResult
  {
    new TResult Execute();
  }
}
