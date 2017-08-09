using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.NUnit;

namespace CSF.Screenplay.Web.Tests.Actions
{
  [ScreenplayFixture]
  [Description("The click action")]
  public class ClickTests
  {
    readonly ScreenplayContext context;

    public ClickTests(ScreenplayContext context)
    {
      if(context == null)
        throw new ArgumentNullException(nameof(context));
      this.context = context;
    }

    [Test]
    [Description("Clicking on the link to page two navigates to the second page.")]
    public void Click_OnLinkToPageTwo_navigates_to_second_page()
    {
      var joe = context.GetCast().GetOrCreate("joe");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      When(joe).AttemptsTo(Click.On(HomePage.SecondPageLink));

      Then(joe).Should(Wait.ForAtMost(TimeSpan.FromSeconds(2)).Until(PageTwo.SpecialInputField).IsVisible());

      Then(joe).ShouldSee(TheWindow.Title()).Should().Be("Page two");
    }
  }
}
