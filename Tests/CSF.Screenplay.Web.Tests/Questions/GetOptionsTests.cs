using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.NUnit;

namespace CSF.Screenplay.Web.Tests.Questions
{
  [ScreenplayFixture]
  [Description("Reading options from HTML <select> elements")]
  public class GetOptionsTests
  {
    readonly ScreenplayContext context;

    public GetOptionsTests(ScreenplayContext context)
    {
      if(context == null)
        throw new ArgumentNullException(nameof(context));
      this.context = context;
    }

    [Test]
    [Description("Reading the available options reveals the expected collection of items.")]
    public void GetAllOptions_returns_expected_collection()
    {
      var joe = context.GetCast().Get("joe");

      var expected = new Models.Option[] {
        new Models.Option("One", "1"),
        new Models.Option("Two", "2"),
        new Models.Option("Three", "3"),
      };

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      Then(joe).ShouldSee(TheOptions.In(PageTwo.SingleSelectionList)).ShouldBeEquivalentTo(expected);
    }

    [Test]
    [Description("Reading the selected options reveals the expected collection of items.")]
    public void GetSelectedOptions_returns_expected_collection()
    {
      var joe = context.GetCast().Get("joe");

      var expected = new Models.Option[] {
        new Models.Option("Carrot", "veg"),
        new Models.Option("Steak", "meat"),
      };

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      Then(joe).ShouldSee(TheOptions.SelectedIn(PageTwo.MultiSelectionList)).ShouldBeEquivalentTo(expected);
    }
  }
}
