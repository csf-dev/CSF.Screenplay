---
uid: ScenarioGlossaryItem
---

# Scenario

A **Scenario** is a single test within a testing framework.
Testing frameworks differ between one another in the terminology that they use to name their individual tests.
Screenplay uses the name "Scenario" because this [matches the name used in Cucumber].
Scenarios _are only relevant_ when Screenplay is used in conjunction with a [test framework integration].
It is irrelevant when [using Screenplay standalone].

A Scenario represents the lifetime of each individual test; when using Screenplay as a testing tool, the lifetime of the Scenario corresponds _very closely_ to the lifetime of a **[Performance]**.
When using Screenplay, each Performance (and thus Scenario) is executed [within its own Dependency Injection Lifetime Scope].

Every scenario is typically contained within a **[Feature]**.
The structure of scenarios-within-features may be represented in the [Performance] using the [`NamingHierarchy`].

## Other names for Scenarios

Various testing frameworks have established different naming conventions for what is - fundamentally - the same thing.
Here are some other names which you might see in different testing frameworks.

* Test
* Test case
* Theory
* Example

[matches the name used in Cucumber]: https://cucumber.io/docs/gherkin/reference/#examples
[test framework integration]: Integration.md
[using Screenplay standalone]: ../docs/StandaloneScreenplay.md
[Performance]: xref:CSF.Screenplay.IPerformance
[within its own Dependency Injection Lifetime Scope]: ../docs/dependencyInjection/DependencyInjectionScope.md
[Feature]: Feature.md
[`NamingHierarchy`]: xref:CSF.Screenplay.IPerformance.NamingHierarchy
