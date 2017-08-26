using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using CSF.Screenplay.NUnit;
using FluentAssertions;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Web.Tests.Actions
{
  [TestFixture]
  [Description("The deselect action")]
  public class DeselectTests
  {
    [Test,Screenplay]
    [Description("Deselecting everything leaves nothing selected.")]
    public void DeselectAll_leaves_nothing_selected(IScreenplayScenario scenario)
    {
      IgnoreOn.Browsers("Deselecting items from a multi-select is broken in Edge.  https://github.com/SeleniumHQ/selenium/issues/4490",
                        BrowserName.Edge);

      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Deselect.EverythingFrom(PageTwo.MultiSelectionList));

      Then(joe).ShouldSee(TheText.Of(PageTwo.MultiSelectionValue)).Should().Be("Nothing!");
    }

    [Test,Screenplay]
    [Description("Deselecting by index leaves one item selected.")]
    public void DeselectByIndex_leaves_one_item_selected(IScreenplayScenario scenario)
    {
      IgnoreOn.Browsers("Deselecting items from a multi-select is broken in Edge.  https://github.com/SeleniumHQ/selenium/issues/4490",
                        BrowserName.Edge);

      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Deselect.ItemNumber(2).From(PageTwo.MultiSelectionList));

      Then(joe).ShouldSee(TheText.Of(PageTwo.MultiSelectionValue)).Should().Be("meat");
    }

    [Test,Screenplay]
    [Description("Deselecting by text leaves one item selected.")]
    public void DeselectByText_leaves_one_item_selected(IScreenplayScenario scenario)
    {
      IgnoreOn.Browsers("Deselecting items from a multi-select is broken in Edge.  https://github.com/SeleniumHQ/selenium/issues/4490",
                        BrowserName.Edge);

      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Deselect.Item("Steak").From(PageTwo.MultiSelectionList));

      Then(joe).ShouldSee(TheText.Of(PageTwo.MultiSelectionValue)).Should().Be("veg");
    }

    [Test,Screenplay]
    [Description("Deselecting by value leaves one item selected.")]
    public void DeselectByValue_leaves_one_item_selected(IScreenplayScenario scenario)
    {
      IgnoreOn.Browsers("Deselecting items from a multi-select is broken in Edge.  https://github.com/SeleniumHQ/selenium/issues/4490",
                        BrowserName.Edge);

      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Deselect.ItemValued("meat").From(PageTwo.MultiSelectionList));

      Then(joe).ShouldSee(TheText.Of(PageTwo.MultiSelectionValue)).Should().Be("veg");
    }
  }
}
