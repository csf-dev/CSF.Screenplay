using System;

namespace CSF.Screenplay
{
  public static class StepComposer
  {
    public static IGivenActor Given(Actor actor)
    {
      return Compose(actor);
    }

    public static IWhenActor When(Actor actor)
    {
      return Compose(actor);
    }

    public static IThenActor Then(Actor actor)
    {
      return Compose(actor);
    }

    static IActor Compose(Actor actor)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));

      return actor;
    }

    public static ExpectationComposer<TAnswer> SeeThat<TAnswer>(IQuestion<TAnswer> question)
    {
      return new ExpectationComposer<TAnswer>(question);
    }
  }
}
