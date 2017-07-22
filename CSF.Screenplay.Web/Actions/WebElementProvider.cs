using System;
using System.Collections.Generic;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Actions
{
  /// <summary>
  /// Helper which converts <see cref="ITarget"/> instances into Selenium <c>IWebElement</c> instances.
  /// </summary>
  public class WebElementProvider
  {
    static readonly WebElementProvider singleton;

    /// <summary>
    /// Gets a single element.
    /// </summary>
    /// <returns>The element.</returns>
    /// <param name="ability">Ability.</param>
    /// <param name="target">Target.</param>
    public IWebElement GetElement(BrowseTheWeb ability, ITarget target)
    {
      var locator = target.GetWebDriverLocator();
      var webDriver = ability.WebDriver;
      return webDriver.FindElement(locator);
    }

    /// <summary>
    /// Gets the a collection of elements.
    /// </summary>
    /// <returns>The elements.</returns>
    /// <param name="ability">Ability.</param>
    /// <param name="target">Target.</param>
    public IReadOnlyList<IWebElement> GetElements(BrowseTheWeb ability, ITarget target)
    {
      var locator = target.GetWebDriverLocator();
      var webDriver = ability.WebDriver;
      return webDriver.FindElements(locator);
    }

    static WebElementProvider()
    {
      singleton = new WebElementProvider();
    }

    internal static WebElementProvider Instance => singleton;
  }
}
