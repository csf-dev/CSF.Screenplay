---
uid: SeleniumTargetsAndElementsArticle
---

# Targets and elements

Many of the Selenium [Actions], [Questions] and [Tasks] in this extension interact with or inspect HTML elements.
Elements on a web page are named **Targets** in Screenplay (you might say _"The target for this action"_).
All targets derive from the interface [`ITarget`].

[Actions]: Actions.md
[Questions]: Questions.md
[Tasks]: Tasks.md
[`ITarget`]: xref:CSF.Screenplay.Selenium.Elements.ITarget

## Elements and locators

Broadly, there are two kinds of target; **Elements** represent concrete references to elements on a web page.
The only way to get an element is to use the WebDriver to retrieve it, such as with the questions [`FindElement`] or [`FindElements`].
The [`SeleniumElement`] and [`SeleniumElementCollection`] classes are Elements and these contain instances of the underlying Selenium `IWebElement` type.

**Locators**, on the other hand, describe or identify elements without being concrete references.
The [`ElementId`], [`ClassName`], [`CssSelector`] and [`XPath`] classes are all Locators.

All these types derive from `ITarget`, so that any of these may be used as the parameter for the Selenium extension's performables.

[`FindElement`]: xref:CSF.Screenplay.Selenium.Questions.FindElement
[`FindElements`]: xref:CSF.Screenplay.Selenium.Questions.FindElements
[`SeleniumElement`]: xref:CSF.Screenplay.Selenium.Elements.SeleniumElement
[`SeleniumElementCollection`]: xref:CSF.Screenplay.Selenium.Elements.SeleniumElementCollection
[`ElementId`]: xref:CSF.Screenplay.Selenium.Elements.ElementId
[`ClassName`]: xref:CSF.Screenplay.Selenium.Elements.ClassName
[`CssSelector`]: xref:CSF.Screenplay.Selenium.Elements.CssSelector
[`XPath`]: xref:CSF.Screenplay.Selenium.Elements.XPath

## Naming targets

Every type which derives from `ITarget` offers a mechanism by which the target may be named.
This is a human-readable name for the target by which it would be recognised.
For Locators, this is a constructor parameter, for Elements there is an opportunity to name the element as it is being retrieved.

The target name is displayed in the [Screenplay Report] when it is produced, making easy reading.
For example, a single line of a report might read _"John clicks on the Buy Now button"_.
That would be a report for an actor named `John` using the [`Click`] action upon a target which is named `the Buy Now button`.

> [!TIP]
> Always provide human-readable names for your targets, it makes your reports far more informative.

[Screenplay Report]: ../../GettingReports.md
[`Click`]: xref:CSF.Screenplay.Selenium.Actions.Click

## Defining Locators

As you will see from their documentation, the Locator classes are easily created, reusable and immutable.
This makes them ideal to store in a shared _library_ of locators, organised according to the web application.
The recommended way to organise locators (and [named URIs]) is all that remains of [the Page Object pattern], for which the Screenplay pattern is an iteration.

A library of URIs and locators might be a series of `static` classs and static readonly fields.
Consider the following.

```csharp
public static sealed class ShoppingCart
{
    public static readonly NamedUri MyCart = new NamedUri("https://example.com/user/shopping_cart", "my shopping cart");

    public static readonly Locator
        Items = new CssSelector("#shoppingCart ul.items_list li", "the items in the cart"),
        BuyNow = new ElementId("buy_now", "the Buy Now button");
}
```

This provides a URI by which to reach the shopping cart page and two targets for relevant parts of the page.
As with the Page Object pattern, the class does not need represent an entire web page.
Common components, shared between many pages, may have their own classes.

The important difference between libraries of locators and/or named URIs and the original Page Object pattern is that libraries contain _no functionality_.
It is the Screenplay [Performables] which contain the logic, independently of the targets and URIs.
These performables may be parameterised with the targets and/or URIs, or they may consume targets/URIs directly from within the performable logic.
The choice is up to the developer writing them, allowing flexibility between both approaches.

> [!TIP]
> Store your locators and named URIs in libraries (for example static classes, as shown above).
> Organise these in however best makes sense for your application, perhaps by page or by component.
> Do not include any functionality/performable logic in these libraries.

[named URIs]: xref:CSF.Screenplay.Selenium.NamedUri
[the Page Object pattern]: https://martinfowler.com/bliki/PageObject.html
[Performables]: ../../../glossary/Performable.md
