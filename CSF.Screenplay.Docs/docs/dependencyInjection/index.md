---
uid: DependencyInjectionMainArticle
---

# Dependency injection

The Screenplay design pattern is fundamentally compatible with and based upon [dependency injection], aka DI.
You may add Screenplay to an existing container of you wish, via the [`AddScreenplay`] extension method.
Alternatively you may create an instance of [`Screenplay`] which uses its own self-contained DI container with the `static` [`Screenplay.Create`] helper method.

[dependency injection]: https://en.wikipedia.org/wiki/Dependency_injection
[`AddScreenplay`]: TODO
[`Screenplay`]: TODO
[`Screenplay.Create`]: TODO

---

TODO: Everything below this line needs rewrite


Screenplay introduces a form of simple DI of its own, though; that is the concept of **[Abilities]**.

As you can see on [the diagram of how Actors, Abilities and Performables relate to one another], 

Only _a few object types_ should be constructor-injected as dependencies, though.

TODO: Write this docco

* [Injecting services](InjectingServices.md)


[Abilities]: ../../glossary/Ability.md
[the diagram of how Actors, Abilities and Performables relate to one another]: ../MakeupOfAScreenplay.md