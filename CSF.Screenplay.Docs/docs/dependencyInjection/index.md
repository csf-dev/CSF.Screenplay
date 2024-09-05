---
uid: DependencyInjectionMainArticle
---

# Dependency injection

The Screenplay design pattern is fundamentally compatible with [dependency injection], aka DI.
Screenplay introduces a form of Simone DI of its own, though; that is the concept of **[Abilities]**.

As you can see on [the diagram of how Actors, Abilities and Performables relate to one another], 

Only _a few object types_ should be constructor-injected as dependencies, though.

TODO: Write this docco

* [Injecting services](InjectingServices.md)

[dependency injection]: https://en.wikipedia.org/wiki/Dependency_injection
[Abilities]: ../../glossary/Ability.md
[the diagram of how Actors, Abilities and Performables relate to one another]: ../MakeupOfAScreenplay.md