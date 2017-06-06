public interface IHasAbilities
{
  IActor IsAbleTo<TAbility>(TAblity ability);

  IActor IsAbleTo<TAbility>(Lazy<TAblity> ability);

  IActor IsAbleTo<TAbility>(Func<TAblity> ability);

  IActor IsAbleTo<TAbility>();

  IActor IsAbleTo(Type abilityType, IAbility ability);

  IActor IsAbleTo(Type abilityType, Lazy<IAbility> ability);

  IActor IsAbleTo(Type abilityType, Func<IAbility> ability);

  IActor IsAbleTo(Type abilityType);
}