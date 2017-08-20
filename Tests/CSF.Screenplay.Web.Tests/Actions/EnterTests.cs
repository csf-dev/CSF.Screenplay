using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.NUnit;

namespace CSF.Screenplay.Web.Tests.Actions
{
  [ScreenplayTest]
  [Description("Entering text into elements")]
  public class EnterTests
  {
    [Test]
    [Description("Typing text into an input box produces the expected result on the page.")]
    public void Type_text_into_an_input_box_produces_expected_result_on_page(ScreenplayScenario context)
    {
      var joe = context.GetCast().Get("joe");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Enter.TheText("The right value").Into(PageTwo.SpecialInputField));

      Then(joe).ShouldSee(TheText.Of(PageTwo.TheDynamicTextArea)).Should().Be("different value");
    }

    [Test]
    [Description("Typing different text into an input box produces the expected result on the page.")]
    public void Type_different_text_into_an_input_box_produces_expected_result_on_page(ScreenplayScenario context)
    {
      var joe = context.GetCast().Get("joe");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Enter.TheText("The wrong value").Into(PageTwo.SpecialInputField));

      Then(joe).ShouldSee(TheText.Of(PageTwo.TheDynamicTextArea)).Should().Be("dynamic value");
    }
  }
}
