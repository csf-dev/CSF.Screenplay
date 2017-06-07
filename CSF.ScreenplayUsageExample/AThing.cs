using System;
using CSF.Screenplay.Actions;

namespace CSF.Screenplay.Example
{
  public class AThing : Actions.Action
  {
    public string Value { get; set; }

    protected override void Execute()
    {
      throw new NotImplementedException();
    }
  }
}
