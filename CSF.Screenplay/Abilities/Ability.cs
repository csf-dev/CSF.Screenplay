using System;
using System.Collections.Generic;

namespace CSF.Screenplay.Abilities
{
  public abstract class Ability : IAbility
  {
    public abstract void RegisterActions();

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
  }
}
