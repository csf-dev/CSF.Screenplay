using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Models
{
  /// <summary>
  /// An <see cref="ITarget"/> which finds elements based upon a CSS selector string.
  /// </summary>
  public class CssSelector : StringBasedTarget
  {
    static readonly CssSelector all;

    /// <summary>
    /// Static convenience property indicating all elements.  This is the selector string <c>*</c>
    /// </summary>
    /// <value>The 'all elements' selector.</value>
    public static CssSelector AllElements => all;

    /// <summary>
    /// Gets a Selenium WebDriver <c>By</c> implementation using the given identifier.
    /// </summary>
    /// <returns>A Selenium WebDriver locator instance.</returns>
    /// <param name="identifier">The identifier which will be used to get the locator.</param>
    protected override By GetWebDriverLocator(string identifier)
    {
      return By.CssSelector(identifier);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CssSelector"/> class.
    /// </summary>
    /// <param name="selector">The selector string.</param>
    /// <param name="name">The human-readable target name.</param>
    public CssSelector(string selector, string name) : base(selector, name) {}

    /// <summary>
    /// Initializes the <see cref="CssSelector"/> class.
    /// </summary>
    static CssSelector()
    {
      all = new CssSelector("*", "everything");
    }
  }
}
