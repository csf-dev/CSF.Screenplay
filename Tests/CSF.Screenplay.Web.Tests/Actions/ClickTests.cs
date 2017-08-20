using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.NUnit;

namespace CSF.Screenplay.Web.Tests.Actions
{
  [ScreenplayTest]
  [Description("The click action")]
  public class ClickTests
  {
    [Test]
    [Description("Clicking on the link to page two navigates to the second page.")]
    public void Click_OnLinkToPageTwo_navigates_to_second_page(ScreenplayScenario context)
    {
      var joe = context.GetCast().Get("joe");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      When(joe).AttemptsTo(Click.On(HomePage.SecondPageLink));

      Then(joe).Should(Wait.ForAtMost(2).Seconds().OrUntil(PageTwo.SpecialInputField).IsVisible());

      Then(joe).ShouldSee(TheWindow.Title()).Should().Be("Page two");
    }
  }
}
