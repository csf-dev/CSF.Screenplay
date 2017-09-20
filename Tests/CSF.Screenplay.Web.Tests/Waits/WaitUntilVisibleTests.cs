using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Tests.Pages;
using FluentAssertions;
using CSF.Screenplay.NUnit;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Web.Tests.Waits
{
  [TestFixture]
  [Description("Waiting for elements to be available")]
  public class WaitUntilVisibleTests
  {
    [Test,Screenplay]
    [Description("Waiting for an element to become visible eventually detects that element, with the appropriate text.")]
    public void Wait_UntilVisible_returns_element_with_correct_text(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageThree>());

      When(joe).AttemptsTo(Click.On(PageThree.DelayedButtonOne));
      When(joe).AttemptsTo(Wait.ForAtMost(5).Seconds().OrUntil(PageThree.DelayedLinkOne).IsVisible());

      Then(joe).ShouldSee(TheText.Of(PageThree.DelayedLinkOne)).Should().Be("This link appears!");
    }

    [Test,Screenplay]
    [Description("If the actor does not wait long enough for the element to appear then an exception is raised.")]
    public void Wait_UntilVisible_raises_exception_if_we_dont_wait_long_enough(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageThree>());

      When(joe).AttemptsTo(Click.On(PageThree.DelayedButtonOne));

      Assert.Throws<GivenUpWaitingException>(() => {
        When(joe).AttemptsTo(Wait.ForAtMost(2).Seconds().OrUntil(PageThree.DelayedLinkOne).IsVisible());
      });
    }

    [Test,Screenplay]
    [Description("If the actor waits for a page-load, then the operation blocks until the page is ready.")]
    public void Wait_UntilThePageLoads_does_not_cause_race_condition_on_slow_loading_page(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      When(joe).AttemptsTo(Click.On(HomePage.SlowLoadingLink));

      Then(joe).Should(Wait.UntilThePageLoads());
      Then(joe).ShouldSee(TheText.Of(HomePage.LoadDelay))
               .Should()
               .Be("2 seconds");
    }
  }
}
