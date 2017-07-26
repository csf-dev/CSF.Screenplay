using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace CSF.Screenplay.Web.Actions
{
  /// <summary>
  /// An action driver whereby a user selects an option element by its human-readable text.
  /// </summary>
  public class SelectByText : SelectActionDriver
  {
    readonly string text;

    /// <summary>
    /// Gets a human-readable report of the action.
    /// </summary>
    /// <returns>The report.</returns>
    /// <param name="actor">Actor.</param>
    /// <param name="targetName">The name of the target of this action.</param>
    public override string GetReport(INamed actor, string targetName)
    {
      return $"{actor.Name} selects '{text}' from {targetName}.";
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
      select.SelectByText(text);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Web.Actions.SelectByText"/> class.
    /// </summary>
    /// <param name="text">Text.</param>
    public SelectByText(string text)
    {
      if(text == null)
        throw new ArgumentNullException(nameof(text));

      this.text = text;
    }
  }
}
