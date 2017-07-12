using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Web.Tests.Actions
{
  [TestFixture]
  public class ClickTests
  {
    Actor joe;

    [SetUp]
    public void Setup()
    {
      joe = WebdriverTestSetup.GetJoe();
    }

    [Test]
    public void Click_OnLinkToPageTwo_navigates_to_second_page()
    {
      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      When(joe).AttemptsTo(Click.On(HomePage.SecondPageLink));

      WebdriverTestSetup.TakeScreenshot(GetType(), nameof(Click_OnLinkToPageTwo_navigates_to_second_page));

      Then(joe).ShouldSee(TheWindow.Title()).Should().Be("Page two");
    }
  }
}
