---
uid: DependencyInjectionScopeArticle
---

# Dependency injection scope

Developers familiar with dependency injection are likely to be familiar with the concept of [DI Scopes].
Screenplay uses this concept; a number of its services are added to the container with _a per-scope lifetime_.
Screenplay creates **a new DI scope per [Performance]**.
As you can see on [the diagram of how Actors, Abilities and Performables relate to one another], each [`Screenplay`] contains and executes many performances.
This scope-creation is handled automatically by the Screenplay framework logic.

Within a performance, when any of the scoped services (listed below) are injected, each point of injection will receive the same shared instance of that service.
Instances are independent per-performance; each performance gets its own shared instance of each of the listed services.

[DI Scopes]: https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection#service-lifetimes
[Performance]: xref:CSF.Screenplay.Performance
[the diagram of how Actors, Abilities and Performables relate to one another]: ../concepts/MakeupOfAScreenplay.md
[`Screenplay`]: xref:CSF.Screenplay.Screenplay

## List of scoped services

The following services are added to DI "per lifetime scope".

* [The Cast](xref:CSF.Screenplay.ICast)
* [The Stage](xref:CSF.Screenplay.IStage)
* [The current performance](xref:CSF.Screenplay.IPerformance)

## List of singleton services

The following services are added to DI as singletons.
There is only ever a single instance of these services per Screenplay.

* [The Screenplay](xref:CSF.Screenplay.Screenplay)
* [The event bus](xref:CSF.Screenplay.Performances.PerformanceEventBus)
  * _This includes its interfaces_
* [The Screenplay options](xref:CSF.Screenplay.ScreenplayOptions)
* [The report builder](xref:CSF.Screenplay.Reporting.ScreenplayReportBuilder)
* [The reporter implementation](xref:CSF.Screenplay.Reporting.IReporter)
  * _The implementation resolved depends upon [whether reporting is enabled](xref:CSF.Screenplay.ScreenplayOptions.ReportPath)_