using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;
using static CSF.Screenplay.NUnit.ScenarioGetter;

namespace CSF.Screenplay.Web.Tests.Questions
{
  [TestFixture,Screenplay]
  [Description("Reading the text of an element")]
  public class GetTextTests
  {
    [Test]
    [Description("Reading the text of an element detects the expected value.")]
    public void GetText_returns_expected_value()
    {
      var joe = Scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      Then(joe).ShouldSee(TheText.Of(HomePage.ImportantString)).Should().Be("banana!");
    }

    [Test]
    [Description("Reading the text of an element and converting it to a number detects the expected value.")]
    public void GetConvertedText_returns_expected_value()
    {
      var joe = Scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      Then(joe).ShouldSee(TheText.From(HomePage.ImportantNumber).As<int>()).Should().Be(42);
    }

    [Test]
    [Description("Reading the text of multiple elements returns the correct collection of values.")]
    public void GetText_for_multiple_elements_returns_expected_values()
    {
      var joe = Scenario.GetJoe();

      var expected = new [] { "One", "Two", "Three", "Four", "Five" };

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      Then(joe)
        .ShouldSee(TheText.OfAll(PageTwo.ItemsInTheList))
        .Should().BeEquivalentTo(expected);
    }
  }
}
