using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Tasks
{
  public interface ITaskWithResult
  {
    object PerformAs(IPerformer actor);
  }
}
