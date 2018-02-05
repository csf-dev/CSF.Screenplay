using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Tests.Pages;
using CSF.Screenplay.NUnit;
using FluentAssertions;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.Selenium.Tests.Tasks;
using CSF.Screenplay.Selenium.Abilities;

namespace CSF.Screenplay.Selenium.Tests.Actions
{
  [TestFixture]
  [Description("The 'clear cookies' action")]
  public class ClearCookiesTests
  {
    [Test,Screenplay]
    [Description("Clearing all cookies does not raise an exception.")]
    public void Clear_all_cookies_does_not_raise_an_exception(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      var ability = joe.GetAbility<BrowseTheWeb>();
      var isCapable = ability.GetCapability(Capabilities.ClearDomainCookies);
      if(!isCapable)
        Assert.Ignore("Joe is using a web browser which is not capable of clearing domain cookies");

      Given(joe).WasAbleTo(new EnterTextIntoThePageTwoInputField("Some text"));

      Assert.DoesNotThrow(() => When(joe).AttemptsTo(ClearTheirBrowser.Cookies()));
    }
  }
}
