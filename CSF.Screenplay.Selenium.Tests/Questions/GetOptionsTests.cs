using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Tests.Pages;
using FluentAssertions;
using CSF.Screenplay.NUnit;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Selenium.Tests.Questions
{
  [TestFixture]
  [Description("Reading options from HTML <select> elements")]
  public class GetOptionsTests
  {
    [Test,Screenplay]
    [Description("Reading the available options reveals the expected collection of items.")]
    public void GetAllOptions_returns_expected_collection(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      var expected = new Models.Option[] {
        new Models.Option("One", "1"),
        new Models.Option("Two", "2"),
        new Models.Option("Three", "3"),
      };

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      Then(joe).ShouldSee(TheOptions.In(PageTwo.SingleSelectionList)).ShouldBeEquivalentTo(expected);
    }

    [Test,Screenplay]
    [Description("Reading the selected options reveals the expected collection of items.")]
    public void GetSelectedOptions_returns_expected_collection(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      var expected = new Models.Option[] {
        new Models.Option("Carrot", "veg"),
        new Models.Option("Steak", "meat"),
      };

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      Then(joe).ShouldSee(TheOptions.SelectedIn(PageTwo.MultiSelectionList)).ShouldBeEquivalentTo(expected);
    }
  }
}
