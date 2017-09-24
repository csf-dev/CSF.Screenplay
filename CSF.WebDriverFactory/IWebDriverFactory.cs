using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace CSF.WebDriverFactory
{
  /// <summary>
  /// The main interface all factory implementations must implement.  Provides the functionality to get a fully-configured
  /// web driver.
  /// </summary>
  public interface IWebDriverFactory
  {
    /// <summary>
    /// Gets the name of the web browser that this factory will create.
    /// </summary>
    /// <returns>The browser name.</returns>
    string GetBrowserName();

    /// <summary>
    /// Gets the version of the web browser that this factory will create.
    /// </summary>
    /// <returns>The browser version.</returns>
    string GetBrowserVersion();

    /// <summary>
    /// Gets the web driver.
    /// </summary>
    /// <returns>The web driver.</returns>
    IWebDriver GetWebDriver();

    /// <summary>
    /// Gets the web driver.
    /// </summary>
    /// <returns>The web driver.</returns>
    IWebDriver GetWebDriver(IDictionary<string,object> capabilities);
  }
}
