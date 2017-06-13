using System;
using System.Collections.Generic;

namespace CSF.Screenplay.Abilities
{
  public abstract class Ability : IAbility, IDisposable
  {
    public virtual bool CanProvideAction<TAction>()
    {
      return CanProvideAction(typeof(TAction));
    }

    public virtual TAction GetAction<TAction>()
    {
      return (TAction) GetAction(typeof(TAction));
    }

    public abstract bool CanProvideAction(Type actionType);

    public abstract object GetAction(Type actionType);

    public abstract IEnumerable<Type> GetActionTypes();

    #region IDisposable Support

    bool disposed;

    protected bool Disposed => disposed;

    protected virtual void Dispose(bool disposing)
    {
      if(!disposed)
      {
        disposed = true;
      }
    }

    ~Ability()
    {
      Dispose(false);
    }

    void IDisposable.Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    #endregion
  }
}
