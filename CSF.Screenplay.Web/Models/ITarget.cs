using System;
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
    /// Gets a Selenium WebDriver <c>By</c> implementation for the current instance.
    /// </summary>
    /// <returns>A Selenium WebDriver locator instance.</returns>
    By GetWebDriverLocator();
  }
}
