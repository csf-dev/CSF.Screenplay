using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.SpecFlow.Tests.Abilities;

namespace CSF.Screenplay.SpecFlow.Tests.Actions
{
  public class HasTheNumber : Performable
  {
    int number;

    public HasTheNumber(int number)
    {
      this.number = number;
    }

    protected override void PerformAs(IPerformer actor)
    {
      var ability = actor.GetAbility<AddNumbers>();
      ability.Set(number);
    }

    protected override string GetReport(INamed actor) => $"{actor.Name} has the number {number}";
  }

  public class StartWith
  {
    public static IPerformable TheNumber(int number)
    {
      return new HasTheNumber(number);
    }
  }
}
