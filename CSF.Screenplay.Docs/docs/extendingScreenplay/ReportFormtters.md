# Report value formtters

To get the best results [when Screenplay writes a report], the wording in the reports should be human-readable and avoid language that relates to .NET.
Reports are built from [report fragments], accumulated during each [performance].

[when Screenplay writes a report]: ../GettingReports.md
[report fragments]: xref:CSF.Screenplay.ReportFragment
[performance]: xref:CSF.Screenplay.IPerformance

## Baseline reporting functionality

It's strongly recommended that all [performables] and all [abilities] implement the [`ICanReport`] interface.
This allows these types to generate report fragments when they are performed-by or granted-to to [Actors].
Performables and abilities _which do not_ implement `ICanReport` will use a default/fallback report template which is likely to produce sub-optimal results.

[performables]: ../../glossary/Performable.md
[abilities]: ../../glossary/Ability.md
[`ICanReport`]: xref:CSF.Screenplay.ICanReport
[Actors]: xref:CSF.Screenplay.Actor

## Formatting values

As noted in the documentation for [report fragments], they are written using template strings which include _a placeholder syntax_.
Values must be inserted into these placeholders to get the final report; this is performed by [`IFormatsReportFragment`].
There are a few mechanisms by which `IFormatsReportFragments` converts values to human-readable strings; you are encouraged to pick the most suitable for each scenario.

You may **extend Screenplay** with new implementations of [`IHasName`], [`IFormattableValue`] and/or [`IValueFormatter`].

[`IFormatsReportFragment`]: xref:CSF.Screenplay.IFormatsReportFragment

### Objects with names

[Actors] and some other types implement the interface [`IHasName`].
This is suitable for objects which would always appear the same in any report.
For example, the Actor "Joe" is always "Joe"; there's nothing more to their name than that.

[`IHasName`]: xref:CSF.Screenplay.IHasName

### Self-formattable values

Types which implement [`IFormattableValue`] have a `Format()` method which returns a human-readable formatted representation of that object, suitable for appearance in reports.

Use this if the object's state must be used to get the reporting representation, but does not require any external dependencies or services.
Obviously, you must have control over the type - the ability to add `IFormattableValue` to its interfaces - in order to use this technique.

[`IFormattableValue`]: xref:CSF.Screenplay.Reporting.IFormattableValue

### Value formatters

Value formatters are external objects which are able to format objects, without needing to make any changes to the object-to-be-formatted.
Value formatters implement the interface [`IValueFormatter`] and must be added to [dependency injection], as well as registered with the [`IFormatterRegistry`].

Use this technique when either you require external services from dependendency injection to format the object, or if you are unable/unwilling to have the type to be formatted implement [`IFormattableValue`].

[`IValueFormatter`]: xref:CSF.Screenplay.Reporting.IValueFormatter
[dependency injection]: ../dependencyInjection/index.md
[`IFormatterRegistry`]: xref:CSF.Screenplay.Reporting.IFormatterRegistry

### Use `ToString()`

The least optimal mechanism of formatting values in a report is to rely on the built-in (or an overridden) `Object.ToString()` method.
Whilst this will work in reports, it is intended as fallback functionality for types which are not covered by any of the techniques above.

The results of the `ToString()` method are often a very poor choice for reports.
