using System;
namespace CSF.Screenplay.Web.Tests
{
  public class ScreenplayTestAttribute : NUnit.ScreenplayTestAttribute
  {
    protected override void CustomiseScenario(ScreenplayScenario scenario)
    {
      scenario.SubscribeReporterToActorCreation();

      GrantWebBrowserToAllCreatedActors(scenario);
    }

    void GrantWebBrowserToAllCreatedActors(ScreenplayScenario scenario)
    {
      var cast = scenario.GetCast();
      cast.ActorCreated += (sender, e) => {
        var browseTheWeb = scenario.GetWebBrowser();
        e.Actor.IsAbleTo(browseTheWeb);
      };
    }
  }
}
