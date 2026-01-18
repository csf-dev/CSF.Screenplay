# CSF.Screenplay.SpecFlow

**CSF.Screenplay.SpecFlow** is a [Test Framework Integration] which allows Screenplay-based logic/syntax to be used with tests written for the [legacy SpecFlow, *which is now retired*].

Learn more at the [Getting Started with Reqnroll documentation page].
[Reqnroll] is the maintained fork of SpecFlow, supported by its original authors.

At present, documentation for Reqnroll is equally applicable to SpecFlow.
Nevertheless, it is recommended to migrate SpecFlow projects to Reqnroll ASAP.
It is not assured that extensibility will remain compatible between the two projects.
Maintaining support for SpecFlow is not a priority for CSF.Screenplay; *support may end as Reqnroll diverges further*.

[Test Framework Integration]: https://csf-dev.github.io/CSF.Screenplay/glossary/Integration.html
[legacy SpecFlow, *which is now retired*]: https://reqnroll.net/news/2025/01/specflow-end-of-life-has-been-announced/
[Getting Started with Reqnroll documentation page]: https://csf-dev.github.io/CSF.Screenplay/docs/gettingStarted/reqnroll/index.html
[Reqnroll]: https://reqnroll.net/

## Source code note

Browsing the repository/source code for this package/library, you will notice that there is none!
The source code for this Test Framework Integration and [CSF.Screenplay.Reqnroll] are identical, and is maintained in the CSF.Screenplay.Reqnroll project.

The compiler symbol `SPECFLOW` is used to identify the small number of places where code differences are required to cater for SpecFlow.
These are limited to minor differences in naming (primarily .NET namespaces).

[CSF.Screenplay.Reqnroll]: https://www.nuget.org/packages/CSF.Screenplay.Reqnroll
