using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using FluentAssertions;
using CSF.Screenplay.NUnit;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Web.Tests.Questions
{
  [TestFixture]
  [Description("Finding HTML elements")]
  public class FindElementsTests
  {
    [Test,Screenplay]
    [Description("Finding child elements of the item list detects the correct count of children.")]
    public void FindElements_In_gets_expected_count_of_elements(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      Then(joe)
        .ShouldSee(Elements.In(PageTwo.ListOfItems).Get())
        .Elements.Count.Should().Be(5);
    }

    [Test,Screenplay]
    [Description("Finding elements on the page detects the correct count of children.")]
    public void FindElements_OnThePage_gets_expected_count_of_elements(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      Then(joe)
        .ShouldSee(Elements.OnThePage().ThatAre(PageTwo.ItemsInTheList).Called("the listed items"))
        .Elements.Count.Should().Be(5);
    }
  }
}
