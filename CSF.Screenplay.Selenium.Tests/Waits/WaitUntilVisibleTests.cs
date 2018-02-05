using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Models;
using CSF.Screenplay.Selenium.Tests.Pages;
using FluentAssertions;
using CSF.Screenplay.NUnit;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Selenium.Abilities;
using System;

namespace CSF.Screenplay.Selenium.Tests.Waits
{
  [TestFixture]
  [Description("Waiting for elements to be available")]
  public class WaitUntilVisibleTests
  {
    [Test,Screenplay]
    [Description("Waiting for an element to become visible eventually detects that element, with the appropriate text.")]
    public void Wait_UntilVisible_returns_element_with_correct_text(ICast cast, Func<BrowseTheWeb> webBrowserFactory)
    {
      var joe = cast.GetJoe(webBrowserFactory);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageThree>());

      When(joe).AttemptsTo(Click.On(PageThree.DelayedButtonOne));
      When(joe).AttemptsTo(Wait.ForAtMost(5).Seconds().OrUntil(PageThree.DelayedLinkOne).IsVisible());

      Then(joe).ShouldSee(TheText.Of(PageThree.DelayedLinkOne)).Should().Be("This link appears!");
    }

    [Test,Screenplay]
    [Description("If the actor does not wait long enough for the element to appear then an exception is raised.")]
    public void Wait_UntilVisible_raises_exception_if_we_dont_wait_long_enough(ICast cast, Func<BrowseTheWeb> webBrowserFactory)
    {
      var joe = cast.GetJoe(webBrowserFactory);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageThree>());

      When(joe).AttemptsTo(Click.On(PageThree.DelayedButtonOne));

      Assert.Throws<GivenUpWaitingException>(() => {
        When(joe).AttemptsTo(Wait.ForAtMost(2).Seconds().OrUntil(PageThree.DelayedLinkOne).IsVisible());
      });
    }
  }
}
