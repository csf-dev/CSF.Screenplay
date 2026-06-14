# Screenplay & Reqnroll tutorial

> [!TIP]
> Are you using the legacy **SpecFlow**?
> [Reqnroll is the maintained fork of SpecFlow], so it's recommended you upgrade your projects ASAP.
> See [the documentation on using CSF.Screenplay with SpecFlow] for more information.

Begin writing Reqnroll tests using Screenplay by following these steps.
Further detail is provided below.

1. Ensure that your test project uses [Reqnroll version 2.0.0] or higher
1. Install the NuGet package **[CSF.Screenplay.Reqnroll]** to the project which will contain your `.feature` files
1. Configure dependency injection with any services you intend to use in your tests
1. Write step binding classes which dependency-inject and use Screenplay's architecture

[Reqnroll is the maintained fork of SpecFlow]: https://reqnroll.net/news/2025/01/specflow-end-of-life-has-been-announced/
[the documentation on using CSF.Screenplay with SpecFlow]: Specflow.md
[Reqnroll version 2.0.0]: https://www.nuget.org/packages/Reqnroll/2.0.0
[CSF.Screenplay.Reqnroll]: https://www.nuget.org/packages/CSF.Screenplay.Reqnroll
[Abilities]: ../../../glossary/Ability.md

## Step 3: Configuring dependency injection

Reqnroll has a built-in mechanism which offers an opportunity to add services to its DI container; these are [the event hook attributes].
This is a viable technique because [Reqnroll's internal DI container "BoDi"] is unusual in that it permits retrospective addition of services.

Use this technique by writing a small binding class like the following, replacing the comment with your DI registrations.
There's no need to use `.AddScreenplay()` here, _the integration does that already_.
For example, to add [the Screenplay/Selenium extension], add a line like `.AddSelenium()`.

```csharp
[Binding]
public class DependenciesSetup(IObjectContainer reqnrollContainer)
{
    // Or [BeforeTestRun] or [BeforeFeature]
    [BeforeScenario]
    public void AddExtraDependencies()
    {
        reqnrollContainer.ToServiceCollection()
            // Add your own dependency injection service descriptors to the service collection here
            // For example, services which will be used by Screenplay Abilities.
            ;
    }
}
```

> [!IMPORTANT]
> When using Reqnroll with Screenplay, every Screenplay-using test within a test assembly (thus, within a .NET project) must share the same instance of `Screenplay`.
> This is not expected to be problematic, as all the [`Screenplay`] object does is set-up the Screenplay architecture and dependency injection for the tests.

[the event hook attributes]: https://docs.reqnroll.net/latest/automation/context-injection.html#advanced-options
[Reqnroll's internal DI container "BoDi"]: https://github.com/reqnroll/Reqnroll/tree/main/Reqnroll/BoDi
[the Screenplay/Selenium extension]: ../../extensions/selenium/index.md

## Step 4: Writing step bindings

When using Screenplay with Reqnroll, `.feature` files are written as normal.
The only difference in writing your tests is that **Step Binding** classes should inject Screenplay architecture and use it within the bindings. 

The recommended services to inject into your step binding classes are either [`IStage`] or [`ICast`].
If you are using [Personas], which are the recommended way to get [Actors] for your performances, then most step binding classes will need only one of the two services above.
If you are not using Personas to get actors, then you might also need to inject some services which relate to the [Abilities] that you wish to grant actors.

[`Screenplay`]: xref:CSF.Screenplay.Screenplay
[`IStage`]: xref:CSF.Screenplay.IStage
[`ICast`]: xref:CSF.Screenplay.ICast
[Personas]: ../../../glossary/Persona.md
[Actors]: xref:CSF.Screenplay.Actor

### Example

> [!TIP]
> The implied ability, the performables, persona and `DishwashingBuilder` used in this test, related to washing dishes, are all fictitious.
> See [the documentation for writing performables] to learn about how these could be written.

This example assumes that Reqnroll is writting using [the NUnit runner], and thus [it makes use of NUnit-style assertions].
Feel free to replace the assertion _with whichever assertion library you wish to use_.

```csharp
using CSF.Screenplay;
using static CSF.Screenplay.PerformanceStarter;
using static WashingTheDishes.DishwashingBuilder;

[Binding]
public class WashTheDishesSteps(IStage stage)
{
    [Given(@"^Joe was able to get some dirty dishes$")]
    public async Task GetDirtyDishes()
    {
        var joe = stage.Spotlight<Joe>();
        await Given(joe).WasAbleTo(GetSomeDirtyDishes());
    }

    [When(@"^(?:he|she|they) attempts? to wash the dishes$")]
    public async Task WashTheDishes()
    {
        var actor = stage.GetSpotlitActor();
        await When(actor).AttemptsTo(WashTheDishes());
    }

    [Then(@"^(?:he|she|they) should see that the dishes are clean$")]
    public async Task GetDirtyDishes()
    {
        var actor = stage.GetSpotlitActor();
        var condition = await Then(actor).Should(LookAtTheDishesCondition());

        Assert.That(condition, Is.EqualTo("Clean"));
    }
}
```

[the documentation for writing performables]: ../../writingPerformables/index.md
[the NUnit runner]: https://docs.reqnroll.net/latest/integrations/nunit.html
[it makes use of NUnit-style assertions]: https://docs.nunit.org/articles/nunit/writing-tests/assertions/assertion-models/constraint.html
