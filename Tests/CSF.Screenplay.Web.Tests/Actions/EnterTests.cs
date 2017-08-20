using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;
using static CSF.Screenplay.NUnit.ScenarioGetter;

namespace CSF.Screenplay.Web.Tests.Actions
{
  [TestFixture,Screenplay]
  [Description("Entering text into elements")]
  public class EnterTests
  {
    [Test]
    [Description("Typing text into an input box produces the expected result on the page.")]
    public void Type_text_into_an_input_box_produces_expected_result_on_page()
    {
      var joe = Scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Enter.TheText("The right value").Into(PageTwo.SpecialInputField));

      Then(joe).ShouldSee(TheText.Of(PageTwo.TheDynamicTextArea)).Should().Be("different value");
    }

    [Test]
    [Description("Typing different text into an input box produces the expected result on the page.")]
    public void Type_different_text_into_an_input_box_produces_expected_result_on_page()
    {
      var joe = Scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Enter.TheText("The wrong value").Into(PageTwo.SpecialInputField));

      Then(joe).ShouldSee(TheText.Of(PageTwo.TheDynamicTextArea)).Should().Be("dynamic value");
    }
  }
}
