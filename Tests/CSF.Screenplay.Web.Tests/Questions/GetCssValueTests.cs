using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using FluentAssertions;
using CSF.Screenplay.NUnit;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Web.Tests.Questions
{
  [TestFixture]
  [Description("Reading the value of CSS properties")]
  public class GetCssValueTests
  {
    [Test,Screenplay]
    [Description("Reading the value of the 'color' property detects the expected value.")]
    public void GetCssValue_for_red_string_gets_correct_colour(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      Then(joe).ShouldSee(TheCss.Property("color").From(HomePage.ImportantString))
               .Should().MatchRegex(@"^rgba?\(255, *0, *0(, *1)?\)$");
    }
  }
}
