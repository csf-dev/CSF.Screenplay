public interface IHasAbilities
{
  IHasAbilities IsAbleTo<TAbility>(TAblity ability);

  IHasAbilities IsAbleTo<TAbility>(Lazy<TAblity> ability);

  IHasAbilities IsAbleTo<TAbility>(Func<TAblity> ability);

  IHasAbilities IsAbleTo<TAbility>();

  IHasAbilities IsAbleTo(Type abilityType, IAbility ability);

  IHasAbilities IsAbleTo(Type abilityType, Lazy<IAbility> ability);

  IHasAbilities IsAbleTo(Type abilityType, Func<IAbility> ability);

  IHasAbilities IsAbleTo(Type abilityType);
}