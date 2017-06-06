using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay
{
  public class ActionProvider : ICanPerformActions
  {
    readonly IEnumerable<IAbility> abilities;

    public TAction GetAction<TAction>() where TAction : class
    {
      var ability = abilities.FirstOrDefault(x => x.CanProvideAction<TAction>());
      if(ability == null)
        return null;

      return ability.GetAction<TAction>();
    }

    public IEnumerable<Type> GetAllActionTypes()
    {
      return abilities
        .SelectMany(x => x.GetActionTypes())
        .Distinct()
        .ToArray();
    }

    internal ActionProvider(IEnumerable<IAbility> abilities)
    {
      if(abilities == null)
        throw new ArgumentNullException(nameof(abilities));

      this.abilities = abilities;
    }
  }
}
