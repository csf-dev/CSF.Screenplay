using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Tests.Pages;
using FluentAssertions;
using CSF.Screenplay.NUnit;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Selenium.Tests.Questions
{
  [TestFixture]
  [Description("Reading element attributes")]
  public class GetAttributeTests
  {
    [Test,Screenplay]
    [Description("Reading the value of a 'title' attribute detects the expected value.")]
    public void GetAttribute_returns_expected_value(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      Then(joe).ShouldSee(TheAttribute.Named("title").From(PageTwo.TheDynamicTextArea)).Should().Be("This is a dynamic value");
    }
  }
}
