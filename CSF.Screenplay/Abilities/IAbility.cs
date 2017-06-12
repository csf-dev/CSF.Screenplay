using System;
using System.Collections.Generic;

namespace CSF.Screenplay.Abilities
{
  public interface IAbility
  {
    /// <summary>
    /// Initialise the ability type, preparing it to supply actions.
    /// </summary>
    void Init();

    bool CanProvideAction<TAction>();

    bool CanProvideAction(Type actionType);

    IEnumerable<Type> GetActionTypes();

    TAction GetAction<TAction>();

    object GetAction(Type actionType);
  }
}