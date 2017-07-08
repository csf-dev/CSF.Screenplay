using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.SampleTests
{
  public class DoAThing : Performable
  {
    protected override void PerformAs(IPerformer actor)
    {
      actor.Perform(new AThing(value: "The parameter"));
    }
  }
}
