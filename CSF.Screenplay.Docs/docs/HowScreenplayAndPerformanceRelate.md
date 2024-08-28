---
uid: HowScreenplayAndPerformanceRelateArticle
---

# Top-down look at a screenplay

The diagram below shows a top-down look at a [Screenplay] and how it relates to [Performances], [Actors] and [Performables].
The Screenplay _might itself be controlled by_ a [Test Integration], if Screenplay is being used for automated tests.

The lifetime shown, for the [Performance] also indicates the lifetime of [the dependency injection scope].

```mermaid
sequenceDiagram
    accDescr {
        A single instance of Screenplay runs each Performance.
        Each Performance contains scripts for one or more Actors.
        Within the Performance, the Actor(s) perform one or more Performables.
        The lifetime of a single performance is shown, to illustrate the DI lifetime scope.
    }
    Screenplay->>Performance: Runs each
    actor A as Actor
    activate Performance
    Performance->>A: Contains scripts for<br>one or more
    A->>Performable: Performs one<br>or more
    Performable-->>A: Complete
    A-->>Performance: Complete
    Performance-->>Screenplay: Complete
    deactivate Performance
```

[Screenplay]: xref:CSF.Screenplay.Screenplay
[Performances]: xref:CSF.Screenplay.IPerformance
[Actors]: xref:CSF.Screenplay.Actor
[Performables]: ../glossary/Performable.md
[Test Integration]: ../glossary/Integration.md
[Performance]: xref:CSF.Screenplay.IPerformance
[the dependency injection scope]: dependencyInjection/DependencyInjectionScope.md