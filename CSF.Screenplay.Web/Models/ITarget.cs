using System;
using System.Collections.Generic;
using CSF.Screenplay.Web.Abilities;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Models
{
  /// <summary>
  /// An object which is capable of identifying one or more elements upon a web page.  In essence this is a form of
  /// locator, or search criteria for page elements.
  /// </summary>
  /// <remarks>
  /// <para>
  /// Targets have both a way of locating elements which the Selenium WebDriver may use, and also a human-readable name.
  /// For example, the locator might represent a CSS selector which identifies a login button such
  /// as <c>.login_form button.submit.login</c>.  However, in that same example the human-readable name might be
  /// <c>the login button</c>.
  /// </para>
  /// </remarks>
  public interface ITarget : IHasTargetName
  {
    /// <summary>
    /// Gets a web element adapter from the current instance, using the given web-browsing ability.
    /// </summary>
    /// <returns>The web element adapter.</returns>
    /// <param name="ability">Ability.</param>
    IWebElementAdapter GetWebElementAdapter(BrowseTheWeb ability);

    /// <summary>
    /// Gets a collection of web element adapters from the current instance, using the given web-browsing ability.
    /// </summary>
    /// <returns>The web element adapters.</returns>
    /// <param name="ability">Ability.</param>
    ElementCollection GetWebElementAdapters(BrowseTheWeb ability);

    /// <summary>
    /// Gets a web element adapter from the current instance, using a given Selenium web driver.
    /// </summary>
    /// <returns>The web element adapter.</returns>
    /// <param name="driver">The web driver.</param>
    IWebElementAdapter GetWebElementAdapter(IWebDriver driver);

    /// <summary>
    /// Gets a collection of web element adapters from the current instance, using a given Selenium web driver.
    /// </summary>
    /// <returns>The web element adapters.</returns>
    /// <param name="driver">The web driver.</param>
    ElementCollection GetWebElementAdapters(IWebDriver driver);
  }
}
