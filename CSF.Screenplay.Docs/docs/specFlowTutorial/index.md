# Screenplay & SpecFlow tutorial

Begin writing SpecFlow tests using Screenplay by following these steps.
Further detail is provided below.

1. Ensure that your test project uses [SpecFlow version 3.4.3] or higher
1. Install the NuGet package **[CSF.Screenplay.SpecFlow]** to the project which will contain your `.feature` files
1. _Optional:_ Add services to dependency injection which will be required by the [Abilities] you intend to use.  If required, [use SpecFlow context injection & hooks] to add these to the DI container.
1. Write step binding classes which dependency-inject and use Screenplay's architecture

[SpecFlow version 3.4.3]: https://www.nuget.org/packages/SpecFlow/3.4.3
[CSF.Screenplay.SpecFlow]: https://www.nuget.org/packages/CSF.Screenplay.SpecFlow
[Abilities]: ../../glossary/Ability.md
[use SpecFlow context injection & hooks]: https://docs.specflow.org/projects/specflow/en/latest/Bindings/Context-Injection.html#advanced-options

## Writing step bindings

> [!IMPORTANT]
> When using SpecFlow with Screenplay, every Screenplay-using test within a test assembly (thus, within a .NET project) must share the same instance of `Screenplay`.
> This is not expected to be problematic, as all the [`Screenplay`] object does is set-up the Screenplay architecture and dependency injection for the tests.

When using Screenplay with SpecFlow, `.feature` files are written as normal.
The only difference in writing your tests is that **Step Binding** classes should inject Screenplay architecture and use it within the bindings. 

The recommended services to inject into your step binding classes are either [`IStage`] or [`ICast`].
If you are using [Personas], which are the recommended way to get [Actors] for your performances, then most step binding classes will need only one of the two services above.
If you are not using Personas to get actors, then you might also need to inject some services which relate to the [Abilities] that you wish to grant actors.

[`Screenplay`]: xref:CSF.Screenplay.Screenplay
[`IStage`]: xref:CSF.Screenplay.IStage
[`ICast`]: xref:CSF.Screenplay.ICast
[Personas]: ../../glossary/Persona.md
[Actors]: xref:CSF.Screenplay.Actor

### Example

> [!TIP]
> The implied ability, the performables, persona and `DishwashingBuilder` used in this test, related to washing dishes, are all fictitious.
> See [the documentation for writing performables] to learn about how these could be written.

This example assumes that SpecFlow is writting using [the NUnit runner], and thus [it makes use of NUnit-style assertions].
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

[the documentation for writing performables]: ../writingPerformables/index.md
[the NUnit runner]: https://docs.specflow.org/projects/specflow/en/latest/Installation/Unit-Test-Providers.html
[it makes use of NUnit-style assertions]: https://docs.nunit.org/articles/nunit/writing-tests/assertions/assertion-models/constraint.html
