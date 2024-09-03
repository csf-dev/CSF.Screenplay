# Implement `ICanReport`

When writing a [Performable] class, implement the [`ICanReport`] interface a well as the relevant performable interface.

This enables a performable to emit a formatted, human-readable fragment of a report for the current performable.

If you plan to redistribute your performables as a library then consider making your report fragments localizable

[Performable]: ../../glossary/Performable.md
[`ICanReport`]: xref:CSF.Screenplay.ICanReport