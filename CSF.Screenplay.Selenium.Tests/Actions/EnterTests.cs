using CSF.Screenplay.NUnit;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Tests.Pages;
using FluentAssertions;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Selenium.Tests.Actions
{
  [TestFixture]
  [Description("Entering text into elements")]
  public class EnterTests
  {
    [Test,Screenplay]
    [Description("Typing text into an input box produces the expected result on the page.")]
    public void Type_text_into_an_input_box_produces_expected_result_on_page(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Enter.TheText("The right value").Into(PageTwo.SpecialInputField));

      Then(joe).ShouldSee(TheText.Of(PageTwo.TheDynamicTextArea)).Should().Be("different value");
    }

    [Test,Screenplay]
    [Description("Typing different text into an input box produces the expected result on the page.")]
    public void Type_different_text_into_an_input_box_produces_expected_result_on_page(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Enter.TheText("The wrong value").Into(PageTwo.SpecialInputField));

      Then(joe).ShouldSee(TheText.Of(PageTwo.TheDynamicTextArea)).Should().Be("dynamic value");
    }
  }
}
