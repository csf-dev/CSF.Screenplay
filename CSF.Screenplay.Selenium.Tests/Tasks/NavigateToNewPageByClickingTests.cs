using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.NUnit;
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Tests.Pages;
using FluentAssertions;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Selenium.Tests.Tasks
{
  [TestFixture]
  [Description("Navigating to a new page")]
  public class NavigateToNewPageByClickingTests
  {
    [Test,Screenplay]
    [Description("Using the navigate task to a slow-loading page produces the expected output on the page")]
    public void Navigate_to_a_slow_loading_page_finds_the_correct_page(ICast cast, Lazy<BrowseTheWeb> webBrowserFactory)
    {
      var joe = cast.GetJoe(webBrowserFactory);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      When(joe).AttemptsTo(Navigate.ToAnotherPageByClicking(HomePage.SlowLoadingLink));

      Then(joe).ShouldSee(TheText.Of(HomePage.LoadDelay))
               .Should()
               .Be("2 seconds");
    }
  }
}
