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
  [Description("Reading the text of an element")]
  public class GetTextTests
  {
    readonly ScreenplayContext context;

    public GetTextTests(ScreenplayContext context)
    {
      if(context == null)
        throw new ArgumentNullException(nameof(context));
      this.context = context;
    }

    [Test]
    [Description("Reading the text of an element detects the expected value.")]
    public void GetText_returns_expected_value()
    {
      var joe = context.GetCast().Get("joe");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      Then(joe).ShouldSee(TheText.Of(HomePage.ImportantString)).Should().Be("banana!");
    }

    [Test]
    [Description("Reading the text of an element and converting it to a number detects the expected value.")]
    public void GetConvertedText_returns_expected_value()
    {
      var joe = context.GetCast().Get("joe");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      Then(joe).ShouldSee(TheText.From(HomePage.ImportantNumber).As<int>()).Should().Be(42);
    }
  }
}
