using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using FluentAssertions;
using CSF.Screenplay.NUnit;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Tests.Waits
{
  [TestFixture]
  [Description("Waiting for a page load")]
  public class WaitUntilThePageLoadsTests
  {
    [Test,Screenplay]
    [Description("If the actor waits for a page-load, then the operation blocks until the page is ready.")]
    public void Wait_UntilThePageLoads_blocks_operation_until_page_is_ready(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());
      Given(joe).WasAbleTo(Click.On(HomePage.SlowLoadingLink));

      When(joe).AttemptsTo(Wait.UntilThePageLoads());

      Then(joe).ShouldSee(TheText.Of(HomePage.LoadDelay))
               .Should()
               .Be("2 seconds");
    }
  }
}
