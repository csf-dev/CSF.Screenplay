using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.NUnit;

namespace CSF.Screenplay.Web.Tests.Questions
{
  [ScreenplayTest]
  [Description("Reading the value of CSS properties")]
  public class GetCssValueTests
  {
    readonly ScreenplayContext context;

    public GetCssValueTests(ScreenplayContext context)
    {
      if(context == null)
        throw new ArgumentNullException(nameof(context));
      this.context = context;
    }

    [Test]
    [Description("Reading the value of the 'color' property detects the expected value.")]
    public void GetCssValue_for_red_string_gets_correct_colour()
    {
      var joe = context.GetCast().Get("joe");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      Then(joe).ShouldSee(TheCss.Property("color").From(HomePage.ImportantString))
               .Should().MatchRegex(@"^rgba?\(255, *0, *0(, *1)?\)$");
    }
  }
}
