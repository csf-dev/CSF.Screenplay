using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Models
{
  /// <summary>
  /// Indicates an <see cref="ITarget"/> which may provide a Selenium <c>By</c> locator.
  /// </summary>
  public interface ILocatorBasedTarget : ITarget
  {
    /// <summary>
    /// Gets a Selenium WebDriver <c>By</c> implementation for the current instance.
    /// </summary>
    /// <returns>A Selenium WebDriver locator instance.</returns>
    By GetWebDriverLocator();
  }
}
