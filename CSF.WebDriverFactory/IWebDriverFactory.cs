using System;
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
    /// Gets the web driver.
    /// </summary>
    /// <returns>The web driver.</returns>
    IWebDriver GetWebDriver();
  }
}
