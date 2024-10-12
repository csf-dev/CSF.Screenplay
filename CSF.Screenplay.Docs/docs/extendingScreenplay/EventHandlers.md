# Writing new Event handlers

A [Screenplay] is an architecture for directing a series of steps, organised into groups named [Performances].
Within that architecture is an **event** model, which allows arbitrary logic to subscribe to the progress of a Screenplay and react accordingly. 
This is one of the natural _extension points_ within the Screenplay library.

[Screenplay]: xref:CSF.Screenplay.Screenplay
[Performances]: xref:CSF.Screenplay.IPerformance

## How to subscribe to events

Within the CSF.Screenplay.Abstractions library/NuGet package is the type [`IHasPerformanceEvents`].
An instance of this type is available to resolve via [dependency injection] whilst a Screenplay is in-progress.
Within DI, this object is a singleton which will emit when any significant Screenplay-related event occurs.
Take a look at the API of the interface and its documentation to read about the available events and their meaning.

If you wish to extend Screenplay you may subscribe to these from your own logic as you please.

[`IHasPerformanceEvents`]: xref:CSF.Screenplay.Performances.IHasPerformanceEvents
[dependency injection]: ../dependencyInjection/index.md