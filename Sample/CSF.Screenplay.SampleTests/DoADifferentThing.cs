using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.SampleTests
{
  public class DoADifferentThing : Performable
  {
    protected override void PerformAs(IPerformer actor)
    {
      throw new NotImplementedException();
    }
  }
}
