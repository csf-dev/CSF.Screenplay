using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Actions
{
  /// <summary>
  /// A service which 'drives' a single interaction with a web element.
  /// </summary>
  public interface IActionDriver
  {
    /// <summary>
    /// Gets a human-readable report of the action.
    /// </summary>
    /// <returns>The report.</returns>
    /// <param name="actor">Actor.</param>
    /// <param name="targetName">The name of the target of this action.</param>
    string GetReport(INamed actor, string targetName);

    /// <summary>
    /// Performs the action using the given actor, web-browsing ability and target element.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="ability">Ability.</param>
    /// <param name="element">Element.</param>
    void PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter element);
  }
}
