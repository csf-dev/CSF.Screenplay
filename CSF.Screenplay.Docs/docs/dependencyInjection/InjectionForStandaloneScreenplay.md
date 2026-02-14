# Dependency injection for standalone Screenplay

If you are [using Screenplay standalone] then you are likely using the method [`Screenplay.ExecuteAsPerformanceAsync`] or one of its overloads.

[using Screenplay standalone]: ../gettingStarted/nonTesting/index.md
[`Screenplay.ExecuteAsPerformanceAsync`]: xref:CSF.Screenplay.Screenplay.ExecuteAsPerformanceAsync(System.Func{System.IServiceProvider,System.Threading.CancellationToken,System.Threading.Tasks.Task{System.Nullable{System.Boolean}}},System.Collections.Generic.IList{CSF.Screenplay.Performances.IdentifierAndName},System.Threading.CancellationToken)

## Recommended: Use `IHostsPerformance`

Developers are urged to consider encapsulating their performance logic in a class which derives from [`IHostsPerformance`].
Through an overload (extension method) named [`ExecuteAsPerformanceAsync<T>`], developers may specify the concrete implementation type of that class as a generic type parameter.
This overload of `ExecuteAsPerformanceAsync` will resolve your class which derives from `IHostsPerformance` from dependency injection (_remember to add it to the container!_).
This resolution will include all the class' dependencies which are constructor-injected and leads to very clean code.
The service provider is never exposed, thus removing the temptation to use the service locator anti-pattern.

[`IHostsPerformance`]: xref:CSF.Screenplay.IHostsPerformance
[`ExecuteAsPerformanceAsync<T>`]: xref:CSF.Screenplay.ScreenplayExtensions.ExecuteAsPerformanceAsync``1(CSF.Screenplay.Screenplay,System.Collections.Generic.IList{CSF.Screenplay.Performances.IdentifierAndName},System.Threading.CancellationToken)

### Example of the performance host technique

This example is functionally identical to the example below.

```csharp
public class MyPerformance(ICast cast) : IHostsPerformance
{
    public Task<bool?> ExecutePerformanceAsync(CancellationToken cancellationToken)
    {
        var aaron = cast.GetActor<Aaron>();
        // Further performance logic ...
    }
}

// To consume the above:
await screenplay.ExecuteAsPerformanceAsync<MyPerformance>();
```

## Alternative: Use a function

The original [`Screenplay.ExecuteAsPerformanceAsync`] overload accepts up to three parameters, the first of which is a function.
That function should contain the logic which makes up the performance.
The function (which drives the performance logic) provides two parameters itself:

* An [`IServiceProvider`](xref:System.IServiceProvider)
* A [`CancellationToken`](xref:System.Threading.CancellationToken)

The serivce provider may be used to resolve services which may be used by the performance logic.

### Example of the function-based technique

This example is functionally identical to the example above.

```csharp
await screenplay.ExecuteAsPerformanceAsync((services, cancellationToken) => {
    var cast = services.GetRequiredService<ICast>();
    var aaron = cast.GetActor<Aaron>();
    // Further performance logic ...
});

```
