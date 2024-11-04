using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Selenium.Abilities;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Models
{
  /// <summary>
  /// Base type for <see cref="ITarget"/> implementations which use a <c>System.String</c> to identify the
  /// element(s) that they match.
  /// </summary>
  public abstract class StringBasedTarget : ILocatorBasedTarget
  {
    readonly string identifier, name;

    /// <summary>
    /// Gets the value of the string element identifier;
    /// </summary>
    /// <value>The identifier value.</value>
    public string IdentifierValue => identifier;

    string IHasTargetName.GetName() => name;

    By ILocatorBasedTarget.GetWebDriverLocator() => GetWebDriverLocator(identifier);

    /// <summary>
    /// Gets a web element adapter from the current instance, using the given web-browsing ability.
    /// </summary>
    /// <returns>The web element adapter.</returns>
    /// <param name="ability">Ability.</param>
    public IWebElementAdapter GetWebElementAdapter(BrowseTheWeb ability)
    {
      if(ability == null)
        throw new ArgumentNullException(nameof(ability));

      return GetWebElementAdapter(ability.WebDriver);
    }

    /// <summary>
    /// Gets a collection of web element adapters from the current instance, using the given web-browsing ability.
    /// </summary>
    /// <returns>The web element adapters.</returns>
    /// <param name="ability">Ability.</param>
    public ElementCollection GetWebElementAdapters(BrowseTheWeb ability)
    {
      if(ability == null)
        throw new ArgumentNullException(nameof(ability));

      return GetWebElementAdapters(ability.WebDriver);
    }

    /// <summary>
    /// Gets a web element adapter from the current instance, using a given Selenium web driver.
    /// </summary>
    /// <returns>The web element adapter.</returns>
    /// <param name="driver">The web driver.</param>
    public IWebElementAdapter GetWebElementAdapter(IWebDriver driver)
    {
      if(driver == null)
        throw new ArgumentNullException(nameof(driver));

      var locator = GetWebDriverLocator(identifier);
      var element = GetElement(locator, driver);
      return new SeleniumWebElementAdapter(element, name);
    }

    /// <summary>
    /// Gets a collection of web element adapters from the current instance, using a given Selenium web driver.
    /// </summary>
    /// <returns>The web element adapters.</returns>
    /// <param name="driver">The web driver.</param>
    public ElementCollection GetWebElementAdapters(IWebDriver driver)
    {
      if(driver == null)
        throw new ArgumentNullException(nameof(driver));

      var locator = GetWebDriverLocator(identifier);
      var elements = driver.FindElements(locator);
      return new ElementCollection(elements.Select(x => new SeleniumWebElementAdapter(x, name)).ToArray(), name);
    }

    /// <summary>
    /// Gets a Selenium WebDriver <c>By</c> implementation using the given identifier.
    /// </summary>
    /// <returns>A Selenium WebDriver locator instance.</returns>
    /// <param name="identifier">The identifier which will be used to get the locator.</param>
    protected abstract By GetWebDriverLocator(string identifier);

    IWebElement GetElement(By locator, IWebDriver driver)
    {
      try
      {
        return driver.FindElement(locator);
      }
      catch(NoSuchElementException ex)
      {
        throw new TargetNotFoundException("The required element was not found on the screen.", ex) {
          Target = this
        };
      }
    }

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
