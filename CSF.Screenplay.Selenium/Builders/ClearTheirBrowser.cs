using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Actions;

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// Builds actions which relate to clearing state held by the browser itself.
  /// </summary>
  public class ClearTheirBrowser
  {
    /// <summary>
    /// Deletes all of the cookies from the actor's web browser, for the current site.
    /// </summary>
    /// <returns>A performable action instance.</returns>
    public static IPerformable Cookies()
    {
      return new ClearCookies();
    }

    /// <summary>
    /// Deletes a single named cookie from the actor's web browser.
    /// </summary>
    /// <returns>A performable action instance.</returns>
    /// <param name="cookieName">Cookie name.</param>
    public static IPerformable CookieNamed(string cookieName)
    {
      return new DeleteCookie(cookieName);
    }

    /// <summary>
    /// Clears the HTML5 local storage for the current site within the actor's web browser.
    /// </summary>
    /// <returns>A performable action instance.</returns>
    public static IPerformable LocalStorage()
    {
      return new ClearLocalStorage();
    }
  }
}
