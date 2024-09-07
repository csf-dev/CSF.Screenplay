---
_layout: landing
---

# Screenplay pattern

Screenplay is a *software design pattern* to assist in the automation of complex processes.
It is particularly useful for writing tests which use a [Behaviour Driven Development] (BDD) style.
**CSF.Screenplay** is a library and framework for using this design pattern.

[Behaviour Driven Development]: https://en.wikipedia.org/wiki/Behavior-driven_development

## Learn about Screenplay's concepts

* [Makeup of a Performance](docs/MakeupOfAScreenplay.md)
* [How a Screenplay runs](docs/HowScreenplayAndPerformanceRelate.md)
* [Using dependency injection](docs/dependencyInjection/index.md)
* [Glossary of Screenplay terminology](glossary/index.md)

## As a testing tool

Screenplay is useful for directing [integration and system tests].
It may be used for testing web applications via a web browser.
In that context, Screenplay is a refinement of the [Page Object Pattern].

* [Screenplay in the testing stack](docs/ScreenplayInTheTestingStack.md)
* [NUnit & Screenplay tutorial](docs/nUnitTutorial/index.md)
* [SpecFlow & Screenplay tutorial](docs/specFlowTutorial/index.md)

[integration and system tests]: docs/SuitabilityAsATestingTool.md
[Page Object Pattern]: https://martinfowler.com/bliki/PageObject.html

## Using Screenplay

* [Writing performables, usually tasks](docs/writingPerformables/index.md)
* [Writing builders](docs/builderPattern/index.md)
* [Getting reports](docs/GettingReports.md)
* [Using pre-created Abilities & Performables](docs/performables/index.md)
* [Using Screenplay standalone](docs/StandaloneScreenplay.md)
