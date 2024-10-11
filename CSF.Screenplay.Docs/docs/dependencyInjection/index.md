---
uid: DependencyInjectionMainArticle
---

# Dependency injection

The Screenplay design pattern is fundamentally compatible with and based upon [dependency injection], aka DI.
You may add Screenplay to an existing container of you wish, via the [`AddScreenplay`] extension method.
Alternatively you may create an instance of [`Screenplay`] which uses its own self-contained DI container with the `static` [`Screenplay.Create`] helper method.

[dependency injection]: https://en.wikipedia.org/wiki/Dependency_injection
[`AddScreenplay`]: xref:CSF.Screenplay.ScreenplayServiceCollectionExtensions.AddScreenplay(Microsoft.Extensions.DependencyInjection.IServiceCollection,System.Action{CSF.Screenplay.ScreenplayOptions})
[`Screenplay`]: xref:CSF.Screenplay.Screenplay
[`Screenplay.Create`]: xref:CSF.Screenplay.Screenplay.Create(System.Action{Microsoft.Extensions.DependencyInjection.IServiceCollection},System.Action{CSF.Screenplay.ScreenplayOptions})

## Scopes

Screenplay makes use of [Dependency Injection Scopes].
As you can see on [the diagram of how Actors, Abilities and Performables relate to one another], each [`Screenplay`] and executes contains many [Performances].
Each performance is executed within its own DI scope, thus scoped services will have _one shared instance per performance_.

It is also important to note that within DI, the [`Screenplay`] object _must be added_ as a singleton (the mechanisms described above all do this for you).
As a consequence there may be _at most one_ Screenplay object per dependency injection container.

[Dependency Injection Scopes]: https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection#scoped
[the diagram of how Actors, Abilities and Performables relate to one another]: ../MakeupOfAScreenplay.md
[Performances]: xref:CSF.Screenplay.IPerformance

## Using DI in Screenplay

TODO: Write this section!

* [Injecting services](InjectingServices.md)
* [Adding services](AddingServices.md)
* [Abilities as a form of DI](../../glossary/Ability.md)
