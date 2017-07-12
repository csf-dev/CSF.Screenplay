using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;

namespace CSF.Screenplay.Web.Questions
{
  public class GetWindowTitle : Question<string>
  {
    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} reads the window title.";
    }

    protected override string GetAnswer(IPerformer actor)
    {
      var ability = actor.GetAbility<BrowseTheWeb>();
      return ability.WebDriver.Title;
    }
  }
}
