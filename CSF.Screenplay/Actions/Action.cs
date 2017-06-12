using System;
namespace CSF.Screenplay.Actions
{
  public abstract class Action<TParams> : IAction<TParams>
  {
    protected abstract void Execute(TParams parameters);

    void IAction<TParams>.Execute(TParams parameters)
    {
      Execute(parameters);
    }
  }
}
