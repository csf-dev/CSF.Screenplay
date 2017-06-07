using System;
using CSF.Screenplay;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Questions;

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
