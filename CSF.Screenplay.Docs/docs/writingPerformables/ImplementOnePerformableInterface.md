# Implementing the performable interface

A [Performable] is a class which implements one of the three performable interfaces:

* [`IPerformable`]
* [`IPerformableWithResult`]
* [`IPerformableWithResult<TResult>`]

Performables must implement _precisely one_ of these interfaces.
Implementing more than one upon a single performable is not recommended or supported.
Doing so is liable to cause difficulties.

If you wish to share code then move the reusable logic into [a Task] and consume that from two separate performable classes.

[Performable]: ../../glossary/Performable.md
[`IPerformable`]: xref:CSF.Screenplay.IPerformable
[`IPerformableWithResult`]: xref:CSF.Screenplay.IPerformableWithResult
[`IPerformableWithResult<TResult>`]: xref:CSF.Screenplay.IPerformableWithResult`1
[a Task]: ../../glossary/Task.md
