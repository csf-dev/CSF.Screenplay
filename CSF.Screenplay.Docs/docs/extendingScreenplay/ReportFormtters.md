# Report value formtters

To get the best results [when Screenplay writes a report], it's important to format relevant values in a human-readable manner.

Getting human-readable reports from [performables] and abilities may be achieved by implementing the [`ICanReport`] interface, and is strongly recommended.
[Actors] implement [`IHasName`]; this might be suitable for types which you create for extending Screenplay. 

For other types, to see human-readable formatted values in reports, use one of the following two approaches: 

* If you are able, you may have your type implement [`IFormattableValue`]
* If that is unsuitable, create and register an implementation of [`IValueFormatter`] for the type

[when Screenplay writes a report]: ../GettingReports.md
[performables]: ../../glossary/Performable.md
[`ICanReport`]: xref:CSF.Screenplay.ICanReport
[Actors]: xref:CSF.Screenplay.Actor
[`IHasName`]: xref:CSF.Screenplay.IHasName
[`IFormattableValue`]: xref:
[`IValueFormatter`]: xref:

## Formattable values

Formattable values are types which are able to format themselves in a report. 
This technique is most suitable for types over which you have full control, and are able to add an additional interface. 

Built-in functionality of the reporting architecture will detect values in reports which implement [`IFormattableValue`] and will format them using their `Format()` method.

## Value formatters 

A value formatter is a type which is [added to DI] to format values which it is able. 
