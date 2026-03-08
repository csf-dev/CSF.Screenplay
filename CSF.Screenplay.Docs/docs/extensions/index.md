# Screenplay extensions

Most of the useful functionality of Screenplay comes from **[Screenplay Extensions]**.
Extensions typically provide [Abilities], [Performables] and [Builders] which offer functionality relevant to that extension's technology.

[Screenplay Extensions]: ../../glossary/Extension.md
[Abilities]: ../../glossary/Ability.md
[Performables]: ../../glossary/Performable.md
[Builders]: ../builderPattern/index.md

## Officially-supported extensions

These extensions are authored and maintained alongside the main Screenplay library.
Developers are encouraged and welcomed to create their own extensions, too.

* **[Selenium](selenium/index.md)**: _remote control Web Browsers using Selenium Web Driver_
* **[Web APIs](webApis/index.md)**: _communicate with Web APIs with an HTTP Client_

## Built-in abilities and performables

Screenplay offers a very small number of abilities and performables within the base library; these do not require the installation of an extension.

### Using a Stopwatch

When an actor needs to keep precise track of time, you may give them the [`UseAStopwatch`] ability.
Actors with this ability may use Actions and Questions which relate to use of the stopwatch.
These are all accessible from the builder class [`StopwatchBuilder`].

[`UseAStopwatch`]: xref:CSF.Screenplay.Abilities.UseAStopwatch
[`StopwatchBuilder`]: xref:CSF.Screenplay.Performables.StopwatchBuilder
