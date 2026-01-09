---
uid: IntegrationGlossaryItem
---

# Integration

An **Integration** refers to an integration library between the Screenplay library and a framework for performing automated tests.

The integration library performs the necessary scaffolding to ensure that [supported Screenplay types are available for dependency injection].
It also deals with the association of **[Scenarios]** with **[Performances]** and the lifetime of the whole **[Screenplay]**, culminating with the production of the **[Report]**.

[supported Screenplay types are available for dependency injection]: ../docs/dependencyInjection/InjectableServices.md
[Scenarios]: Scenario.md
[Performances]: xref:CSF.Screenplay.IPerformance
[Screenplay]: xref:CSF.Screenplay.Screenplay
[Report]: Report.md
