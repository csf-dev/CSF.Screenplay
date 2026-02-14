---
uid: DependencyInjectionMainArticle
---

# Dependency injection

The Screenplay design pattern is fundamentally compatible with and based upon [dependency injection], aka DI.
You may add Screenplay to an existing [container] if you wish, via the [`AddScreenplay`] extension method.
Alternatively you may create an instance of [`Screenplay`] which uses its own self-contained DI container with the static [`Screenplay.Create`] helper method.

[dependency injection]: https://en.wikipedia.org/wiki/Dependency_injection
[container]: xref:Microsoft.Extensions.DependencyInjection.IServiceCollection
[`AddScreenplay`]: xref:CSF.Screenplay.ScreenplayServiceCollectionExtensions.AddScreenplay(Microsoft.Extensions.DependencyInjection.IServiceCollection)
[`Screenplay`]: xref:CSF.Screenplay.Screenplay
[`Screenplay.Create`]: xref:CSF.Screenplay.Screenplay.Create(System.Action{Microsoft.Extensions.DependencyInjection.IServiceCollection})

## Learn more about DI in Screenplay

* [How to add services to the container](AddingServices.md)
* [Which services are injectable](InjectableServices.md)
* [Getting/injecting services](InjectingServices.md)
* [DI scopes](DependencyInjectionScope.md)
