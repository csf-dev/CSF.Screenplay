using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Example
{
  public class ThereAreLights : Question<string>
  {
    protected override string GetAnswer(IPerformer actor)
    {
      return "Bright";
    }
  }
}
