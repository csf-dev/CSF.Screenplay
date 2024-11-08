using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Models
{
  /// <summary>
  /// An <see cref="ITarget"/> which finds elements based upon an XPath expression string.
  /// </summary>
  public class XPath : StringBasedTarget
  {
    /// <summary>
    /// Gets a Selenium WebDriver <c>By</c> implementation using the given identifier.
    /// </summary>
    /// <returns>A Selenium WebDriver locator instance.</returns>
    /// <param name="identifier">The identifier which will be used to get the locator.</param>
    protected override By GetWebDriverLocator(string identifier)
    {
      return By.XPath(identifier);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XPath"/> class.
    /// </summary>
    /// <param name="expression">The XPath expression string.</param>
    /// <param name="name">The human-readable target name.</param>
    public XPath(string expression, string name) : base(expression, name) {}
  }
}
