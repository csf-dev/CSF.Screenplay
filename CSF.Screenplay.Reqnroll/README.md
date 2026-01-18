# CSF.Screenplay.Reqnroll

**CSF.Screenplay.Reqnroll** is a [Test Framework Integration] which allows Screenplay-based logic/syntax to be used with tests written for [Reqnroll], which is the maintained fork of an older project named SpecFlow.

Learn more at the [Getting Started with Reqnroll documentation page].

[Test Framework Integration]: https://csf-dev.github.io/CSF.Screenplay/glossary/Integration.html
[Reqnroll]: https://reqnroll.net/
[Getting Started with Reqnroll documentation page]: https://csf-dev.github.io/CSF.Screenplay/docs/gettingStarted/reqnroll/index.html

## Source code note

The source code for this library/project is shared with [CSF.Screenplay.SpecFlow].
The SpecFlow project has no distinct source code of its own, instead linking to this project's sources.

The compiler symbol `SPECFLOW` is used to identify the small number of places where code differences are required to cater for SpecFlow.
These are limited to minor differences in naming (primarily .NET namespaces).

[CSF.Screenplay.SpecFlow]: https://www.nuget.org/packages/CSF.Screenplay.SpecFlow
