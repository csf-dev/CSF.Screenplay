using System;
using CSF.Screenplay;

namespace CSF.ScreenplayUsageExample
{
  public class LightsQuestion : Question<string>
  {
    public override string GetAnswer(ICanPerformActions actor)
    {
      return "Bright";
    }
  }
}
