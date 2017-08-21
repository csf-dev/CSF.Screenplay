using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using FluentAssertions;
using CSF.Screenplay.NUnit;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Web.Tests.Questions
{
  [TestFixture]
  [Description("Reading the value of a form element")]
  public class GetValueTests
  {
    [Test,Screenplay]
    [Description("Reading the value of an element detects the expected value.")]
    public void GetValue_returns_expected_value(ScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      Then(joe).ShouldSee(TheValue.Of(PageTwo.SecondTextbox)).Should().Be("This is a text box");
    }

    [Test,Screenplay]
    [Description("Reading the value of an element and converting it to a number detects the expected value.")]
    public void GetConvertedValue_returns_expected_value(ScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());
      Given(joe).WasAbleTo(Enter.TheText("55").Into(PageTwo.SpecialInputField));

      Then(joe).ShouldSee(TheValue.From(PageTwo.SpecialInputField).As<int>()).Should().Be(55);
    }
  }
}
