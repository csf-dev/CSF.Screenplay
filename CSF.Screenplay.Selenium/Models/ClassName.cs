using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Models
{
  /// <summary>
  /// An <see cref="ITarget"/> which finds elements based upon an HTML class attribute name.
  /// </summary>
  public class ClassName : StringBasedTarget
  {
    /// <summary>
    /// Gets a Selenium WebDriver <c>By</c> implementation using the given identifier.
    /// </summary>
    /// <returns>A Selenium WebDriver locator instance.</returns>
    /// <param name="identifier">The identifier which will be used to get the locator.</param>
    protected override By GetWebDriverLocator(string identifier)
    {
      return By.ClassName(identifier);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClassName"/> class.
    /// </summary>
    /// <param name="className">The HTML class name.</param>
    /// <param name="name">The human-readable target name.</param>
    public ClassName(string className, string name) : base(className, name) {}
  }
}
