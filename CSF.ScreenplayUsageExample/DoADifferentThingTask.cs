using System;
using CSF.Screenplay;

namespace CSF.Screenplay.Example
{
  public class DoADifferentThingTask : ITask
  {
    public void Execute(ICanPerformActions actor)
    {
      throw new NotImplementedException();
    }
  }
}
