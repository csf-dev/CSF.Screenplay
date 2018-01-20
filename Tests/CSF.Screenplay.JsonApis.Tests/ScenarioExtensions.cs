using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.JsonApis.Abilities;

namespace CSF.Screenplay.JsonApis.Tests
{
  public static class ScenarioExtensions
  {
    public static IActor GetJoe(this IScreenplayScenario scenario)
    {
      var joe = scenario.CreateActor("Joe");
      var consumeWebServices = new ConsumeJsonWebServices("http://localhost:8080/api");
      joe.IsAbleTo(consumeWebServices);
      return joe;
    }
  }
}
