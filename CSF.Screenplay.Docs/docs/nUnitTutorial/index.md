# Screenplay & NUnit tutorial

Begin writing NUnit tests using Screenplay by following these steps.
Further detail is provided below.

1. Ensure that your test project uses [NUnit version 3.6.0] or higher
1. Install the NuGet package **[CSF.Screenplay.NUnit]** to your test project
1. Write a class which implements [`IGetsScreenplay`]
1. Decorate your test assembly with [`ScreenplayAssemblyAttribute`], referencing your implementation of `IGetsScreenplay`
1. Write your tests, decorating each test method with [`ScreenplayAttribute`]
1. Add parameters to your test methods to access the Screenplay architecture

> [!TIP]
> Developers are encouraged to read [these best practices for writing NUnit tests which use Screenplay].

[NUnit version 3.6.0]: https://www.nuget.org/packages/NUnit/3.6.0
[CSF.Screenplay.NUnit]: https://www.nuget.org/packages/CSF.Screenplay.NUnit
[`IGetsScreenplay`]: xref:CSF.Screenplay.IGetsScreenplay
[`ScreenplayAssemblyAttribute`]: xref:CSF.Screenplay.ScreenplayAssemblyAttribute
[`ScreenplayAttribute`]:xref:CSF.Screenplay.ScreenplayAttribute
[these best practices for writing NUnit tests which use Screenplay]: BestPractices.md

## Decorating your test assembly with `[ScreenplayAssembly]`

So that your tests may make use of a [`Screenplay`], you must install the Screenplay extension to the NUnit testing framework.
This is **steps 3 & 4** in the list above.
This is achieved using the [`ScreenplayAssemblyAttribute`].
Place a line of code somewhere in your test project, outside of any type definition like this:

```csharp
[assembly: CSF.Screenplay.ScreenplayAssembly(typeof(MyScreenplayFactory))]
```

There is one other thing you must do, and that is to write a screenplay factory class, which configures how the `Screenplay` should be created for your tests.
A screenplay factory is a class which must implement [`IGetsScreenplay`].
Consider the example below as a starting point for writing your own.

```csharp
using CSF.Screenplay;

public class MyScreenplayFactory : IGetsScreenplay
{
    public Screenplay GetScreenplay()
    {
        return new ScreenplayBuilder()
            .ConfigureServices(services => {
                // Add your own dependency injection service descriptors to the service collection here
                // For example, services which will be used by Screenplay Abilities.
            })
            .BuildScreenplay();
    }
}
```

> [!IMPORTANT]
> When using NUnit with Screenplay, every Screenplay-using test within a test assembly (thus, within a .NET project) must share the same instance of `Screenplay`.
> This is not expected to be problematic, as all the `Screenplay` object does is set-up the Screenplay architecture and dependency injection for the tests.

[`Screenplay`]: xref:CSF.Screenplay.Screenplay

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

    Assert.That(dishesCondition, Is.EqualTo("Clean));
}
```

[provided by dependency injection]: ../dependencyInjection/index.md
[the documentation for writing performables]: ../writingPerformables/index.md
