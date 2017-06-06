using System;

namespace CSF.Screenplay
{
  public interface ICanReceiveAbilities
  {
    ICanReceiveAbilities IsAbleTo<TAbility>(TAbility ability);

    ICanReceiveAbilities IsAbleTo<TAbility>(Lazy<TAbility> ability);

    ICanReceiveAbilities IsAbleTo<TAbility>(Func<TAbility> ability);

    ICanReceiveAbilities IsAbleTo<TAbility>();

    ICanReceiveAbilities IsAbleTo(Type abilityType, IAbility ability);

    ICanReceiveAbilities IsAbleTo(Type abilityType, Lazy<IAbility> ability);

    ICanReceiveAbilities IsAbleTo(Type abilityType, Func<IAbility> ability);

    ICanReceiveAbilities IsAbleTo(Type abilityType);
  }
}