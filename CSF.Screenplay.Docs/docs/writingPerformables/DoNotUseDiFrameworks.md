# Do not resolve performables from DI

[Dependency Injection] using DI frameworks is usually a recommended best practice in modern software.
Screenplay recommends the resolution of many services from DI, including [Personas], the [Cast], [Stage] and [Abilities].

It is not a good idea to resolve [Performable] object instances from DI, though.
Because performables [should be immutable] but stateful, this requires all of their state to be set at the time of their construction, such as via constructor parameters or properties with `init` setters.
Unfortunately, this state represents the configuration of how the performable will be used, so it will be entirely down to the particular usage of the performable.
This would make the use of DI quite cumbersome, as many arbitrary parameter values must be passed to the resolution.

What's more, performables _should not depend upon anything which is resolved from DI_.
At most, [Actions] and [Questions] may depend upon an [Actor]'s [Abilities] but they should access these from the first parameter of the `PerformAsAsync` method; the [`ICanPerform`] parameter (the actor).

Instead, it is far better to [use the builder pattern] to create instances of performables.

[Dependency Injection]: https://en.wikipedia.org/wiki/Dependency_injection
[Personas]: ../../glossary/Persona.md
[Cast]: xref:CSF.Screenplay.ICast
[Stage]: xref:CSF.Screenplay.IStage
[Abilities]: ../../glossary/Ability.md
[Performable]: ../../glossary/Performable.md
[should be immutable]: StatefulButImmutable.md
[Actions]: ../../glossary/Action.md
[Questions]: ../../glossary/Question.md
[Actor]: xref:CSF.Screenplay.Actor
[`ICanPerform`]: xref:CSF.Screenplay.ICanPerform
[use the builder pattern]: ../builderPattern/index.md
