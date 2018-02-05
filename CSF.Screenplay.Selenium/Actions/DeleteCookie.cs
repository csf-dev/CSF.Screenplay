using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Abilities;

namespace CSF.Screenplay.Selenium.Actions
{
  /// <summary>
  /// An action in which the actor deletes a single named browser cookie.
  /// </summary>
  public class DeleteCookie : Performable
  {
    string cookieName;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
      => $"{actor.Name} deletes the browser cookie '{cookieName}'.";

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
    protected override void PerformAs(IPerformer actor)
    {
      var browseTheWeb = actor.GetAbility<BrowseTheWeb>();

      var cookies = browseTheWeb.WebDriver.Manage().Cookies;

      cookies.DeleteCookieNamed(cookieName);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Actions.DeleteCookie"/> class.
    /// </summary>
    /// <param name="cookieName">Cookie name.</param>
    public DeleteCookie(string cookieName)
    {
      if(cookieName == null)
        throw new ArgumentNullException(nameof(cookieName));

      this.cookieName = cookieName;
    }
  }
}
