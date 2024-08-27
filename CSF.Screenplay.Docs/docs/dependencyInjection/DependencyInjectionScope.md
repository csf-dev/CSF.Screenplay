---
uid: DependencyInjectionScopeArticle
---

# Dependency injection scope

Developers familiar with dependency injection are likely to be familiar with the concept of [DI Scopes].
That is - some services which are designated as _scoped_ or _instance per scope_ use a common/shared instance for the lifetime/duration of the scope.

Within Screenplay logic, a DI scope is automatically created, with a lifetime matching that of the current [Performance].
Within a performance, when any of the scoped services (listed below) are injected, each point of injection will receive the same shared instance of that service.
Instances are independent per-performance; each performance gets its own shared instance of each of the listed services.

[DI Scopes]: https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection#service-lifetimes
[Performance]: xref:CSF.Screenplay.Performance

## List of scoped services

* [The Cast](xref:CSF.Screenplay.ICast)
* [The Stage](xref:CSF.Screenplay.IStage)
* [The current performance](xref:CSF.Screenplay.Performance)