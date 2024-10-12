---
uid: AbilityGlossaryItem
---

# Ability

An ability is an arbitrary object which may be granted to **[Actors]** in order to allow them to interact with the application as part of a **[Performance]**.
Unlike many other Screenplay object-types, there is no particular interface which Abilities must implement, as their functionality, capabilities and very nature are specific to them alone.
That said - it is recommended for Abilities to implement [`ICanReport`] if possible, as this allows for the production of a more pleasant human-readable report.

**[Actions]** and/or **[Questions]** may interact directly with an actor's abilities in order to perform their functionality.
**[Tasks]**, on the other hand, should not.

[Actors]: xref:CSF.Screenplay.Actor
[Performance]: xref:CSF.Screenplay.IPerformance
[`ICanReport`]: xref:CSF.Screenplay.ICanReport
[Actions]: Action.md
[Questions]: Question.md
[Tasks]: Task.md

## The purpose of abilities

Abilities typically have one (or more) of the following purposes.

* They provide a [Gateway] or Façade to external functionality
* They provide access to _[dependency services]_
* They provide a model of data, such as:
  * Secrets/access tokens for an API to be consumed by abilities
  * Contextual per-[performance] state

Abilities are intended to be used by [Actions] and/or [Questions], not [Tasks].
Abilities provide the dependencies that those performables require, because [performables cannot participate in constructor-injected dependency injection].
Abilities can receive constructor-injected dependencies from DI, making them an ideal technique to provide dependencies to performables.

[Gateway]: https://martinfowler.com/articles/gateway-pattern.html
[dependency services]: ../docs/dependencyInjection/Performables.md
[performables cannot participate in constructor-injected dependency injection]: ../docs/dependencyInjection/Performables.md