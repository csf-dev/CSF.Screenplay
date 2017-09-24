using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using FluentAssertions;
using CSF.Screenplay.NUnit;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Web.Tests.Tasks
{
  [TestFixture]
  [Description("Navigating to a new page")]
  public class NavigateToNewPageByClickingTests
  {
    [Test,Screenplay]
    [Description("Using the navigate task to a slow-loading page produces the expected output on the page")]
    public void Navigate_to_a_slow_loading_page_finds_the_correct_page(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      When(joe).AttemptsTo(Navigate.ToAnotherPageByClicking(HomePage.SlowLoadingLink));

      Then(joe).ShouldSee(TheText.Of(HomePage.LoadDelay))
               .Should()
               .Be("2 seconds");
    }

    [Test,Screenplay]
    [Description("If the user doesn't wait long enough then navigating to a slow-loading page will fail")]
    public void Navigate_to_a_slow_loading_page_raises_exception_if_the_user_doesnt_wait_long_enough(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      Assert.Throws<GivenUpWaitingException>(NavigateWithoutWaiting(joe));
    }

    TestDelegate NavigateWithoutWaiting(IActor joe)
    {
      return () => {
        When(joe).AttemptsTo(Navigate.WaitingUpTo(500).Milliseconds()
                                     .ToADifferentPageByClicking(HomePage.SlowLoadingLink));
      };
    }
  }
}
