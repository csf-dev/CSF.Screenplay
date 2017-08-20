using System;
namespace CSF.Screenplay.Web.Tests
{
  public class ScreenplayAttribute : NUnit.ScreenplayAttribute
  {
    protected override void CustomiseScenario(ScreenplayScenario scenario)
    {
      scenario.SubscribeReporterToScenarioEvents();
      scenario.SubscribeReporterToCastActorCreation();
      scenario.DismissCastAfterEachScenario();
    }
  }
}
