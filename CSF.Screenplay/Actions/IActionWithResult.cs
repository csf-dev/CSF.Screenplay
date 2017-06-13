using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Actions
{
  public interface IActionWithResult<TParams>
  {
    object Execute(IPerformer performer, TParams parameters);
  }
}
