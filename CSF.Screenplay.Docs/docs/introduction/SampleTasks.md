# Sample tasks

This introductory documentation [continues the example from the previous page].
On that page we looked at two sample Performances and what their code might look like.
In this page we consider the logic of two **Task** classes.
Recall, a Task is a piece of performable logic which may compose any of Actions, Questions and/or other Tasks.

[continues the example from the previous page]: SamplePerformances.md

## Adding an item to the shopping cart

The first task we will consider is the Task which was named `AddAProductToTheirCartNamed` on the previous page.

Notice that in the Performance in which it was used, the `AddAProductToTheirCartNamed` task was preceded by a task named `OpenTheAppWithAnEmptyShoppingCart`.
You might infer from this that - if the current Actor had not already navigated to the web application, the Add a Product task would not function correctly.
If you did, that's completely correct.
The Add a Product task assumes that the current Actor (a user of the shopping web app) already has their browser open somewhere on the app.

> [!NOTE]
> It's perfectly reasonable, recommended in fact, for Tasks to use _implicit assumptions about the state of the application_.
> It's encouraged to document these, to aid reusability.

Let's look at some sample code, which might form this task.

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
        await actor.PerformAsync(Click(SitewideUi.ProductSearchNowButton), cancellationToken);
        var addToCartButton = ProductSearchResultsPage.AddToCartButtonForProductNamed(productName);
        await actor.PerformAsync(Click(addToCartButton), cancellationToken);
    }

    public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
        => formatter.Format("{Actor} searches for '{ProductName}' and adds one to their cart", actor, productName);
}
```
