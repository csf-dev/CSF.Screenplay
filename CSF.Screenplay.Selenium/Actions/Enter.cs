using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
  /// <summary>
  /// An action driver representing a user entering some text into a target.
  /// </summary>
  public class Enter : IActionDriver
  {
    readonly string text;

    /// <summary>
    /// Gets a human-readable report of the action.
    /// </summary>
    /// <returns>The report.</returns>
    /// <param name="actor">Actor.</param>
    /// <param name="targetName">The name of the target of this action.</param>
    public string GetReport(INamed actor, string targetName)
    {
      return $"{actor.Name} types '{text}' into {targetName}";
    }

    /// <summary>
    /// Performs the action using the given actor, web-browsing ability and target element.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="ability">Ability.</param>
    /// <param name="element">Element.</param>
    public void PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter element)
    {
      element.GetUnderlyingElement().SendKeys(text);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Actions.Enter"/> class.
    /// </summary>
    /// <param name="text">Text.</param>
    public Enter(string text)
    {
      if(text == null)
        throw new ArgumentNullException(nameof(text));
      
      this.text = text;
    }
  }
}
