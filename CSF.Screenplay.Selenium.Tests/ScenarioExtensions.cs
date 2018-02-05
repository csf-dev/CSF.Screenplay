using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Selenium.Tests
{
  public static class ScenarioExtensions
  {
    public static IActor GetJoe(this IScreenplayScenario scenario)
    {
      if(scenario == null)
        throw new ArgumentNullException(nameof(scenario));

      var cast = scenario.GetCast();
      var joe = cast.Get("Joe", CustomiseJoe, scenario);
      return joe;
    }

    static void CustomiseJoe(IActor joe, IScreenplayScenario scenario)
    {
      var browseTheWeb = scenario.Resolver.GetWebBrowser();
      joe.IsAbleTo(browseTheWeb);
    }
  }
}
