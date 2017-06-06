using System;

namespace CSF.Screenplay
{
  public interface ICanReceiveAbilities
  {
    void IsAbleTo<TAbility>() where TAbility : IAbility;

    void IsAbleTo(IAbility ability);

    void IsAbleTo(Type abilityType);
  }
}