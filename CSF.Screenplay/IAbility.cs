using System;
using System.Collections.Generic;

namespace CSF.Screenplay
{
  public interface IAbility
  {
    void RegisterActions();

    bool CanProvideAction<TAction>();

    bool CanProvideAction(Type actionType);

    IEnumerable<Type> GetActionTypes();

    TAction GetAction<TAction>();

    object GetAction(Type actionType);
  }
}