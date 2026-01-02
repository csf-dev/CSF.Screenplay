---
uid: InjectableServicesArticle
---

# Injectable services

Screenplay explicitly supports dependency-injection of the following services from the Screenplay architecture into your [performance] logic.
These are _in addition to_ any services you may have configured yourself within DI.

Whilst it may be possible to inject other services from Screenplay's architecture, these are explicitly supported throughout all [integrations].

[performance]: xref:CSF.Screenplay.IPerformance
[integrations]: ../../glossary/Integration.md

## The Stage

Inject the [`IStage`] into your [performance] logic to get and work with [Actors].
This is _the recommended way_ to manage any actors involved in a performance.

The stage provides all the functionality of the Cast (below) as well as control of the [spotlight], should you wish to use it.

[`IStage`]: xref:CSF.Screenplay.IStage
[Actors]: xref:CSF.Screenplay.Actor
[spotlight]: ../../glossary/Spotlight.md

## The Cast

The [`ICast`] is available for dependency injection into your [performance] logic as an alternative mechanism by which to work with [Actors].
It is recommended to _inject the Stage (above) instead of the Cast_.
The Cast is reachable from the Stage should you need it, via the [`Cast`] property.

[`ICast`]: xref:CSF.Screenplay.ICast
[`Cast`]: xref:CSF.Screenplay.IStage.Cast

## The Performance

You may inject an instance of [`IPerformance`] to gain direct access to the current [performance] object, from within your performance logic.
Whilst supported, _it is usually not required (or recommended)_ to inject this object into your performance logic.

[`IPerformance`]: xref:CSF.Screenplay.IPerformance
