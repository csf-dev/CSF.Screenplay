using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;
using static CSF.Screenplay.NUnit.ScenarioGetter;

namespace CSF.Screenplay.Web.Tests.Questions
{
  [TestFixture,Screenplay]
  [Description("Reading the title of the browser window")]
  public class GetWindowTitleTests
  {
    [Test]
    [Description("Reading the title of the browser window, whilst on the App home page, gets the expected title.")]
    public void GetWindowTitle_returns_correct_result()
    {
      var joe = Scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      Then(joe).ShouldSee(TheWindow.Title()).Should().Be("App home page");
    }
  }
}
