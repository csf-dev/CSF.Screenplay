# Parameter injection for tests
For testing frameworks which are based on **test methods** such as [NUnit], services are injected via _method parameter injection_. This is performed directly into each test method which is decorated with the `[Screenplay]` attribute. 

[If Screenplay were to be extended] to work with frameworks such as xUnit or MSTest, then this same technique would be used.

Use dependencies injected in this way to get access to [commonly-used Screenplay services] and anything else required at the root level of your test logic.

[NUnit]: https://nunit.org/
[If Screenplay were to be extended]: ../extendingScreenplay/TestIntegrations.md
[commonly-used Screenplay services]: InjectableServices.md