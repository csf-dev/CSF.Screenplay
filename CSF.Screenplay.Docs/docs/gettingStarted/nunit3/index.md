---
uid: NUnit3GettingStartedArticle
---

# Screenplay & NUnit tutorial

Begin writing NUnit tests using Screenplay by following these steps.
Further detail is provided below.

1. Ensure that your test project uses [NUnit version 3.6.0] or higher
1. Install the NuGet package **[CSF.Screenplay.NUnit]** to your test project
1. Write a Screenplay Factory class, implementing [`IGetsScreenplay`]
1. Decorate your test assembly with [`ScreenplayAssemblyAttribute`], referencing that Screenplay Factory
1. Write your tests, decorating each test method with [`ScreenplayAttribute`]
1. Add parameters to your test methods to access the Screenplay architecture

> [!TIP]
> Developers are encouraged to read [these best practices for writing tests which use Screenplay].

[NUnit version 3.6.0]: https://www.nuget.org/packages/NUnit/3.6.0
[CSF.Screenplay.NUnit]: https://www.nuget.org/packages/CSF.Screenplay.NUnit
[`IGetsScreenplay`]: xref:CSF.Screenplay.IGetsScreenplay
[`ScreenplayAssemblyAttribute`]: xref:CSF.Screenplay.ScreenplayAssemblyAttribute
[`ScreenplayAttribute`]:xref:CSF.Screenplay.ScreenplayAttribute
[these best practices for writing tests which use Screenplay]: ../../bestPractice/WritingTests.md

## Step 3: Write a Screenplay factory

NUnit requires a factory class which implements [`IGetsScreenplay`].
This tells the underlying framework how to create and configure the `Screenplay` for your tests.
This factory class is user-customisable, because it is where you would configure **[dependency injection]** for your own Screenplay.

The example below shows a minimal template for a factory; replace the commment with DI service registrations for anything that you would like to be available to Screenplay.
There's no need to use `.AddScreenplay()` here, _the integration does that already_.
For example, to add [the Screenplay/Selenium extension], add `services.AddSelenium()` here.

> [!IMPORTANT]
> The Screenplay Factory class _must have a parameterless constructor_.

```csharp
using CSF.Screenplay;

public class MyScreenplayFactory : IGetsScreenplay
{
    public Screenplay GetScreenplay()
    {
        return Screenplay.Create(services => {
                // Add your own dependency injection service descriptors to the service collection here
                // For example, services which will be used by Screenplay Abilities.
            });
    }
}
```

[dependency injection]: ../../dependencyInjection/index.md
[the Screenplay/Selenium extension]: ../../extensions/selenium/index.md

## Step 4: Decorate your test assembly with `[ScreenplayAssembly]`

One line of boilerplate code is required per test assembly (project).
Its purpose is to signpost the Screenplay Factory which you wrote in step 3 (above) to the NUnit/Screenplay integration.

Place a line of code like the following in your test project.
This should be outside of any type; it may go into its own source file if you wish.
Be sure to replace `MyScreenplayFactory` with the factory class your wrote in step 3.
See the docs for [`ScreenplayAssemblyAttribute`] for more info.

```csharp
[assembly: CSF.Screenplay.ScreenplayAssembly(typeof(MyScreenplayFactory))]
```

> [!IMPORTANT]
> When using NUnit with Screenplay, every Screenplay-using test within a test assembly (thus, within a .NET project) must share the same instance of `Screenplay`.
> This is not expected to be problematic, as all the `Screenplay` object does is set-up the Screenplay architecture and dependency injection for the tests.

## Writing test methods

When writing test methods, the test methods must be decorated with [`ScreenplayAttribute`], which activates Screenplay for that particular test method.
NUnit test methods which are decorated with `[Screenplay]` may have test parameters; these parameters will be [provided by dependency injection].
This covers **steps 5 & 6** in the list above.
Here is an example of an NUnit test method which is written using Screenplay; it assumes that the assembly has been decorated with [`ScreenplayAssemblyAttribute`], as noted above.

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

[provided by dependency injection]: ../../dependencyInjection/index.md
[the documentation for writing performables]: ../../writingPerformables/index.md