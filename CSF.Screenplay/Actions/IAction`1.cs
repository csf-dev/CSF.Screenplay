using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Actions
{
  public interface IAction<TParams>
  {
    void Execute(IPerformer performer, TParams parameters);
  }
}
