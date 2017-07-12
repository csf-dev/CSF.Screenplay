using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Web.Tests.Actions
{
  [TestFixture]
  public class SelectTests
  {
    Actor joe;

    [SetUp]
    public void Setup()
    {
      joe = WebdriverTestSetup.GetJoe();
    }

    [Test]
    public void SelectByText_generates_expected_result_on_page()
    {
      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Select.Item("Two").From(PageTwo.SingleSelectionList));

      Then(joe).ShouldSee(TheText.From(PageTwo.SingleSelectionValue).As<int>()).Should().Be(2);
    }

    [Test]
    public void SelectByIndex_generates_expected_result_on_page()
    {
      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Select.ItemNumber(3).From(PageTwo.SingleSelectionList));

      Then(joe).ShouldSee(TheText.From(PageTwo.SingleSelectionValue).As<int>()).Should().Be(3);
    }

    [Test]
    public void SelectByValue_generates_expected_result_on_page()
    {
      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Select.ItemValued("1").From(PageTwo.SingleSelectionList));

      Then(joe).ShouldSee(TheText.From(PageTwo.SingleSelectionValue).As<int>()).Should().Be(1);
    }
  }
}
