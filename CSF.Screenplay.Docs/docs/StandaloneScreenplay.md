# Using Screenplay standalone

Screenplay is often used [as a tool for automating software tests], but it is not limited to only this usage.
Screenplay may be added to any application or library, _via dependency injection_.

Adding the Screenplay architecture to your apps DI is as simple as installing the **[CSF.Screenplay]** NuGet package and:

```csharp
using CSF.Screenplay;
// IServiceCollection services; 
services.AddScreenplay();
```

For more information, see the documentation for [`ScreenplayServiceCollectionExtensions`].

[as a tool for automating software tests]: SuitabilityAsATestingTool.md
[CSF.Screenplay]: https://www.nuget.org/packages/CSF.Screenplay
[`ScreenplayServiceCollectionExtensions`]: xref:CSF.Screenplay.ScreenplayServiceCollectionExtensions

## Abstractions package

If your solution is separated into multiple projects/assemblies then only your entry-point project needs the full CSF.Screenplay NuGet package. 
Once Screenplay has been added to DI, other projects in the solution may consume its logic, only requiring the [CSF.Screenplay.Abstractions] package.

[CSF.Screenplay.Abstractions]: https://www.nuget.org/packages/CSF.Screenplay.Abstractions