using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.Models;

namespace CSF.Screenplay.Selenium.Actions
{
  /// <summary>
  /// Action driver which clears the contents of the target HTML element which has user-editable content.
  /// </summary>
  public class ClearTheContents : IActionDriver
  {
    /// <summary>
    /// Gets a human-readable report of the action.
    /// </summary>
    /// <returns>The report.</returns>
    /// <param name="actor">Actor.</param>
    /// <param name="targetName">The name of the target of this action.</param>
    public string GetReport(INamed actor, string targetName)
      => $"{actor.Name} clears {targetName}";

    /// <summary>
    /// Performs the action using the given actor, web-browsing ability and target element.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="ability">Ability.</param>
    /// <param name="element">Element.</param>
    public void PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter element)
      => element.GetUnderlyingElement().Clear();
  }
}
