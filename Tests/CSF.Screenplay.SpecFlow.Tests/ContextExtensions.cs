using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.SpecFlow.Tests.Abilities;

namespace CSF.Screenplay.SpecFlow.Tests
{
  public static class ContextExtensions
  {
    public static IActor GetMathsWhiz(this ScreenplayContext context, string name)
    {
      IActor actor;

      var cast = context.GetCast();
      if(cast == null)
      {
        actor = new Actor(name);
        actor.IsAbleTo<AddNumbers>();
        return actor;
      }

      actor = cast.GetExisting(name);
      if(actor != null)
        return actor;

      actor = cast.Get(name);
      actor.IsAbleTo<AddNumbers>();
      return actor;
    }
  }
}
