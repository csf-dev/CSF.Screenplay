using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.NUnit;
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Tests.Pages;
using FluentAssertions;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Selenium.Tests.Tasks
{
  [TestFixture]
  [Description("Entering dates")]
  public class EnterTheDateTests
  {
    [Test,Screenplay]
    [Description("Entering a date into an HTML 5 input field should work cross-browser")]
    public void Enter_TheDate_puts_the_correct_value_into_the_control(ICast cast, BrowseTheWeb browseTheWeb)
    {
      var joe = cast.Get("Joe");joe.IsAbleTo(browseTheWeb);

      // https://github.com/csf-dev/CSF.Screenplay/issues/109
      joe.ShouldIgnoreThisTestIfTheirBrowserHasAnyOfTheFlags(Flags.HtmlElements.InputTypeDate.CannotClearDateInteractively);

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
