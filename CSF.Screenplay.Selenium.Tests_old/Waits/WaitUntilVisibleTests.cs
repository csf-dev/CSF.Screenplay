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
    public void Wait_UntilVisible_returns_element_with_correct_text(ICast cast, BrowseTheWeb browseTheWeb)
    {
      var joe = cast.Get("Joe");joe.IsAbleTo(browseTheWeb);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageThree>());

      When(joe).AttemptsTo(Click.On(PageThree.DelayedButtonOne));
      When(joe).AttemptsTo(Wait.ForAtMost(5).Seconds().OrUntil(PageThree.DelayedLinkOne).IsVisible());

      Then(joe).ShouldSee(TheText.Of(PageThree.DelayedLinkOne)).Should().Be("This link appears!");
    }

    [Test,Screenplay]
    [Description("If the actor does not wait long enough for the element to appear then an exception is raised.")]
    public void Wait_UntilVisible_raises_exception_if_we_dont_wait_long_enough(ICast cast, BrowseTheWeb browseTheWeb)
    {
      var joe = cast.Get("Joe");joe.IsAbleTo(browseTheWeb);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageThree>());

      When(joe).AttemptsTo(Click.On(PageThree.DelayedButtonOne));

      Assert.Throws<GivenUpWaitingException>(() => {
        When(joe).AttemptsTo(Wait.ForAtMost(2).Seconds().OrUntil(PageThree.DelayedLinkOne).IsVisible());
      });
    }

    [Test,Screenplay]
    [Description("If the actor's default wait time is not long enough for the element to appear then an exception is raised.")]
    public void Wait_UntilVisible_raises_exception_if_actors_default_wait_time_is_not_long_enough(ICast cast, BrowseTheWeb browseTheWeb)
    {
      var joe = cast.Get("Joe");
      joe.IsAbleTo(browseTheWeb);
      joe.IsAbleTo(ChooseADefaultWaitTime.Of(TimeSpan.FromSeconds(2)));

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageThree>());

      When(joe).AttemptsTo(Click.On(PageThree.DelayedButtonOne));

      Assert.Throws<GivenUpWaitingException>(() => {
        When(joe).AttemptsTo(Wait.Until(PageThree.DelayedLinkOne).IsVisible());
      });
    }

    [Test,Screenplay]
    [Description("If the actor's default wait time is sufficient for the element to appear then no exception is raised.")]
    public void Wait_UntilVisible_does_not_raise_exception_if_actors_default_wait_time_is_long_enough(ICast cast, BrowseTheWeb browseTheWeb)
    {
      var joe = cast.Get("Joe");
      joe.IsAbleTo(browseTheWeb);
      joe.IsAbleTo(ChooseADefaultWaitTime.Of(TimeSpan.FromSeconds(5)));

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageThree>());

      When(joe).AttemptsTo(Click.On(PageThree.DelayedButtonOne));
      When(joe).AttemptsTo(Wait.Until(PageThree.DelayedLinkOne).IsVisible());

      Then(joe).ShouldSee(TheText.Of(PageThree.DelayedLinkOne)).Should().Be("This link appears!");
    }
  }
}
