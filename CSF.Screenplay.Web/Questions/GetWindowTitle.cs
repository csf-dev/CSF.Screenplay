using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;

namespace CSF.Screenplay.Web.Questions
{
  /// <summary>
  /// Gets the title of the current browser window/tab.
  /// </summary>
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

    /// <summary>
    /// Gets the answer to the current question.
    /// </summary>
    /// <returns>The answer.</returns>
    /// <param name="actor">The actor for whom we are asking this question.</param>
    protected override string PerformAs(IPerformer actor)
    {
      var ability = actor.GetAbility<BrowseTheWeb>();
      return ability.WebDriver.Title;
    }
  }
}
