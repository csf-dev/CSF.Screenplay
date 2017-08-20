using System;
using CSF.Screenplay.NUnit;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using FluentAssertions;

namespace CSF.Screenplay.Web.Tests.Waits
{
  [ScreenplayTest]
  [Description("General waits which pause the test execution")]
  public class GeneralWaitTests
  {
    [Test]
    [Description("When waiting for only half a second, the page event has not yet occurred")]
    public void Wait_for_500_milliseconds_means_that_the_delayed_link_has_not_appeared(ScreenplayScenario context)
    {
      var joe = context.GetCast().Get("joe");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageThree>());

      When(joe).AttemptsTo(Click.On(PageThree.DelayedButtonOne));
      When(joe).AttemptsTo(Wait.For(500).Milliseconds());

      Then(joe).ShouldSee(TheVisibility.Of(PageThree.DelayedLinkOne)).Should().Be(false);
    }

    [Test]
    [Description("When waiting for 6 seconds, the page event fires")]
    public void Wait_for_6_seconds_means_that_the_delayed_link_appears(ScreenplayScenario context)
    {
      var joe = context.GetCast().Get("joe");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageThree>());

      When(joe).AttemptsTo(Click.On(PageThree.DelayedButtonOne));
      When(joe).AttemptsTo(Wait.For(6).Seconds());

      Then(joe).ShouldSee(TheVisibility.Of(PageThree.DelayedLinkOne)).Should().Be(true);
    }

  }
}
