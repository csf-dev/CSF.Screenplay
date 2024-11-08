using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Models;
using CSF.Selenium.Support.UI;

namespace CSF.Screenplay.Selenium.Actions
{
  /// <summary>
  /// An action driver in which the user deselects all of the contained options from inside the target element.
  /// </summary>
  public class DeselectAll : SelectActionDriver
  {
    /// <summary>
    /// Gets a human-readable report of the action.
    /// </summary>
    /// <returns>The report.</returns>
    /// <param name="actor">Actor.</param>
    /// <param name="targetName">The name of the target of this action.</param>
    public override string GetReport(INamed actor, string targetName)
    {
      return $"{actor.Name} deselects all options in {targetName}.";
    }

    /// <summary>
    /// Performs the action using a JavaScript workaround instead of the normal mechanism.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="ability">Ability.</param>
    /// <param name="element">Element.</param>
    protected override void PerformUsingWorkaround(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter element)
    {
      actor.Perform(Execute.JavaScript.WhichDeselectsEverythingFrom(element));
    }

    /// <summary>
    /// Performs the action using the given actor, web-browsing ability and target element.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="ability">Ability.</param>
    /// <param name="element">Element.</param>
    /// <param name="select">The select element</param>
    protected override void PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter element, SelectElement select)
    {
      select.DeselectAll();
    }
  }
}
