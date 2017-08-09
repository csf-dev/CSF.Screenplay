using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.NUnit;

namespace CSF.Screenplay.Web.Tests.Actions
{
  [ScreenplayFixture]
  [Description("The select action")]
  public class SelectTests
  {
    readonly ScreenplayContext context;

    public SelectTests(ScreenplayContext context)
    {
      if(context == null)
        throw new ArgumentNullException(nameof(context));
      this.context = context;
    }

    [Test]
    [Description("Selecting by text generates the expected result on the page.")]
    public void SelectByText_generates_expected_result_on_page()
    {
      var joe = context.GetCast().GetOrCreate("joe");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Select.Item("Two").From(PageTwo.SingleSelectionList));

      Then(joe).ShouldSee(TheText.From(PageTwo.SingleSelectionValue).As<int>()).Should().Be(2);
    }

    [Test]
    [Description("Selecting by index generates the expected result on the page.")]
    public void SelectByIndex_generates_expected_result_on_page()
    {
      var joe = context.GetCast().GetOrCreate("joe");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Select.ItemNumber(3).From(PageTwo.SingleSelectionList));

      Then(joe).ShouldSee(TheText.From(PageTwo.SingleSelectionValue).As<int>()).Should().Be(3);
    }

    [Test]
    [Description("Selecting by value generates the expected result on the page.")]
    public void SelectByValue_generates_expected_result_on_page()
    {
      var joe = context.GetCast().GetOrCreate("joe");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Select.ItemValued("1").From(PageTwo.SingleSelectionList));

      Then(joe).ShouldSee(TheText.From(PageTwo.SingleSelectionValue).As<int>()).Should().Be(1);
    }
  }
}
