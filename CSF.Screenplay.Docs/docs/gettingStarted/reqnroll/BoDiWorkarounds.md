Reqnroll (and SpecFlow before it) use a custom dependency injection container named BoDi. It does not implement the complete set of functionality of `IServiceCollection` or `IServiceProvider` and what's it does implement differs from them in some important ways.

To work with BoDi, Screenplay offers two limited **adapters** which allow some usage of BoDi's `IObjectContainer` as a service collection and as a service provider. These are accessible via the extension methods [`ToServiceCollection`] and [`ToServiceProvider`].

This article is likely not exhaustive, but it documents some of the difficulties and workarounds which have been found/developed whilst developing CSF.Screenplay.

[`ToServiceCollection`]: xref:CSF.Screenplay. ObjectContainerExtensions.ToServiceCollection
[`ToServiceProvider`]: xref:CSF.Screenplay. ObjectContainerExtensions.ToServiceProvider

## BoDi cannot natively resolve `IEnumerable<T>` of registered services

Say you were to separately register (add) three different implementations of `IMyCustomService` to a normal `ServiceCollection` instance. If you subsequently resolve `IEnumerable<IMyCustomService>` from the resultant service provider then you would get a collection of custom service instances containing three items, one of each of the three implementations. This is built-in functionality for the standard Microsoft Service Collection.

_BoDi cannot do this without some help_. The Screenplay Reqnroll plugin includes that help in the form of an extension method for `IServiceCollection`: [`AddEnumerableAdapter<T>()`]. Use this method to enable enumerable resolution (as described above) for the specified generic type. You must use this method for each type T of `IEnumerable<T>` your tests need to be capable of resolving.

[`AddEnumerableAdapter<T>()`]:xref:CSF.Screenplay.ServiceCollectionExtensions.AddEnumerableAdapter`1()

## BoDi does not natively support the Microsoft Options Pattern

This is only relevant if you intend to make use of the [Microsoft Options Pattern] in your tests. The Selenium plugin does (via [CSF.WebDriver.Extensions]) and if using it, you must include `.AddOptionsAdapters<WebDriverCreationOptionsCollection>()` in your DI setup.

This limitation is a specific case of BoDi's inability to resolve enumerables of registered services (above). There is an extension method specifically crafted for this: [`AddOptionsAdapters<TOptions>()`]. This method must be used once for each type of TOptions you wish to use. It puts adapters/workarounds in place, to allow resolution of (for example) `IOptions<TOptions>` in your tests.