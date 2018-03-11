﻿using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Models;
using CSF.Selenium.Support.UI;

namespace CSF.Screenplay.Selenium.Actions
{
  /// <summary>
  /// An action driver whereby a user selects an option element by its underlying value.
  /// </summary>
  public class SelectByValue : SelectActionDriver
  {
    readonly string value;

    /// <summary>
    /// Gets a human-readable report of the action.
    /// </summary>
    /// <returns>The report.</returns>
    /// <param name="actor">Actor.</param>
    /// <param name="targetName">The name of the target of this action.</param>
    public override string GetReport(INamed actor, string targetName)
    {
      return $"{actor.Name} selects option <{value}> from {targetName}.";
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
      select.SelectByValue(value);
    }

    /// <summary>
    /// Performs the action using a JavaScript workaround instead of the normal mechanism.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="ability">Ability.</param>
    /// <param name="element">Element.</param>
    protected override void PerformUsingWorkaround(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter element)
    {
      actor.Perform(Execute.JavaScript.WhichSelectsTheOptionFrom(element).ByValue(value));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Actions.SelectByValue"/> class.
    /// </summary>
    /// <param name="value">Value.</param>
    public SelectByValue(string value)
    {
      if(value == null)
        throw new ArgumentNullException(nameof(value));

      this.value = value;
    }
  }
}
