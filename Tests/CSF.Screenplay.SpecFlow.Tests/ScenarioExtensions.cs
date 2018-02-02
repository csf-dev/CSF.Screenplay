using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.SpecFlow.Tests.Abilities;

namespace CSF.Screenplay.SpecFlow.Tests
{
  public static class ScenarioExtensions
  {
    public static IActor GetMathsWhiz(this IScenario context, string name)
    {
      var cast = context.Resolver.GetCast();
      if(cast == null)
      {
        var actor = new Actor(name, context.Identity);
        CustomiseActor(actor);
        return actor;
      }

      return cast.Get(name, CustomiseActor);
    }

    static void CustomiseActor(IActor actor)
    {
      actor.IsAbleTo<AddNumbers>();
    }
  }
}
