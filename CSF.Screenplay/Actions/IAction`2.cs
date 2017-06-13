using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Actions
{
  public interface IAction<TParams,TResult> : IActionWithResult<TParams>
  {
    new TResult Execute(IPerformer performer, TParams parameters);
  }
}
