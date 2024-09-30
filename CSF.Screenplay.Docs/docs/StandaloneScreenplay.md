# Using Screenplay standalone

Screenplay is often used [as a tool for automating software tests], but it is not limited to only this usage.
Screenplay may be added to any application or library, _via dependency injection_.
This is as simple as installing the **[CSF.Screenplay]** NuGet package and adding Screenplay to your service collection.
For more information, see the documentation for [`ScreenplayServiceCollectionExtensions`].

```csharp
using CSF.Screenplay;
// IServiceCollection services; 
services.AddScreenplay();
```

Once Screenplay has been added to your DI, [you may resolve and use Screenplay-related services from dependency injection].
To execute some logic in the scope of a [`Performance`], consider using the method [`Screenplay.ExecuteAsPerformanceAsync`].

[as a tool for automating software tests]: SuitabilityAsATestingTool.md
[CSF.Screenplay]: https://www.nuget.org/packages/CSF.Screenplay
[`ScreenplayServiceCollectionExtensions`]: xref:CSF.Screenplay.ScreenplayServiceCollectionExtensions
[you may resolve and use Screenplay-related services from dependency injection]: dependencyInjection/InjectingServices.md
[`Performance`]: xref:CSF.Screenplay.IPerformance
[`Screenplay.ExecuteAsPerformanceAsync`]: xref:CSF.Screenplay.Screenplay.ExecuteAsPerformanceAsync(System.Func{System.IServiceProvider,System.Threading.CancellationToken,System.Threading.Tasks.Task{System.Nullable{System.Boolean}}},System.Collections.Generic.IList{CSF.Screenplay.Performances.IdentifierAndName},System.Threading.CancellationToken)

## Abstractions package

If your solution is separated into multiple projects/assemblies then only your entry-point project needs the full CSF.Screenplay NuGet package. 
Once Screenplay has been added to DI, other projects in the solution may consume its logic, only requiring the [CSF.Screenplay.Abstractions] package.

[CSF.Screenplay.Abstractions]: https://www.nuget.org/packages/CSF.Screenplay.Abstractions
