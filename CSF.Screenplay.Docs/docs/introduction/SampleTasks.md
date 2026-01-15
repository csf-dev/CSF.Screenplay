# Sample tasks

This introductory documentation [continues the example from the previous page].
On that page we looked at two sample Performances and what their code might look like.
In this page we consider the logic of two **Task** classes.
Recall, a Task is a piece of performable logic which may compose any of Actions, Questions and/or other Tasks.

[continues the example from the previous page]: SamplePerformances.md

## Adding an item to the shopping cart

The first task we will consider is the Task which was named `AddAProductToTheirCartNamed` on the previous page.

```csharp
public static class SitewideUi
{
    public static readonly Locator ProductSearchTextbox
        = ElementId("product-search", "the product search");
    public static readonly Locator ProductSearchNowButton
        = ElementId("execute-product-search", "the search button");
}

public static class ProductSearchResultsPage
{
    public static readonly Locator AddToCartButtonForProductNamed(string productName)
        => CssSelector($"ol.search-results li[data-productName='{productName}'] .addToCart");
}

public class AddAProductToTheirCartNamed(string productName) : IPerformable, ICanReport
{
    public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    {
        await actor.PerformAsync(EnterTheText(productName).Into(SitewideUi.ProductSearchTextbox), cancellationToken);
        await actor.PerformAsync(ClickOn(SitewideUi.ProductSearchNowButton), cancellationToken);
        var addToCartButton = ProductSearchResultsPage.AddToCartButtonForProductNamed(productName);
        await actor.PerformAsync(ClickOn(addToCartButton), cancellationToken);
    }

    public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
        => formatter.Format("{Actor} searches for '{ProductName}' and adds one to their cart", actor, productName);
}
```

## Reading the cart's total value

Now let's consider some code which might form the logic of the Task named `ReadTheTotalValueOfTheirCart`.

```csharp
public static class ShoppingCartPage
{
    public static readonly Locator TotalCartValue
        = ElementId("cart-value", "the total cart value");
}

public class ReadTheTotalValueOfTheirCart : IPerformableWithResult<string>, ICanReport
{
    public async ValueTask<string> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    {
        await actor.PerformAsync(NavigateToTheirShoppingCart(), cancellationToken);
        return await actor.PerformAsync(ReadFromTheElement(ShoppingCartPage.TotalCartValue).TheText(), cancellationToken);
    }

    public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
        => formatter.Format("{Actor} reads the total value of their cart", actor, productName);
}
```

## Inspecting these two Tasks

Now, we will disect the code of these two sample Tasks, to see what we can learn from them.

### Tasks coordinate other Performables

Common to both tasks is that they make prolific use of `actor.PerformAsync`.
This is the mechanism by which Task classes compose other Performables, which may include any or all of Actions, Questions or other Tasks.
These consumed Performables are built in the same manner, using the same builders and/or static factories, as if they were being used directly from the Performance logic.

As a side-note, the first of these sample Tasks is built using only **Actions**.
The [`EnterTheText`] and [`ClickOn`] builder methods create Actions, which are built into the CSF.Screenplay.Selenium Extension.
The second sample Task makes use of a **Question** from the same Extension.
[`ReadFromTheElement`] followed by `TheText` gets a Question which reads text from the web browser screen.

On the other hand, the second sample Task makes use of (a fictitious) Task as well: `NavigateToTheirShoppingCart`.
If this were a real project then the developer would have written that Task also, including whatever logic is required to navigate the current user to their shopping cart screen.

> [!NOTE]
> The same `PerformAsync` method is used to consume Actions, Questions or Tasks.
> So, Tasks may consume _any kind_ of Performable.

[`EnterTheText`]: xref:CSF.Screenplay.Selenium.PerformableBuilder.EnterTheText(System.String[])
[`ClickOn`]: xref:CSF.Screenplay.Selenium.PerformableBuilder.ClickOn(CSF.Screenplay.Selenium.Elements.ITarget)
[`ReadFromTheElement`]: xref:CSF.Screenplay.Selenium.PerformableBuilder.ReadFromTheElement(CSF.Screenplay.Selenium.Elements.ITarget)

### Always implement `ICanReport`, if you can

Notice how each of the Task classes also implements `ICanReport`?
One of the features/benefits of Screenplay which we have not yet touched upon is its ability to produce detailed human-readable **Reports** of the Performances.
By implementing `ICanReport`, you may decide upon the human-readable report text that the current Task emits to describe itself.

### Tasks may be built on assumptions

Notice that in the Performance in which it was used, the `AddAProductToTheirCartNamed` task was preceded by a task named `OpenTheAppWithAnEmptyShoppingCart`.
You might infer from this that - if the current Actor had not already navigated to the web application, the Add a Product task would not function correctly.
If you did, that's completely correct.
The Add a Product task assumes that the current Actor (a user of the shopping web app) already has their browser open somewhere on the app.

> [!NOTE]
> It's perfectly reasonable, recommended in fact, for Tasks to use _implicit assumptions about the state of the application_.
> It's encouraged to document these, to aid reusability.

### Well-known parameters are held in static classes

Notice the static classes which are used alongside these tasks.
Each provides some Locators which specify some HTML elements on the screen.
Locators and HTML elements are specific to the CSF.Screenplay.Selenium Extension, but the principle is the same regardless of the extension.

If there's a well-known value, a URL, a file system path, etc, then represent this value in your code.  This way it may be referenced by builders and the fluent design of Screenplay Performables.

## Next: Try Screenplay out for yourself

This concludes the introduction to Screenplay.
The next steps are to [try using Screenplay for yourself].

[try using Screenplay for yourself]: ../usingScreenplay/index.md
