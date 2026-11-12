---
uid: BoDiWorkaroundsArticle
---

# Workarounds for the BoDi DI container

Reqnroll (and SpecFlow before it) use a custom dependency injection container named BoDi.
It does not implement the complete set of functionality of `IServiceCollection` or `IServiceProvider` and what's it does implement differs from them in some important ways.
However, the .NET Dependency Injection ecosystem fully expects the pair of `IServiceCollection` and `IServiceProvider`.
Many libraries expose extension methods for `IServiceCollection` which use that API to add themselves to a container.

In an attempt to close this gap, Screenplay offers two limited **adapters** which allow some usage of BoDi's `IObjectContainer` as a service collection and as a service provider.
These are accessible via the extension methods [`ToServiceCollection`] and [`ToServiceProvider`].
_Be warned that these are known to be imperfect_; they suffer from multiple limitations and will not work in all use-cases.
If you encounter showstopper problems using BoDi, _do not forget that Reqnroll provides support for swapping its DI container_ with another.
[This article shows you how to replace the BoDi container with Autofac], for example.

The remainder of this page lists some of the particular difficulties and workarounds which have solutions and/or further documentation.

[`ToServiceCollection`]: xref:CSF.Screenplay.ObjectContainerExtensions.ToServiceCollection(Reqnroll.BoDi.IObjectContainer)
[`ToServiceProvider`]: xref:CSF.Screenplay.ObjectContainerExtensions.ToServiceProvider(Reqnroll.BoDi.IObjectContainer)
[This article shows you how to replace the BoDi container with Autofac]: https://docs.reqnroll.net/latest/integrations/autofac.html

## BoDi cannot natively resolve `IEnumerable<T>` of registered services

Say you were to separately register (add) three different implementations of `IMyCustomService` to a normal `ServiceCollection` instance. If you subsequently resolve `IEnumerable<IMyCustomService>` from the resultant service provider then you would get a collection of custom service instances containing three items, one of each of the three implementations. This is built-in functionality for the standard Microsoft Service Collection.

_BoDi cannot do this without some help_. The Screenplay Reqnroll plugin includes that help in the form of an extension method for `IServiceCollection`: [`AddEnumerableAdapter<T>()`]. Use this method to enable enumerable resolution (as described above) for the specified generic type. You must use this method for each type T of `IEnumerable<T>` your tests need to be capable of resolving.
There are further technical details about this limitation and the workaround available in the remarks for [`EnumerableResolutionAdapter<T>`].

[`AddEnumerableAdapter<T>()`]: xref:CSF.Screenplay.ServiceCollectionExtensions.AddEnumerableAdapter``1(Microsoft.Extensions.DependencyInjection.IServiceCollection)
[`EnumerableResolutionAdapter<T>`]: xref:CSF.Screenplay.EnumerableResolutionAdapter`1

## BoDi does not natively support the Microsoft Options Pattern

This is only relevant if you intend to make use of the [Microsoft Options Pattern] in your tests. The Selenium plugin does (via [CSF.WebDriver.Extensions]) and if using it, you must include `.AddOptionsAdapters<WebDriverCreationOptionsCollection>()` in your DI setup.

This limitation is a specific case of BoDi's inability to resolve enumerables of registered services (above). There is an extension method specifically crafted for this: [`AddOptionsAdapters<TOptions>()`]. This method must be used once for each type of TOptions you wish to use. It puts adapters/workarounds in place, to allow resolution of (for example) `IOptions<TOptions>` in your tests.

[Microsoft Options Pattern]: https://learn.microsoft.com/en-us/dotnet/core/extensions/options
[CSF.WebDriver.Extensions]: https://github.com/csf-dev/CSF.Extensions.WebDriver
[`AddOptionsAdapters<TOptions>()`]: xref:CSF.Screenplay.ServiceCollectionExtensions.AddOptionsAdapters``1(Microsoft.Extensions.DependencyInjection.IServiceCollection)

## Liskov Substitution Principle violations

The adapters for using BoDi as an `IServiceCollection` and `IServiceProvider` are imperfect and limited.
In addition, they do not provide the complete range of functionality which is expected by the contract of these two interfaces.
This is [a violation of LSP].

There is extensive documentation about the differences in implementation details in the remarks for the classes [`ServiceCollectionAdapter`] and [`ServiceProviderAdapter`].

[a violation of LSP]: https://en.wikipedia.org/wiki/Liskov_substitution_principle
[`ServiceCollectionAdapter`]: xref:CSF.Screenplay.ServiceCollectionAdapter
[`ServiceProviderAdapter`]: xref:CSF.Screenplay.ServiceProviderAdapter
