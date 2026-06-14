---
uid: DependencyInjectionMainArticle
---

# Dependency injection

The Screenplay design pattern is fundamentally compatible with and based upon [dependency injection], aka DI.

* You may add Screenplay to an existing [container] via the [`AddScreenplay`] extension method
* You may create a self-contained instance of [`Screenplay`] via the static [`Screenplay.Create`] helper method
  * Use this is you wish to integrate with an app or library which does not use DI itself
* [Test integrations] have their own ways to integrate with DI
  * [DI integration with NUnit 3+]
  * [DI integration with Reqnroll/Specflow]

[dependency injection]: https://en.wikipedia.org/wiki/Dependency_injection
[container]: xref:Microsoft.Extensions.DependencyInjection.IServiceCollection
[`AddScreenplay`]: xref:CSF.Screenplay.ScreenplayServiceCollectionExtensions.AddScreenplay(Microsoft.Extensions.DependencyInjection.IServiceCollection)
[`Screenplay`]: xref:CSF.Screenplay.Screenplay
[`Screenplay.Create`]: xref:CSF.Screenplay.Screenplay.Create(System.Action{Microsoft.Extensions.DependencyInjection.IServiceCollection})
[Test integrations]: ../../glossary/Integration.md
[DI integration with NUnit 3+]: ../gettingStarted/nunit3/index.md#step-3-write-a-screenplay-factory
[DI integration with Reqnroll/Specflow]: ../gettingStarted/reqnroll/index.md#step-3-configuring-dependency-injection

## Learn more about DI in Screenplay

* [How to add services to the container](AddingServices.md)
* [Which services are injectable](InjectableServices.md)
* [Getting/injecting services](InjectingServices.md)
* [DI scopes](DependencyInjectionScope.md)
