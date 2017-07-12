using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Web.Tests.Actions
{
  [TestFixture]
  public class DeselectTests
  {
    Actor joe;

    [SetUp]
    public void Setup()
    {
      joe = WebdriverTestSetup.GetJoe();
    }

    [Test]
    public void DeselectAll_leaves_nothing_selected()
    {
      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Deselect.EverythingFrom(PageTwo.MultiSelectionList));

      // Assert
      WebdriverTestSetup.TakeScreenshot(GetType(), nameof(DeselectAll_leaves_nothing_selected));
      Then(joe).ShouldSee(TheText.Of(PageTwo.MultiSelectionValue)).Should().Be("Nothing!");
    }

    [Test]
    public void DeselectByIndex_leaves_one_item_selected()
    {
      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Deselect.ItemNumber(2).From(PageTwo.MultiSelectionList));

      // Assert
      WebdriverTestSetup.TakeScreenshot(GetType(), nameof(DeselectByIndex_leaves_one_item_selected));
      Then(joe).ShouldSee(TheText.Of(PageTwo.MultiSelectionValue)).Should().Be("meat");
    }

    [Test]
    public void DeselectByText_leaves_one_item_selected()
    {
      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Deselect.Item("Steak").From(PageTwo.MultiSelectionList));

      // Assert
      WebdriverTestSetup.TakeScreenshot(GetType(), nameof(DeselectByText_leaves_one_item_selected));
      Then(joe).ShouldSee(TheText.Of(PageTwo.MultiSelectionValue)).Should().Be("veg");
    }

    [Test]
    public void DeselectByValue_leaves_one_item_selected()
    {
      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Deselect.ItemValued("meat").From(PageTwo.MultiSelectionList));

      // Assert
      WebdriverTestSetup.TakeScreenshot(GetType(), nameof(DeselectByValue_leaves_one_item_selected));
      Then(joe).ShouldSee(TheText.Of(PageTwo.MultiSelectionValue)).Should().Be("veg");
    }
  }
}
