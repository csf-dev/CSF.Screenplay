using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;

namespace CSF.Screenplay.Web.Actions
{
  public class ClearCookies : Performable
  {
    protected override string GetReport(INamed actor)
      => $"{actor.Name} clears all browser cookies for the current site.";

    protected override void PerformAs(IPerformer actor)
    {
      var browseTheWeb = actor.GetAbility<BrowseTheWeb>();

      var cookies = browseTheWeb.WebDriver.Manage().Cookies;

      cookies.DeleteAllCookies();
    }
  }
}
