# Performables

Screenplay comes with a few pre-created [Abilities], [Performables] and [Builders], for common tasks.

[Abilities]: ../../glossary/Ability.md
[Performables]: ../../glossary/Performable.md
[Builders]: ../builderPattern/index.md

## Using a Stopwatch

When an actor needs to keep precise track of time, you may give them the [`UseAStopwatch`] ability.
Actors with this ability may use Actions and Questions which relate to use of the stopwatch.
These are all accessible from the builder class [`StopwatchBuilder`].

[`UseAStopwatch`]: xref:CSF.Screenplay.Abilities.UseAStopwatch
[`StopwatchBuilder`]: xref:CSF.Screenplay.Performables.StopwatchBuilder

## Interacting with web APIs

The NuGet package [CSF.Screenplay.WebApis] provides an ability, performables and supporting types to interact with web API endpoints.
Further information is available on [the web API documentation page].

[CSF.Screenplay.WebApis]: https://www.nuget.org/packages/CSF.Screenplay.WebApis/
[the web API documentation page]: WebApis.md

## TimeSpan builder

The [`TimeSpanBuilder<TOtherBuilder>`] is not a complete performable builder; it is intended to supplement other builders such as those of your own design.
It handles a commonly-used aspect of building performables in a reusable manner.

[`TimeSpanBuilder<TOtherBuilder>`]: xref:CSF.Screenplay.Performables.TimeSpanBuilder`1