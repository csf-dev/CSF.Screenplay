using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Tasks;
using static CSF.Screenplay.ActionComposer;

namespace CSF.Screenplay.Example
{
  public class DoAThing : ITask
  {
    public void Execute(IPerformer actor)
    {
      actor.AttemptsTo(Perform<AThing>().With(x => x.Value = "foo bar").With(x => x.Value = "baz"));
    }
  }
}
