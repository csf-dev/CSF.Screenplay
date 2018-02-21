using CSF.Screenplay.Selenium.Builders;
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
  [Description("General waits which pause the test execution")]
  public class GeneralWaitTests
  {
    [Test,Screenplay]
    [Description("When waiting for only half a second, the page event has not yet occurred")]
    public void Wait_for_500_milliseconds_means_that_the_delayed_link_has_not_appeared(ICast cast, BrowseTheWeb browseTheWeb)
    {
      var joe = cast.Get("Joe");joe.IsAbleTo(browseTheWeb);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageThree>());

      When(joe).AttemptsTo(Click.On(PageThree.DelayedButtonOne));
      When(joe).AttemptsTo(Wait.For(500).Milliseconds());

      Then(joe).ShouldSee(TheVisibility.Of(PageThree.DelayedLinkOne)).Should().Be(false);
    }

    [Test,Screenplay]
    [Description("When waiting for 6 seconds, the page event fires")]
    public void Wait_for_6_seconds_means_that_the_delayed_link_appears(ICast cast, BrowseTheWeb browseTheWeb)
    {
      var joe = cast.Get("Joe");joe.IsAbleTo(browseTheWeb);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageThree>());

      When(joe).AttemptsTo(Click.On(PageThree.DelayedButtonOne));
      When(joe).AttemptsTo(Wait.For(6).Seconds());

      Then(joe).ShouldSee(TheVisibility.Of(PageThree.DelayedLinkOne)).Should().Be(true);
    }

  }
}
