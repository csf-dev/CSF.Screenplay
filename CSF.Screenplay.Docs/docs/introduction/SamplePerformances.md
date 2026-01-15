# Sample performances

Let's take a look at what a Performance looks like.
The following is written in C#, as a test, using [the NUnit syntax], using the **CSF.Screenplay.Selenium** extension.
This was selected to demonstrate a variety of Screenplay's features, used in a manner that's close to a real-world scenario.
NUnit is a popular testing framework, recognisable to most .NET developers.

[the NUnit syntax]: https://nunit.org/

```csharp
using static CSF.Screenplay.PerformanceStarter;
using static MyAppNamespace.ShoppingCartBuilder;

[Test, Screenplay]
public async Task TheQuantityOfAnItemShouldBeIncrementedAfterAddingAnother(ICast cast)
{
    var webster = cast.GetActor<Webster>();

    await Given(webster).WasAbleTo(OpenTheAppWithAnEmptyShoppingCart());
    await Given(webster).WasAbleTo(AddAProductToTheirCartNamed("Blue widget"));
    await When(webster).AttemptsTo(AddAProductToTheirCartNamed("Blue widget"));
    var quantity = await Then(webster).Should(ReadTheQuantityOfItemsInTheirCartNamed("Blue widget"));

    Assert.That(quantity, Is.EqualTo(2));
}

[Test, Screenplay]
public async Task TheTotalValueOfTheCartShouldEqualTheQuantityMultipliedByUnitPrice(ICast cast)
{
    var admira = cast.GetActor<Admira>();
    var webster = cast.GetActor<Webster>();

    await Given(admira).WasAbleTo(SetThePriceOfAProduct("Blue widget", "$4.99"));
    await Given(webster).WasAbleTo(OpenTheAppWithAnEmptyShoppingCart());
    await Given(webster).WasAbleTo(AddAProductToTheirCartNamed("Blue widget"));
    await When(webster).AttemptsTo(AddAProductToTheirCartNamed("Blue widget"));
    var quantity = await Then(webster).Should(ReadTheTotalValueOfTheirCart());

    Assert.That(quantity, Is.EqualTo("$9.98"));
}
```

## Notable features

Let's look a little more deeply into these two performances.
The headings below point out how they differ from normal NUnit tests; these differences are the features of Screenplay.

### Static usings for builders

At the very beginning of the code listing are two `using static` declarations.
Screenplay relies heavily on [the builder pattern], including static builders, in order to provide [a fluent interface].
The static builder [`PerformanceStarter`] is common to Screenplay when using tests; the second is fictious.
`ShoppingCartBuilder` is provided as an example of a builder that the author of these tests might have written for their own performables.

[the builder pattern]: https://en.wikipedia.org/wiki/Builder_pattern
[a fluent interface]: https://en.wikipedia.org/wiki/Fluent_interface
[`PerformanceStarter`]: xref:CSF.Screenplay.PerformanceStarter

### The `[Screenplay]` attribute

When using Screenplay with NUnit, each test method must have [the `[Screenplay]` attribute](xref:CSF.Screenplay.ScreenplayAttribute).
This requirement is specific to the NUnit integration for Screenplay.

### The `ICast` parameter

The **Cast** is one of the mechanisms by which the Performance logic may access **Actors**.
Just like in a real-life theater performance, the Cast is a collection of the Actors who are involved.
Part of the role of the `[Screenplay]` attribute (above) is to provide an appropriate implementation of the Cast object to NUnit, so that it may be 'parameter injected' into tests as shown here.

### The strongly-typed Actors

Notice how when the cast is used to get actors, it uses a class with the actor's name as the generic type parameter to `GetActor`?
This is [a technique called a Persona]; personas are the best way to define Actors.
The logic which grants/configures the Actors' **Abilities** is contained within a reusable Persona class.
This way the same-named actor has the same abilities across every Performance in the Screenplay, making them easy to recognise and understand.

Good Personas/Actors have names which remind you of their role in the Performance.
In this case **Admira** is an Administrator, who can edit the prices of items for sale in the app.
**Webster** is someone browsing the web, who is otherwise unremarkable.

[a technique called a Persona]: ../../glossary/Persona.md

### The Given/When/Then methods

When writing a test, Given/When/Then (aka Arrange/Act/Assert) is a popular way to organise test logic.
This is available in Screenplay and (if using it for tests) developers are encouraged to use it to separate set-up logic, the logic which conducts the test, and logic which leads to an assertion.

These three methods (which take an Actor as a parameter) are followed-up with either a `WasAbleTo`, `AttemptsTo` or `Should` method.
These three follow-up methods are synomymous and none have any limitations or functionality different from the others.
They are named differently only to improve the readability of the test logic.

### The performables

There are a number of Performables used in these two performances.
All the performables listed above would be **Tasks**, because they are high-level verbs which are written in terms of the application being tested.

To use syntax like this, it is assumed that there are some static factory/builder methods on a (fictitious) class named `ShoppingCartBuilder`, accessible without the class name via the `using static` declaration (above).
The Performables included in these tests are:

* `OpenTheAppWithAnEmptyShoppingCart`
* `AddAProductToTheirCartNamed`
* `ReadTheQuantityOfItemsInTheirCartNamed`
* `SetThePriceOfAProduct`
* `ReadTheTotalValueOfTheirCart`

Just by reading the code, we can already see that:

* Some of these Performables can accept parameters
* Some of these Performables can return results

Notice how each of these tasks is named in a manner which makes it very clear and obvious what they accomplish.
Each name begins with a verb, and their name would make sense to both developers and non-developers alike, assuming they are at least familiar with the application/[domain of logic] in question.

[domain of logic]: https://en.wikipedia.org/wiki/Domain_(software_engineering)

### The assertions

Notice that the assertions at the end of each test is _a normal NUnit assertion_.
When writing tests using Screenplay it is best practice to place any assertions _outside of any Performables_; write it at the end of the Performance (end of the test) just like any other test.

## Next: Let's see some of those Tasks

[Read page 4, in which we take two of the Tasks from this example and examine their code](SampleTasks.md)
