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
  [Description("The deselect action")]
  public class DeselectTests
  {
    [Test,Screenplay]
    [Description("Deselecting everything leaves nothing selected.")]
    public void DeselectAll_leaves_nothing_selected(ICast cast, Lazy<BrowseTheWeb> webBrowserFactory)
    {
      var joe = cast.GetJoe(webBrowserFactory);

      joe.ShouldIgnoreThisTestIfTheirBrowserHasAnyOfTheFlags(Flags.HtmlElements.Select.CannotChangeState);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Deselect.EverythingFrom(PageTwo.MultiSelectionList));

      Then(joe).ShouldSee(TheText.Of(PageTwo.MultiSelectionValue)).Should().Be("Nothing!");
    }

    [Test,Screenplay]
    [Description("Deselecting by index leaves one item selected.")]
    public void DeselectByIndex_leaves_one_item_selected(ICast cast, Lazy<BrowseTheWeb> webBrowserFactory)
    {
      var joe = cast.GetJoe(webBrowserFactory);

      joe.ShouldIgnoreThisTestIfTheirBrowserHasAnyOfTheFlags(
        Flags.HtmlElements.Select.CannotChangeState
      );

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Deselect.ItemNumber(2).From(PageTwo.MultiSelectionList));

      Then(joe).ShouldSee(TheText.Of(PageTwo.MultiSelectionValue)).Should().Be("meat");
    }

    [Test,Screenplay]
    [Description("Deselecting by text leaves one item selected.")]
    public void DeselectByText_leaves_one_item_selected(ICast cast, Lazy<BrowseTheWeb> webBrowserFactory)
    {
      var joe = cast.GetJoe(webBrowserFactory);

      joe.ShouldIgnoreThisTestIfTheirBrowserHasAnyOfTheFlags(Flags.HtmlElements.Select.CannotChangeState);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Deselect.Item("Steak").From(PageTwo.MultiSelectionList));

      Then(joe).ShouldSee(TheText.Of(PageTwo.MultiSelectionValue)).Should().Be("veg");
    }

    [Test,Screenplay]
    [Description("Deselecting by value leaves one item selected.")]
    public void DeselectByValue_leaves_one_item_selected(ICast cast, Lazy<BrowseTheWeb> webBrowserFactory)
    {
      var joe = cast.GetJoe(webBrowserFactory);

      joe.ShouldIgnoreThisTestIfTheirBrowserHasAnyOfTheFlags(Flags.HtmlElements.Select.CannotChangeState);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Deselect.ItemValued("meat").From(PageTwo.MultiSelectionList));

      Then(joe).ShouldSee(TheText.Of(PageTwo.MultiSelectionValue)).Should().Be("veg");
    }
  }
}
