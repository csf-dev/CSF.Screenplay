using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Questions;
using CSF.Screenplay.Tasks;

namespace CSF.Screenplay
{
  public static class StepComposer
  {
    public static IGivenActor Given(Actor actor)
    {
      return actor;
    }

    public static IWhenActor When(Actor actor)
    {
      return actor;
    }

    public static IThenActor Then(Actor actor)
    {
      return actor;
    }
  }
}
