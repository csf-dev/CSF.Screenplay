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
  [Description("Finding HTML elements")]
  public class FindElementsTests
  {
    [Test,Screenplay]
    [Description("Finding child elements of the item list detects the correct count of children.")]
    public void FindElements_In_gets_expected_count_of_elements(ICast cast, Lazy<BrowseTheWeb> webBrowserFactory)
    {
      var joe = cast.GetJoe(webBrowserFactory);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      Then(joe)
        .ShouldSee(Elements.In(PageTwo.ListOfItems).Get())
        .Elements.Count.Should().Be(5);
    }

    [Test,Screenplay]
    [Description("Finding elements on the page detects the correct count of children.")]
    public void FindElements_OnThePage_gets_expected_count_of_elements(ICast cast, Lazy<BrowseTheWeb> webBrowserFactory)
    {
      var joe = cast.GetJoe(webBrowserFactory);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      Then(joe)
        .ShouldSee(Elements.InThePageBody().ThatAre(PageTwo.ItemsInTheList).Called("the listed items"))
        .Elements.Count.Should().Be(5);
    }
  }
}
