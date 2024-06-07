using System;
using CSF.Screenplay.Abilities;

namespace CSF.Screenplay.SpecFlow.Tests.Abilities
{
  public class AddNumbers : Ability
  {
    int current = 0;

    protected override string GetReport(Actors.INamed actor) => $"{actor.Name} is able to add numbers together";

    public void Add(int number)
    {
      current += number;
    }

    public void Set(int number)
    {
      current = number;
    }

    public int GetTotal() => current;
  }
}
