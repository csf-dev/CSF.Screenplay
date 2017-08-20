using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.NUnit;

namespace CSF.Screenplay.Web.Tests.Questions
{
  [ScreenplayTest]
  [Description("Finding HTML elements")]
  public class FindElementsTests
  {
    [Test]
    [Description("Finding child elements of the item list detects the correct count of children.")]
    public void FindElements_In_gets_expected_count_of_elements(ScreenplayScenario context)
    {
      var joe = context.GetCast().Get("joe");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      Then(joe)
        .ShouldSee(Elements.In(PageTwo.ListOfItems).Get())
        .Elements.Count.Should().Be(5);
    }

    [Test]
    [Description("Finding elements on the page detects the correct count of children.")]
    public void FindElements_OnThePage_gets_expected_count_of_elements(ScreenplayScenario context)
    {
      var joe = context.GetCast().Get("joe");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      Then(joe)
        .ShouldSee(Elements.OnThePage().ThatAre(PageTwo.ItemsInTheList).Called("the listed items"))
        .Elements.Count.Should().Be(5);
    }
  }
}
