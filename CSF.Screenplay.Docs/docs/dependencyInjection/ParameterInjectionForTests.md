# Parameter injection for tests

For testing frameworks which are based on **test methods** such as [NUnit], services are injected via _method parameter injection_. This is performed directly into each test method which is decorated with the `[Screenplay]` attribute.
Use dependencies injected in this way to get access to [commonly-used Screenplay services] and anything else required at the root level of your test logic.

[If Screenplay were to be extended] to work with frameworks such as xUnit or MSTest, then this same technique would be used.

[NUnit]: https://nunit.org/
[If Screenplay were to be extended]: ../extendingScreenplay/TestIntegrations.md
[commonly-used Screenplay services]: InjectableServices.md

## Example

This is an example of an NUnit-style test method with a parameter-injected dependency.
This would require [installing the NUnit test integration] to run.
Take note of the Screenplay attribute and the `cast` parameter.

```csharp
[Test, Screenplay]
public async Task TheSampleTestShouldBeInformative(ICast cast)
{
    var janeSmith = cast.GetActor<JaneSmith>();

    // ... use the actor with some performables, then make assertions etc
}
```

[installing the NUnit test integration]: ../gettingStarted/nunit3/index.md
