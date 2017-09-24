using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;

namespace CSF.Screenplay.Web.Actions
{
  /// <summary>
  /// An action in which the actor clears all browser cookies for the current website.
  /// </summary>
  public class ClearCookies : Performable
  {
    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
      => $"{actor.Name} clears all browser cookies for the current site.";

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
    protected override void PerformAs(IPerformer actor)
    {
      var browseTheWeb = actor.GetAbility<BrowseTheWeb>();

      browseTheWeb.DemandCapability(Capabilities.ClearDomainCookies);

      var cookies = browseTheWeb.WebDriver.Manage().Cookies;
      cookies.DeleteAllCookies();
    }
  }
}
