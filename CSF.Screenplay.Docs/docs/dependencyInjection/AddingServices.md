# Adding dependency services to Screenplay

There are two techniques to add dependency services to Screenplay.
You may either integrate Screenplay into an existing container for your application or tests or you may add additional services via the static [`Create`] factory method.

[`Create`]: xref:CSF.Screenplay.Screenplay.Create(System.Action{Microsoft.Extensions.DependencyInjection.IServiceCollection},System.Action{CSF.Screenplay.ScreenplayOptions})

## Integrating with an existing container

When using an existing [`IServiceCollection`] for you application, either [using Screenplay standalone] or [with a test framework] that integrates with dependency injection, then adding services is simple.
Just add your services to the container (the service collection) as normal; they will be available [to resolve and inject] over the course of the Screenplay.

[`IServiceCollection`]: https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection
[using Screenplay standalone]: ../gettingStarted/nonTesting/index.md
[with a test framework]: ../concepts/ScreenplayInTheTestingStack.md
[to resolve and inject]: InjectingServices.md

## Using the static `Create` factory

If you do not wish to integrate Screenplay into an existing dependency injection container then the simplest way to create an instance of Screenplay is to use the static [`Screenplay.Create`] factory method.
This method offers an optional parameter to provide an `Action<IServiceCollection>`.
If you do so, then you may provide a customization action which adds additional services to the container.
These services will be integrated into the self-contained service collection which the `Create` method creates for Screenplay.

```csharp
var screenplay = Screenplay.Create(services => {
    services.AddTransient<MyCustomService>();
    // ... and anything further you want here
});
```

[`Screenplay.Create`]: xref:CSF.Screenplay.Screenplay.Create(System.Action{Microsoft.Extensions.DependencyInjection.IServiceCollection},System.Action{CSF.Screenplay.ScreenplayOptions})

## A reminder on lifetime scopes

Remember that Screenplay imposes [some requirements upon service lifetime]:

* The [`Screenplay`] object is _always a singleton_
* Each [Performance] _is always executed within its own lifetime scope_

Add your own services to dependency injection accordingly.

[some requirements upon service lifetime]: DependencyInjectionScope.md
[`Screenplay`]: xref:CSF.Screenplay.Screenplay
[Performance]: xref:CSF.Screenplay.IPerformance
