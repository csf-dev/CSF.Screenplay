using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.NUnit;

namespace CSF.Screenplay.Web.Tests.Waits
{
  [TestFixture]
  [Description("Waiting for elements to be available")]
  public class WaitUntilVisibleTests
  {
    [Test]
    [Description("Waiting for an element to become visible eventually detects that element, with the appropriate text.")]
    public void Wait_UntilVisible_returns_element_with_correct_text()
    {
      var joe = Stage.Cast.GetOrCreate("joe");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageThree>());

      When(joe).AttemptsTo(Click.On(PageThree.DelayedButtonOne));
      When(joe).AttemptsTo(Wait.ForAtMost(TimeSpan.FromSeconds(10)).Until(PageThree.DelayedLinkOne).IsVisible());

      Then(joe).ShouldSee(TheText.Of(PageThree.DelayedLinkOne)).Should().Be("This link appears!");
    }

    [Test]
    [Description("If the actor does not wait long enough for the element to appear then an exception is raised.")]
    public void Wait_UntilVisible_raises_exception_if_we_dont_wait_long_enough()
    {
      var joe = Stage.Cast.GetOrCreate("joe");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageThree>());

      When(joe).AttemptsTo(Click.On(PageThree.DelayedButtonOne));

      Assert.Throws<GivenUpWaitingException>(() => {
        When(joe).AttemptsTo(Wait.ForAtMost(TimeSpan.FromSeconds(4)).Until(PageThree.DelayedLinkOne).IsVisible());
      });
    }
  }
}
