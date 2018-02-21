using System;
using CSF.Screenplay.NUnit;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Tests.Tasks;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Selenium.Tests.Actions
{
  [TestFixture]
  [Description("The 'clear cookies' action")]
  public class ClearCookiesTests
  {
    [Test,Screenplay]
    [Description("Clearing all cookies does not raise an exception.")]
    public void Clear_all_cookies_does_not_raise_an_exception(ICast cast, BrowseTheWeb browseTheWeb)
    {
      var joe = cast.Get("Joe");joe.IsAbleTo(browseTheWeb);

      joe.ShouldIgnoreThisTestIfTheirBrowserHasAnyOfTheFlags(Flags.Browser.CannotClearDomainCookies);

      Given(joe).WasAbleTo(new EnterTextIntoThePageTwoInputField("Some text"));

      Assert.DoesNotThrow(() => When(joe).AttemptsTo(ClearTheirBrowser.Cookies()));
    }
  }
}
