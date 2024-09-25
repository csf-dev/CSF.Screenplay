# Screenplay & SpecFlow tutorial

Begin writing SpecFlow tests using Screenplay by following these steps.
Further detail is provided below.

1. Ensure that your test project uses [SpecFlow version 3.4.3] or higher
1. Install the NuGet package **[CSF.Screenplay.SpecFlow]** to the project which will contain your `.feature` files
1. _Optional:_ Add services which are relevant to Abilities you intend to use to dependency injection, via a binding class
1. Write step binding classes which dependency-inject and use Screenplay's architecture

[SpecFlow version 3.4.3]: https://www.nuget.org/packages/SpecFlow/3.4.3
[CSF.Screenplay.SpecFlow]: https://www.nuget.org/packages/CSF.Screenplay.SpecFlow

---

TODO: Below this line is copy-paste from the NUnit tutorial so far.

> [!IMPORTANT]
> When using SpecFlow with Screenplay, every Screenplay-using test within a test assembly (thus, within a .NET project) must share the same instance of `Screenplay`.
> This is not expected to be problematic, as all the [`Screenplay`] object does is set-up the Screenplay architecture and dependency injection for the tests.

[`Screenplay`]: xref:CSF.Screenplay.Screenplay

## Writing step bindings

When using Screenplay with SpecFlow, `.feature` files are written as normal.
The only difference in writing your tests is that **Step Binding** classes should inject Screenplay architecture and use it within the bindings. 

Recommended services to consider injecting into your bindings are: 

* Either [`IStage`] or [`ICast`]

> [!TIP]
> The ability, performables and `DishwashingBuilder` used in this test, related to washing dishes, are all fictitious.
> See [the documentation for writing performables] to learn about how these could be written.

```csharp
using CSF.Screenplay;
using static CSF.Screenplay.PerformanceStarter;
using static WashingTheDishes.DishwashingBuilder;

[Test, Screenplay]
public async Task TheDishesShouldBeCleanAfterJoeWashesThem(ICast cast, IDishWashingAbility washTheDishes)
{
    var joe = cast.GetActor("Joe");
    joe.IsAbleTo(washTheDishes);

    await Given(joe).WasAbleTo(GetSomeDirtyDishes());
    await When(joe).AttemptsTo(WashTheDishes());
    var dishesCondition = await Then(joe).Should(LookAtTheDishesCondition());

    Assert.That(dishesCondition, Is.EqualTo("Clean"));
}
```

[provided by dependency injection]: ../dependencyInjection/index.md
[the documentation for writing performables]: ../writingPerformables/index.md