using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using CSF.Screenplay.NUnit;
using FluentAssertions;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.Web.Tests.Tasks;

namespace CSF.Screenplay.Web.Tests.Actions
{
  [TestFixture]
  [Description("The clear action")]
  public class ClearTests
  {
    [Test,Screenplay]
    [Description("Clearing an element after entering some text results in an empty string.")]
    public void Clear_an_element_after_entering_some_text_results_in_an_empty_string(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(new EnterTextIntoThePageTwoInputField("Some text"));

      When(joe).AttemptsTo(Clear.TheContentsOf(PageTwo.SpecialInputField));

      Then(joe).ShouldSee(TheValue.Of(PageTwo.SpecialInputField))
               .Should()
               .BeEmpty();
    }
  }
}
