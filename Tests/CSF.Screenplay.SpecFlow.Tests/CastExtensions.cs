using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.SpecFlow.Tests.Abilities;

namespace CSF.Screenplay.SpecFlow.Tests
{
  public static class CastExtensions
  {
    public static IActor GetMathsWhiz(this ICast cast, string name)
    {
      return cast.Get(name, CustomiseActor);
    }

    static void CustomiseActor(IActor actor)
    {
      actor.IsAbleTo<AddNumbers>();
    }
  }
}
