using System;
using CSF.Screenplay.Actions;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Example
{
  public class AThing : Actions.Action<string>
  {
    public string Value { get; set; }

    protected override void Execute(IPerformer performer, string parameters)
    {
      throw new NotImplementedException();
    }
  }
}
