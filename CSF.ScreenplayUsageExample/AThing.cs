using System;
using CSF.Screenplay.Actions;

namespace CSF.Screenplay.Example
{
  public class AThing : Actions.Action<string>
  {
    public string Value { get; set; }

    protected override void Execute(string parameters)
    {
      throw new NotImplementedException();
    }
  }
}
