using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using CSF.Screenplay.NUnit;
using FluentAssertions;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Web.Tests.Actions
{
  [TestFixture]
  [Description("The click action")]
  public class ClickTests
  {
    [Test,Screenplay]
    [Description("Clicking on the link to page two navigates to the second page.")]
    public void Click_OnLinkToPageTwo_navigates_to_second_page(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      When(joe).AttemptsTo(Click.On(HomePage.SecondPageLink));

      Then(joe).Should(Wait.ForAtMost(2).Seconds().OrUntil(PageTwo.SpecialInputField).IsVisible());

      Then(joe).ShouldSee(TheWindow.Title()).Should().Be("Page two");
    }
  }
}
