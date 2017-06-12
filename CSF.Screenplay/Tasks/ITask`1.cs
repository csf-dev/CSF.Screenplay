using System;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Tasks
{
  public interface ITask<TResult> : ITaskWithResult
  {
    new TResult PerformAs(IPerformer actor);
  }
}
