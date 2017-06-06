using System;
using CSF.Screenplay;

namespace CSF.Screenplay.Example
{
  public class LightsQuestion : Question<string>
  {
    public override string GetAnswer(ICanPerformActions actor)
    {
      return "Bright";
    }
  }
}
