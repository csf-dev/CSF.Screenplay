using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Models
{
  /// <summary>
  /// Base type for <see cref="ITarget"/> implementations which use a <c>System.String</c> to identify the
  /// element(s) that they match.
  /// </summary>
  public abstract class StringBasedTarget : ITarget
  {
    readonly string identifier, name;

    string IHasTargetName.GetName()
    {
      return name;
    }

    By ITarget.GetWebDriverLocator()
    {
      return GetWebDriverLocator(identifier);
    }

    /// <summary>
    /// Gets a Selenium WebDriver <c>By</c> implementation using the given identifier.
    /// </summary>
    /// <returns>A Selenium WebDriver locator instance.</returns>
    /// <param name="identifier">The identifier which will be used to get the locator.</param>
    protected abstract By GetWebDriverLocator(string identifier);

    /// <summary>
    /// Initializes a new instance of the <see cref="StringBasedTarget"/> class.
    /// </summary>
    /// <param name="identifier">The identifier for the target.</param>
    /// <param name="name">The human-readable target name.</param>
    public StringBasedTarget(string identifier, string name)
    {
      if(identifier == null)
        throw new ArgumentNullException(nameof(identifier));
      if(name == null)
        throw new ArgumentNullException(nameof(name));

      this.identifier = identifier;
      this.name = name;
    }
  }
}
