using System;
using CSF.Screenplay;

namespace CSF.ScreenplayUsageExample
{
  public class DoADifferentThingTask : ITask
  {
    public void Execute(ICanPerformActions actor)
    {
      throw new NotImplementedException();
    }
  }
}
