using System;
using System.Collections.Generic;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Actions
{
  public class WebElementProvider
  {
    static readonly WebElementProvider singleton;

    public IWebElement GetElement(BrowseTheWeb ability, ITarget target)
    {
      var locator = target.GetWebDriverLocator();
      var webDriver = ability.WebDriver;
      return webDriver.FindElement(locator);
    }

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
