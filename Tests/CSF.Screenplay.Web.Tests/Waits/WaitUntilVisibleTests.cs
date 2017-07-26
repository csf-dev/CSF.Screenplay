using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Tests.Waits
{
  [TestFixture]
  public class WaitUntilVisibleTests
  {
    Actor joe;

    [SetUp]
    public void Setup()
    {
      joe = WebdriverTestSetup.GetJoe();
    }

    [Test, Reportable]
    public void Wait_UntilVisible_returns_element_with_correct_text()
    {
      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageThree>());

      When(joe).AttemptsTo(Click.On(PageThree.DelayedButtonOne));
      When(joe).AttemptsTo(Wait.ForAtMost(TimeSpan.FromSeconds(10)).Until(PageThree.DelayedLinkOne).IsVisible());

      Then(joe).ShouldSee(TheText.Of(PageThree.DelayedLinkOne)).Should().Be("This link appears!");
    }

    [Test, Reportable]
    public void Wait_UntilVisible_raises_exception_if_we_dont_wait_long_enough()
    {
      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageThree>());

      When(joe).AttemptsTo(Click.On(PageThree.DelayedButtonOne));

      Assert.Throws<GivenUpWaitingException>(() => {
        When(joe).AttemptsTo(Wait.ForAtMost(TimeSpan.FromSeconds(4)).Until(PageThree.DelayedLinkOne).IsVisible());
      });
    }
  }
}
