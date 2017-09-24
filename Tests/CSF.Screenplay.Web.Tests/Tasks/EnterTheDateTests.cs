using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using FluentAssertions;
using CSF.Screenplay.NUnit;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Actors;
using System;

namespace CSF.Screenplay.Web.Tests.Tasks
{
  [TestFixture]
  [Description("Entering dates")]
  public class EnterTheDateTests
  {
    [Test,Screenplay]
    [Description("Entering a date into an HTML 5 input field should work cross-browser")]
    public void Navigate_to_a_slow_loading_page_finds_the_correct_page(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();
      var date = new DateTime(2012, 5, 6);
      var expectedString = date.ToString("yyyy-MM-dd");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Enter.TheDate(date).Into(PageTwo.DateInput));

      Then(joe).ShouldSee(TheText.Of(PageTwo.DateOutput))
               .Should()
               .Be(expectedString, because: "the displayed date should match");
    }
  }
}
