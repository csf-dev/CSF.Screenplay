using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Tests.Pages;
using FluentAssertions;
using CSF.Screenplay.NUnit;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Selenium.Abilities;
using System;

namespace CSF.Screenplay.Selenium.Tests.Questions
{
  [TestFixture]
  [Description("Reading the value of CSS properties")]
  public class GetCssValueTests
  {
    [Test,Screenplay]
    [Description("Reading the value of the 'color' property detects the expected value.")]
    public void GetCssValue_for_red_string_gets_correct_colour(ICast cast, Lazy<BrowseTheWeb> webBrowserFactory)
    {
      var joe = cast.GetJoe(webBrowserFactory);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      Then(joe).ShouldSee(TheCss.Property("color").From(HomePage.ImportantString))
               .Should().MatchRegex(@"^rgba?\(255, *0, *0(, *1)?\)$");
    }
  }
}
