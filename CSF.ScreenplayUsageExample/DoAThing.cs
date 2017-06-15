using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Example
{
  public class DoAThing : Performable
  {
    public override void PerformAs(IPerformer actor)
    {
      actor.Perform(new AThing(value: "The parameter"));
    }
  }
}
