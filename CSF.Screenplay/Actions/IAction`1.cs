using System;
namespace CSF.Screenplay.Actions
{
  public interface IAction<TParams,TResult> : IActionWithResult<TParams>
  {
    new TResult Execute(TParams parameters);
  }
}
