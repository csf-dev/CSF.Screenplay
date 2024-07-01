---
_layout: landing
---

Screenplay is a *software design pattern* to assist in the automation of complex processes.
It is commonly recommended for use in writing tests using the [Behaviour Driven Development] (BDD) style.
**CSF.Screenplay** is a library and framework for using this design pattern.

## As a testing tool

Screenplay is useful for directing [integration and acceptance tests].
It is commonly used/recommended for testing web applications via a web browser. 
In that context, Screenplay is a refinement of the [Page Object Pattern].

CSF.Screenplay provides packages which are [consumed by commonly-used testing frameworks] as plugins.

[integration and acceptance tests]: docs/AsATestingTool.md
[Page Object Pattern]: https://martinfowler.com/bliki/PageObject.html
[consumed by commonly-used testing frameworks]: docs/TestFrameworkIntegrations.md

The Screenplay libraries are used as [plugins to a testing framework], such as [SpecFlow] (recommended) or [NUnit]. Test logic is written and composed using [Screenplay's building blocks], which in-turn control the system/software under test.

As well as improving the reusability of test logic, Screenplay also produces [detailed reports] from the test execution. These reports, which may be converted into a number of formats, provide a step-by-step breakdown of what happened in the scenario, allowing quick diagnosis of failures.

[Behaviour Driven Development]: https://en.wikipedia.org/wiki/Behavior-driven_development

[plugins to a testing framework]: docs/ScreenplayInTheTestingStack.md
[Screenplay's building blocks]: docs/MakeupOfAScreenplayTest.md
[NUnit]: http://nunit.org/
[SpecFlow]: http://specflow.org/
[detailed reports]: docs/GettingReports.md


## Tutorial: Your first test
There are two step-by-step tutorials available for writing your first Screenplay tests:
* [Read the NUnit tutorial]
* [Read the SpecFlow tutorial]

## Reference
Once you have the basics, [the Screenplay reference] provides more information about how specific APIs are used.

[Read the NUnit tutorial]: docs/NUnitTutorial.md
[Read the SpecFlow tutorial]: docs/SpecFlowTutorial.md
[the Screenplay reference]: docs/ScreenplayReference.md