using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Actions
{
  public abstract class Action<TParams> : IAction<TParams>
  {
    protected abstract void Execute(IPerformer performer, TParams parameters);

    void IAction<TParams>.Execute(IPerformer performer, TParams parameters)
    {
      Execute(performer, parameters);
    }
  }
}
