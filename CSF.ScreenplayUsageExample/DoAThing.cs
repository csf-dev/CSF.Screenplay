using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Tasks;

namespace CSF.Screenplay.Example
{
  public class DoAThing : ITask
  {
    public void PerformAs(IPerformer actor)
    {
      var doAThing = actor.GetAction<AThing>();
      actor.Perform(doAThing, "The parameter");
    }
  }
}
