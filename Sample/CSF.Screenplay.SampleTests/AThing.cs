using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.SampleTests
{
  public class AThing : Performable<string>
  {
    readonly string value;

    public override string PerformAs(IPerformer actor)
    {
      var ability = actor.GetAbility<SampleAbility>();
      return String.Concat(value, ability.GetSpecialNumber().ToString());
    }

    public AThing(string value = null)
    {
      this.value = value;
    }
  }
}
