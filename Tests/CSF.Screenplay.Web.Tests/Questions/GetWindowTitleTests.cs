using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using FluentAssertions;
using CSF.Screenplay.NUnit;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Web.Tests.Questions
{
  [TestFixture]
  [Description("Reading the title of the browser window")]
  public class GetWindowTitleTests
  {
    [Test,Screenplay]
    [Description("Reading the title of the browser window, whilst on the App home page, gets the expected title.")]
    public void GetWindowTitle_returns_correct_result(ScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      Then(joe).ShouldSee(TheWindow.Title()).Should().Be("App home page");
    }
  }
}
