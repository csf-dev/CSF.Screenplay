# Screenplay reports

As [a Screenplay] executes, the framework produces and saves a JSON-format report file, recording each [Performance].
This file is a machine-readable record of every [Actor] and every [Performable] which was included in the performances.
It records the results from the performables (where applicable) as well as the details of errors, where they occur.

[a Screenplay]: xref:CSF.Screenplay.Screenplay
[Performance]: xref:CSF.Screenplay.Performance
[Actor]: xref:CSF.Screenplay.Actor
[Performable]: ../glossary/Performable.md

## Where the report JSON file is saved

Over the course of a Screenplay, the JSON file will be written and saved at a path defined by the [`ScreenplayOptions.ReportPath`] option.
As the documentation for that option notes, the default if it has not been set is to write the report to a file in the current working directory.
The default filename includes a representation of the current timestamp, so each report is likely to have a unique filename.
This path may be altered by specifying a different set of options to the [`AddScreenplay`] service collection extension method.

[`ScreenplayOptions.ReportPath`]: xref:CSF.Screenplay.ScreenplayOptions.ReportPath
[`AddScreenplay`]: xref:CSF.Screenplay.ScreenplayServiceCollectionExtensions.AddScreenplay(Microsoft.Extensions.DependencyInjection.IServiceCollection,System.Action{CSF.Screenplay.ScreenplayOptions})

## Converting JSON to a human-readable report

JSON is a very good machine-readable format for reports, but it's not a great choice for reading by humans.
CSF.Screenplay offers a utility to convert a JSON-format report into a human-readable single-page HTML file, viewable in any modern web browser.
This tool is the **CSF.Screenplay.JsonToHtmlReport** command-line utility.

The command-line options to this utility are documented in the class [`ReportConverterOptions`].
They should be passed using the dobule-dash syntax, for example `CSF.Screenplay.JsonToHtmlReport.exe --ReportPath c:\path\subfolder\screenplay-report.json`.

[`ReportConverterOptions`]: xref:CSF.Screenplay.JsonToHtmlReport.ReportConverterOptions
