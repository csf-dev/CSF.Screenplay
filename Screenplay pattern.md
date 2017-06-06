# Screenplay pattern
## Actor
Actors expose abilities by type. This could be a static registration of ability types to their implementation instances, but it could also be injected.

Interface for an actor might be:

```csharp
public interface IActor
{
  void WasAbleTo(ITask task);

  void AttemptsTo(ITask task);

  void Should(ITask task);

  void Should(IExpectation expectation);
}

public abstract class Actor : IActor
{
  void IActor.WasAbleTo(ITask task)
  {
    Perform(task);
  }

  void IActor.AttemptsTo(ITask task)
  {
    Perform(task);
  }

  void IActor.Should(ITask task)
  {
    Perform(task);
  }

  void IActor Should(IExpectation expectation)
  {
    Verify(expectation);
  }

  protected abstract ICanPerformActions GetActionProvider();

  protected virtual void Perform(ITask task)
  {
    var provider = GetActionProvider();
    task.Execute(provider);
  }
}

public interface IAbilityRegistrar
{
  // All of the following methods are used to add abilities to the actor
  // Each represents a different strategy depending on whether we are using DI etc
  IActor IsAbleTo<TAbility>(TAblity ability);

  IActor IsAbleTo<TAbility>(Lazy<TAblity> ability);

  IActor IsAbleTo<TAbility>(Func<TAblity> ability);

  IActor IsAbleTo<TAbility>();

  IActor IsAbleTo(Type abilityType, IAbility ability);

  IActor IsAbleTo(Type abilityType, Lazy<IAbility> ability);

  IActor IsAbleTo(Type abilityType, Func<IAbility> ability);

  IActor IsAbleTo(Type abilityType);
}

public interface ICanPerformActions
{
  IEnumerable<Type> GetAllActionTypes();

  TAction GetAction<TAction>();
}
```

## Tasks
Tasks are types which 'drive' the actor and make them use their abilities. They are little more than configurations which Push the actions along.

```csharp
public interface ITask
{
  void Execute(IAbilityProvider actor);
}
```

An expectation is a specialisation of a task which asserts that the answer to a question satisfies a predicate.

## Overall
This is how we want to use this:

```csharp
Given(myActor).WasAbleTo(setupTask);

When(myActor).AttemptsTo(actionTask);

Then(myActor).Should(assertionTask);
```
