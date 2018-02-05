using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Tests.Pages;
using FluentAssertions;
using CSF.Screenplay.NUnit;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.Selenium.Models;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Selenium.Tests.Tasks
{
  [TestFixture]
  [Description("Navigating to a new page")]
  public class NavigateToNewPageByClickingTests
  {
    [Test,Screenplay]
    [Description("Using the navigate task to a slow-loading page produces the expected output on the page")]
    public void Navigate_to_a_slow_loading_page_finds_the_correct_page(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      When(joe).AttemptsTo(Navigate.ToAnotherPageByClicking(HomePage.SlowLoadingLink));

      Then(joe).ShouldSee(TheText.Of(HomePage.LoadDelay))
               .Should()
               .Be("2 seconds");
    }
  }
}
