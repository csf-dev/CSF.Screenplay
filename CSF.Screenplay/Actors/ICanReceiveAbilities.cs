using System;
using CSF.Screenplay.Abilities;

namespace CSF.Screenplay.Actors
{
  public interface ICanReceiveAbilities
  {
    void IsAbleTo<TAbility>() where TAbility : IAbility,new();

    void IsAbleTo(IAbility ability);

    void IsAbleTo(Type abilityType);
  }
}