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
  [Description("Reading the text of an element")]
  public class GetTextTests
  {
    [Test,Screenplay]
    [Description("Reading the text of an element detects the expected value.")]
    public void GetText_returns_expected_value(ICast cast, BrowseTheWeb browseTheWeb)
    {
      var joe = cast.GetJoe(webBrowserFactory);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      Then(joe).ShouldSee(TheText.Of(HomePage.ImportantString)).Should().Be("banana!");
    }

    [Test,Screenplay]
    [Description("Reading the text of an element and converting it to a number detects the expected value.")]
    public void GetConvertedText_returns_expected_value(ICast cast, BrowseTheWeb browseTheWeb)
    {
      var joe = cast.GetJoe(webBrowserFactory);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      Then(joe).ShouldSee(TheText.From(HomePage.ImportantNumber).As<int>()).Should().Be(42);
    }

    [Test,Screenplay]
    [Description("Reading the text of multiple elements returns the correct collection of values.")]
    public void GetText_for_multiple_elements_returns_expected_values(ICast cast, BrowseTheWeb browseTheWeb)
    {
      var joe = cast.GetJoe(webBrowserFactory);

      var expected = new [] { "One", "Two", "Three", "Four", "Five" };

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      Then(joe)
        .ShouldSee(TheText.OfAll(PageTwo.ItemsInTheList))
        .Should().BeEquivalentTo(expected);
    }
  }
}
