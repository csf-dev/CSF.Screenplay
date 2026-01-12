---
_layout: landing
---

# Screenplay pattern for .NET

<div class="landingCodeSample">

```csharp
var webster = stage.Spotlight<Webster>();

await Given(webster).WasAbleTo(OpenTheUrl(shoppingCartPage));
await When(webster).AttemptsTo(RemoveTheItemFromTheirCartNamed("Widgets"));
await When(webster).AttemptsTo(SaveTheirShoppingCart());

var total = await Then(webster).Should(ReadTheTotalValueOfTheirCart());
```

</div>

Screenplay is a _software design pattern_ to script the automation of complex processes.
It is popular for writing <a href="https://en.wikipedia.org/wiki/Behavior-driven_development" title="Behaviour Driven Development">BDD-style</a> tests.
**CSF.Screenplay** is a library and framework for the Screenplay pattern.

<div class="container">
<ul class="landingNextSteps row">
<li class="introduction col">

[What is Screenplay?]

</li>
<li class="capabilities col">

[What can Screenplay do?]

</li>
<li class="running col">

[How do I use Screenplay?]

</li>
</ul>
</div>

[What is Screenplay?]: docs/introduction/index.md
[How do I use Screenplay?]: docs/usingScreenplay/index.md
[What can Screenplay do?]: docs/capabilities/index.md

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
