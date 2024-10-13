# How Performables get their dependencies

Importantly, [Performable] types _do not participate in dependency injection from the container_.
This means that dependency services _cannot be constructor-injected_ into performable types.

## Performables' state represents parameters

Performables are [created using the builder pattern] and do not hold references to any dependency services.
Instead, the state of the performable represents the _'parameters'_ for that performable.
This state is typically set via constructor parameters or public settable properties.

### An example

For example, a [Task] which adds an item to the user's shopping basket might accept a constructor parameter representing the unique ID of the product to be added to the basket.
That unique ID would be stored within the performable instance, in a field or property for example.
When the relevant `ExecuteAsync` method is executed, the unique ID stored in the performable instance is used to conduct whatever logic is appropriate.

## Use abilities to get dependencies

Performables, specifcally [Actions] and [Questions], should access their dependencies via the Actor's **[Abilities]** by using the [`GetAbility<T>`] method or similar.
In this sense, Abilities are a form of dependency injection or service locator (for a specific use case).
Whilst service locators are usually considered an anti-pattern, the benefits in this specific case outweigh the disadvantages.
The primary benefit is [the ability to create performables from static builders].

Importantly, _**[Tasks]** should never make use of [Abilities]_.
Only Actions and Questions should use Abilities.

[Performable]: ../../glossary/Performable.md
[created using the builder pattern]: ../builderPattern/index.md
[Task]: ../../glossary/Task.md
[Actions]: ../../glossary/Action.md
[Questions]: ../../glossary/Question.md
[`GetAbility<T>`]: xref:CSF.Screenplay.ActorExtensions.GetAbility``1(CSF.Screenplay.ICanPerform)
[the ability to create performables from static builders]: ../builderPattern/index.md
[Tasks]: ../../glossary/Task.md
