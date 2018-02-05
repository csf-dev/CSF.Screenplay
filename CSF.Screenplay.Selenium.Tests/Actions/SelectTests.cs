using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Tests.Pages;
using CSF.Screenplay.NUnit;
using FluentAssertions;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Selenium.Abilities;
using System;

namespace CSF.Screenplay.Selenium.Tests.Actions
{
  [TestFixture]
  [Description("The select action")]
  public class SelectTests
  {
    [Test,Screenplay]
    [Description("Selecting by text generates the expected result on the page.")]
    public void SelectByText_generates_expected_result_on_page(ICast cast, Func<BrowseTheWeb> webBrowserFactory)
    {
      var joe = cast.GetJoe(webBrowserFactory);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Select.Item("Two").From(PageTwo.SingleSelectionList));

      Then(joe).ShouldSee(TheText.From(PageTwo.SingleSelectionValue).As<int>()).Should().Be(2);
    }

    [Test,Screenplay]
    [Description("Selecting by index generates the expected result on the page.")]
    public void SelectByIndex_generates_expected_result_on_page(ICast cast, Func<BrowseTheWeb> webBrowserFactory)
    {
      var joe = cast.GetJoe(webBrowserFactory);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Select.ItemNumber(3).From(PageTwo.SingleSelectionList));

      Then(joe).ShouldSee(TheText.From(PageTwo.SingleSelectionValue).As<int>()).Should().Be(3);
    }

    [Test,Screenplay]
    [Description("Selecting by value generates the expected result on the page.")]
    public void SelectByValue_generates_expected_result_on_page(ICast cast, Func<BrowseTheWeb> webBrowserFactory)
    {
      var joe = cast.GetJoe(webBrowserFactory);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Select.ItemValued("1").From(PageTwo.SingleSelectionList));

      Then(joe).ShouldSee(TheText.From(PageTwo.SingleSelectionValue).As<int>()).Should().Be(1);
    }
  }
}
