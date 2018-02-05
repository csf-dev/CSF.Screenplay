using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Tests.Pages;
using CSF.Screenplay.NUnit;
using FluentAssertions;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.Selenium.Tests.Tasks;

namespace CSF.Screenplay.Selenium.Tests.Actions
{
  [TestFixture]
  [Description("The 'Execute Javascript' action")]
  public class ExecuteScriptTests
  {
    [Test,Screenplay]
    [Description("Executing some JavaScript affects the page in the expected manner")]
    public void ExecuteTheJavaScript_results_in_changed_page_content(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      When(joe).AttemptsTo(Execute.TheJavaScript("window.myCallableScript();").AndIgnoreTheResult());

      Then(joe).ShouldSee(TheText.Of(PageTwo.JavaScriptResult))
               .Should()
               .Be("myCallableScript called", because: "the Javascript was executed");
    }

    [Test,Screenplay]
    [Description("Executing some JavaScript and getting the result returns the expected value")]
    public void ExecuteTheJavaScriptAndGetTheResult_returns_correct_value(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      var result = When(joe).AttemptsTo(Execute.TheJavaScript("return window.addFive();").AndGetTheResult());

      result.Should().Be(6L, because: "the Javascript was executed");
    }

    [Test,Screenplay]
    [Description("Executing some JavaScript with a parameter and getting the result returns the expected value")]
    public void ExecuteTheJavaScriptAndGetTheResult_with_params_returns_correct_value(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      var result = When(joe).AttemptsTo(Execute.TheJavaScript("return window.addFive(arguments[0]);").WithTheParameters(5).AndGetTheResult());

      result.Should().Be(10L, because: "the Javascript was executed");
    }
  }
}
