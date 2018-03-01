using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.JsonApis.Abilities;

namespace CSF.Screenplay.JsonApis.Tests
{
  public static class CastExtensions
  {
    public static IActor GetJoe(this ICast cast)
    {
      return cast.Get("Joe", (actor) => {
        var consumeWebServices = new ConsumeJsonWebServices("http://localhost:8080/api/");
        actor.IsAbleTo(consumeWebServices);
      });
    }
  }
}
