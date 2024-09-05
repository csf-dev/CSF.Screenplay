# Performables

Screenplay comes with a few pre-created [Abilities], [Performables] and [Builders], for common tasks.

[Abilities]: ../../glossary/Ability.md
[Performables]: ../../glossary/Performable.md
[Builders]: ../builderPattern/index.md

## Using a Stopwatch

When an actor needs to keep precise track of time, you may give them the [`UseAStopwatch`] ability.
Actors with this ability may use Actions and Questions which relate to use of the stopwatch.
These are all accessible from the builder class [`StopwatchBuilder`].

## TimeSpan builder

The [`TimeSpanBuilder<TOtherBuilder>`] is not a complete performable builder; it is intended to supplement other builders such as those of your own design.
It handles a commonly-used aspect of building performables in a reusable manner.

[`TimeSpanBuilder<TOtherBuilder>`]: xref:CSF.Screenplay.Performables.TimeSpanBuilder`1
