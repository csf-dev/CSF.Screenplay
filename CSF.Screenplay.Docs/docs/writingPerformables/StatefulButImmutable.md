# Performables are stateful, but immutable

Instances of [Performable] classes _are not intended to be reused_.
A single class may have many instances created, but each instance should be used only once.
This is because [the 'parameter values'] for each performable are provided into that performable object instance.

Parameters should ideally be provided into the performable class' public constructor.
Another viable technique could be [`init`-only property setters].
Once these values are set, they should be `readonly` so that they may not be changed.

Performable classes should also avoid the use of mutable class-level data such as fields or properties.
Any temporary state should be scoped only to the relevant `PerformAsAsync` method.

[Performable]: ../../glossary/Performable.md
[the 'parameter values']: ParameterizeLowLevelPerformables.md
[`init`-only property setters]: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/init
