using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.SpecFlow.Tests.Abilities;

namespace CSF.Screenplay.SpecFlow.Tests.Actions
{
  public class AddTheNumber : Performable
  {
    int number;

    public AddTheNumber(int number)
    {
      this.number = number;
    }

    protected override void PerformAs(IPerformer actor)
    {
      var ability = actor.GetAbility<AddNumbers>();
      ability.Add(number);
    }

    protected override string GetReport(INamed actor) => $"{actor.Name} adds {number}";
  }

  public class Add
  {
    public static IPerformable TheNumber(int number)
    {
      return new AddTheNumber(number);
    }
  }
}
