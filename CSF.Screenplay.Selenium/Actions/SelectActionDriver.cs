using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.Models;
using OpenQA.Selenium;
using CSF.Selenium.Support.UI;

namespace CSF.Screenplay.Selenium.Actions
{
  /// <summary>
  /// Base type for action drivers which deal with HTML <c>select</c> elements.
  /// </summary>
  public abstract class SelectActionDriver : IActionDriver
  {
    /// <summary>
    /// Gets a human-readable report of the action.
    /// </summary>
    /// <returns>The report.</returns>
    /// <param name="actor">Actor.</param>
    /// <param name="targetName">The name of the target of this action.</param>
    public abstract string GetReport(INamed actor, string targetName);

    /// <summary>
    /// Performs the action using the given actor, web-browsing ability and target element.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="ability">Ability.</param>
    /// <param name="element">Element.</param>
    public void PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter element)
    {
      if(ability.FlagsDriver.HasFlag(Flags.HtmlElements.Select.CannotChangeState))
      {
        PerformUsingWorkaround(actor, ability, element);
        return;
      }

      var selectElement = GetSelectElement(ability, element);
      PerformAs(actor, ability, element, selectElement);
    }

    /// <summary>
    /// Performs the action using a JavaScript workaround instead of the normal mechanism.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="ability">Ability.</param>
    /// <param name="element">Element.</param>
    protected abstract void PerformUsingWorkaround(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter element);

    /// <summary>
    /// Performs the action using the given actor, web-browsing ability and target element.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="ability">Ability.</param>
    /// <param name="element">Element.</param>
    /// <param name="select">The select element</param>
    protected abstract void PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter element, SelectElement select);

    /// <summary>
    /// Gets an implementation of <see cref="SelectElement"/> which is appropriate for the web-browsing
    /// ability.
    /// </summary>
    /// <returns>The select element implementation.</returns>
    /// <param name="ability">The web-browsing ability.</param>
    /// <param name="element">The HTML select element.</param>
    protected virtual SelectElement GetSelectElement(BrowseTheWeb ability, IWebElementAdapter element)
    {
      var webElement = element.GetUnderlyingElement();

      if(ability.FlagsDriver.HasFlag(Flags.HtmlElements.SelectMultiple.RequiresCtrlClickToToggleOptionSelection))
        return new SelectElementUsingModifierKey(webElement, Keys.Control, ability.WebDriver);

      return new SelectElement(webElement);
    }
  }
}
