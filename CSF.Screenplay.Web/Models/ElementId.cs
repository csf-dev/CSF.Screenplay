using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Models
{
  /// <summary>
  /// An <see cref="ITarget"/> which finds elements based upon an HTML id attribute.
  /// </summary>
  public class ElementId : StringBasedTarget
  {
    /// <summary>
    /// Gets a Selenium WebDriver <c>By</c> implementation using the given identifier.
    /// </summary>
    /// <returns>A Selenium WebDriver locator instance.</returns>
    /// <param name="identifier">The identifier which will be used to get the locator.</param>
    protected override By GetWebDriverLocator(string identifier)
    {
      return By.Id(identifier);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ElementId"/> class.
    /// </summary>
    /// <param name="id">The HTML id.</param>
    /// <param name="name">The human-readable target name.</param>
    public ElementId(string id, string name) : base(id, name) {}
  }
}
