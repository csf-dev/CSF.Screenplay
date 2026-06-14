# Using Screenplay for process automation

Screenplay is often used [as a tool for automating software tests], but it is not limited to only this usage.
Screenplay may be added to any application or library, _via dependency injection_.
This is as simple as installing the **[CSF.Screenplay]** NuGet package and adding Screenplay to your service collection.
For more information, see the documentation for [`ScreenplayServiceCollectionExtensions`].

Once Screenplay has been added to your DI, [you may resolve and use Screenplay-related services from dependency injection].
To execute some logic in the scope of a [`Performance`], consider using the method [`Screenplay.ExecuteAsPerformanceAsync`].

[you may resolve and use Screenplay-related services from dependency injection]: ../../dependencyInjection/InjectingServices.md
[`Performance`]: xref:CSF.Screenplay.IPerformance
[`Screenplay.ExecuteAsPerformanceAsync`]: xref:CSF.Screenplay.Screenplay.ExecuteAsPerformanceAsync(System.Func{System.IServiceProvider,System.Threading.CancellationToken,System.Threading.Tasks.Task{System.Nullable{System.Boolean}}},System.Collections.Generic.IList{CSF.Screenplay.Performances.IdentifierAndName},System.Threading.CancellationToken)

## Example

The following example shows the core components for using Screenplay outside of a testing framework.

* DI registration which adds the Screenplay architecture
* A start-point class which begins one or more performances
* Each performance is coordinated from its own class which implements [`IHostsPerformance`]

_This is the recommended pattern_ for consuming Screenplay outside of a testing framework.
See the documentation for the [`Screenplay`] and [`ScreenplayExtensions`] classes for other techniques.

If you wish to activate Screenplay extensions, be sure to add these to the DI container in your application startup.
For example, to add [the Screenplay/Selenium extension], add a line like `services.AddSelenium()`.

```csharp
using CSF.Screenplay;

// In your app startup, services is an instance of IServiceCollection, your
// application's DI container.
services.AddScreenplay();
// optionally add further services to the container, as normal

// ... now in your regular app logic you may now constructor inject Screenplay:
public class MyScreenplayConsumer(Screenplay screenplay)
{
    public async Task StartScreenplay()
    {
        // As many performances as you want
        await screenplay.ExecuteAsPerformanceAsync<SamplePerformance>();
    }
}

public class SamplePerformance(IStage stage) : IHostsPerformance
{
    public Task<bool?> ExecutePerformanceAsync(CancellationToken token)
    {
        // Use the injected stage to get an actor and execute your Screenplay logic
    }
}
```

[as a tool for automating software tests]: ../../bestPractice/SuitabilityAsATestingTool.md
[CSF.Screenplay]: https://www.nuget.org/packages/CSF.Screenplay
[`ScreenplayServiceCollectionExtensions`]: xref:CSF.Screenplay.ScreenplayServiceCollectionExtensions
[`IHostsPerformance`]: xref:CSF.Screenplay.IHostsPerformance
[`Screenplay`]: xref:CSF.Screenplay.Screenplay
[`ScreenplayExtensions`]: xref:CSF.Screenplay.ScreenplayExtensions
[the Screenplay/Selenium extension]: ../../extensions/selenium/index.md

## The Abstractions package

If your solution is separated into multiple projects/assemblies then only your entry-point project needs the full CSF.Screenplay NuGet package.
Once Screenplay has been added to DI, other projects in the solution may consume its logic, only requiring the [CSF.Screenplay.Abstractions] package.

[CSF.Screenplay.Abstractions]: https://www.nuget.org/packages/CSF.Screenplay.Abstractions
