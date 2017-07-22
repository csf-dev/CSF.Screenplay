using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  /// <summary>
  /// Builds an action representing an actor clicking on an element on the page.
  /// </summary>
  public class Click
  {
    /// <summary>
    /// The actor clicks on a given <see cref="ITarget"/>.
    /// </summary>
    /// <returns>A performable action instance.</returns>
    /// <param name="target">Target.</param>
    public static IPerformable On(ITarget target)
    {
      return new Actions.TargettedAction(target, new Actions.Click());
    }

    /// <summary>
    /// The actor clicks on a given web element.
    /// </summary>
    /// <returns>A performable action instance.</returns>
    /// <param name="element">Element.</param>
    public static IPerformable On(IWebElement element)
    {
      return new Actions.TargettedAction(element, new Actions.Click());
    }
  }
}
