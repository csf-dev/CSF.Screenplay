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
  [Description("Reading the title of the browser window")]
  public class GetWindowTitleTests
  {
    readonly ScreenplayContext context;

    public GetWindowTitleTests(ScreenplayContext context)
    {
      if(context == null)
        throw new ArgumentNullException(nameof(context));
      this.context = context;
    }

    [Test]
    [Description("Reading the title of the browser window, whilst on the App home page, gets the expected title.")]
    public void GetWindowTitle_returns_correct_result()
    {
      var joe = context.GetCast().GetOrCreate("joe");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      Then(joe).ShouldSee(TheWindow.Title()).Should().Be("App home page");
    }
  }
}
