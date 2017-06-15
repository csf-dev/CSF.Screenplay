using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Example
{
  public class AThing : Performable<string>
  {
    readonly string value;

    public override string PerformAs(IPerformer actor)
    {
      throw new NotImplementedException();
    }

    public AThing(string value = null)
    {
      this.value = value;
    }
  }
}
