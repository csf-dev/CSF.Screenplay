using System;
using System.Collections.Generic;

namespace CSF.Screenplay
{
  public abstract class Ability : IAbility
  {
    protected abstract void Register<TAction>(Func<TAction> creator);

    public abstract bool CanProvideAction(Type actionType);

    public virtual bool CanProvideAction<TAction>()
    {
      return CanProvideAction(typeof(TAction));
    }

    public abstract object GetAction(Type actionType);

    public abstract IEnumerable<Type> GetActionTypes();

    public virtual TAction GetAction<TAction>()
    {
      return (TAction) GetAction(typeof(TAction));
    }
  }
}
