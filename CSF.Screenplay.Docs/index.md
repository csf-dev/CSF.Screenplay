---
_layout: landing
---

# Screenplay pattern

Screenplay is a *software design pattern* to assist in the automation of complex processes.
It is commonly recommended for use in writing tests using the [Behaviour Driven Development] (BDD) style.
**CSF.Screenplay** is a library and framework for using this design pattern.

[Behaviour Driven Development]: https://en.wikipedia.org/wiki/Behavior-driven_development

## As a testing tool

Screenplay is useful for directing [integration and system tests].
It is commonly used/recommended for testing web applications via a web browser.
In that context, Screenplay is a refinement of the [Page Object Pattern].

The CSF.Screenplay libraries [act as a plugin to a chosen testing framework].
At this time there are [integrations available for SpecFlow and for NUnit].

[integration and system tests]: docs/SuitabilityAsATestingTool.md
[Page Object Pattern]: https://martinfowler.com/bliki/PageObject.html
[act as a plugin to a chosen testing framework]: docs/ScreenplayInTheTestingStack.md
[integrations available for SpecFlow and for NUnit]: docs/TestFrameworkIntegrations.md

## Using Screenplay

Screenplay logic is written and composed using [Screenplay's building blocks], which in-turn control the software.
As well as improving the reusability of such logic, Screenplay also produces [detailed reports].
These reports provide step-by-step breakdowns, allowing quick analysis and diagnosis.
Reports may also be converted to a number of formats.

[Screenplay's building blocks]: docs/MakeupOfAScreenplay.md
[detailed reports]: docs/GettingReports.md

## Tutorial: Your first test

There are two step-by-step tutorials available for writing your first Screenplay test.

* [Read the NUnit tutorial]
* [Read the SpecFlow tutorial]

[Read the NUnit tutorial]: docs/nUnitTutorial/index.md
[Read the SpecFlow tutorial]: docs/specFlowTutorial/index.md

## Reference

Once you understand the essentials, [the Screenplay reference] provides more information about how specific APIs are used.
You may also be interested in [the glossary of Screenplay terminology].

[the Screenplay reference]: docs/screenplayReference/index.md
[the glossary of Screenplay terminology]: glossary/index.md
