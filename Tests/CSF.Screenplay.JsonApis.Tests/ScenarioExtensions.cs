using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.JsonApis.Abilities;

namespace CSF.Screenplay.JsonApis.Tests
{
  public static class ScenarioExtensions
  {
    public static IActor GetJoe(this IScenario scenario)
    {
      var cast = scenario.GetCast();
      return cast.Get("Joe", (actor, s) => {
        var consumeWebServices = new ConsumeJsonWebServices("http://localhost:8080/api/");
        actor.IsAbleTo(consumeWebServices);
      }, scenario);
    }
  }
}
