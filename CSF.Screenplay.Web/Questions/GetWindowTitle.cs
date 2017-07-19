using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;

namespace CSF.Screenplay.Web.Questions
{
  public class GetWindowTitle : Question<string>
  {
    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
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
