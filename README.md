# CSF.Screenplay
**[Screenplay]** is a *design pattern* for writing [BDD tests]; it has formerly been known as the **Journey** pattern.  Screenplay helps developers write high-value test logic:

* It encourages *human-readable test code*
* Screenplay follows the [SOLID design principles], aiding reusability
* Screenplay keeps tests focussed upon *actors* how they use the software under test
* Detailled human-readable reports are produced at the end of every test run
* It is an ideal framework for controlling technologies such as [Selenium] Web Driver

The CSF.Screenplay library is inspired by [Serenity], a testing framework which includes a Java implementation of the Screenplay pattern.

[Screenplay]: https://www.infoq.com/articles/Beyond-Page-Objects-Test-Automation-Serenity-Screenplay
[BDD tests]: https://en.wikipedia.org/wiki/Behavior-driven_development
[SOLID design principles]: https://en.wikipedia.org/wiki/SOLID_(object-oriented_design)
[Selenium]: http://www.seleniumhq.org/
[Serenity]: https://github.com/serenity-bdd

## Example test
*The preferred integration* for Screenplay is **[SpecFlow]**.  This example uses **[NUnit]** instead though, as it is more concise and more widely recognised.

[SpecFlow]: http://specflow.org/
[NUnit]: http://nunit.org/

```csharp
[TestFixture]
[Description("Users should be able to buy groceries via the web application")]
public class UsersCanBuyGroceries
{
  [Test,Screenplay]
  [Description("Joe should see a thankyou message when he uses the web application to buy eggs.")]
  public void JoeShouldSeeAThankyouMessageWhenHeBuysEggs(ICast cast, BrowseTheWeb browseTheWeb)
  {
    var joe = cast.Get("Joe")
    joe.IsAbleTo(browseTheWeb);

    Given(joe).WasAbleTo(SearchTheShop.ForGroceries());
    When(joe).AttemptsTo(Click.On(GroceriesForSale.BuyEggsButton));
    var message = Then(joe).ShouldSee(TheText.Of(GroceriesForSale.FeedbackMessage));

    Assert.That(message, Is.EqualTo("Thankyou for buying eggs."));
  }
}
```

## Continuous integration status
CI builds are configured via both **Travis** (for build & test on Linux/Mono) and **AppVeyor** (Windows/.NET).
Below are links to the most recent build statuses for these two CI platforms.

Platform | Status
-------- | ------
Linux/Mono (Travis) | [![Travis Status](https://travis-ci.org/csf-dev/CSF.Screenplay.svg?branch=master)](https://travis-ci.org/csf-dev/CSF.Screenplay)
Windows/.NET (AppVeyor) | [![AppVeyor status](https://ci.appveyor.com/api/projects/status/y9ejfko3kflosava?svg=true)](https://ci.appveyor.com/project/craigfowler/csf-screenplay)
