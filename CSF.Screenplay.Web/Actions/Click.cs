using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Actions
{
  /// <summary>
  /// An action driver representing a user clicking upon an element.
  /// </summary>
  public class Click : IActionDriver
  {
    /// <summary>
    /// Gets a human-readable report of the action.
    /// </summary>
    /// <returns>The report.</returns>
    /// <param name="actor">Actor.</param>
    /// <param name="targetName">The name of the target of this action.</param>
    public string GetReport(INamed actor, string targetName)
    {
      return $"{actor.Name} clicks on {targetName}";
    }

    /// <summary>
    /// Performs the action using the given actor, web-browsing ability and target element.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="ability">Ability.</param>
    /// <param name="element">Element.</param>
    public void PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter element)
    {
      element.GetUnderlyingElement().Click();
    }
  }
}
