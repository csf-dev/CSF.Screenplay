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
[What can Screenplay do?]: docs/Capabilities.md
[How do I use Screenplay?]: docs/index.md
