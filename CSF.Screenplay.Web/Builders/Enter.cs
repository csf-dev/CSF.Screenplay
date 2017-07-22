using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  /// <summary>
  /// Builds an action representing an actor entering text into a page element.
  /// </summary>
  public class Enter
  {
    readonly string val;

    /// <summary>
    /// The actor enters the text into a given <see cref="ITarget"/>.
    /// </summary>
    /// <returns>A performable action instance.</returns>
    /// <param name="target">Target.</param>
    public IPerformable Into(ITarget target)
    {
      return new Actions.TargettedAction(target, new Actions.Enter(val));
    }

    /// <summary>
    /// The actor enters the text into a given web element.
    /// </summary>
    /// <returns>A performable action instance.</returns>
    /// <param name="element">Element.</param>
    public IPerformable Into(IWebElement element)
    {
      return new Actions.TargettedAction(element, new Actions.Enter(val));
    }

    /// <summary>
    /// Indicates the text that the actor is to enter.
    /// </summary>
    /// <returns>A builder instance accepting further configuration.</returns>
    /// <param name="val">The text to be entered.</param>
    public static Enter TheText(string val)
    {
      return new Enter(val);
    }

    Enter(string val)
    {
      if(val == null)
        throw new ArgumentNullException(nameof(val));
      
      this.val = val;
    }
  }
}
