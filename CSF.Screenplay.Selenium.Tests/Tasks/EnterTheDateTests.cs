using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.NUnit;
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Tests.Pages;
using CSF.Screenplay.Selenium.Tests.Personas;
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
      var joe = cast.Get<Joe>();

      var date = new DateTime(2012, 5, 6);
      var expectedString = date.ToString("yyyy-MM-dd");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<DateInputPage>());

      When(joe).AttemptsTo(Enter.TheDate(date).Into(DateInputPage.DateInput));

      Then(joe).ShouldSee(TheText.Of(DateInputPage.DateOutput))
               .Should()
               .Be(expectedString, because: "the displayed date should match");
    }
  }
}
