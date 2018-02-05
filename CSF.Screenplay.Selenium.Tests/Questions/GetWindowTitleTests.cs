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
  [Description("Reading the title of the browser window")]
  public class GetWindowTitleTests
  {
    [Test,Screenplay]
    [Description("Reading the title of the browser window, whilst on the App home page, gets the expected title.")]
    public void GetWindowTitle_returns_correct_result(ICast cast, Func<BrowseTheWeb> webBrowserFactory)
    {
      var joe = cast.GetJoe(webBrowserFactory);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      Then(joe).ShouldSee(TheWindow.Title()).Should().Be("App home page");
    }
  }
}
