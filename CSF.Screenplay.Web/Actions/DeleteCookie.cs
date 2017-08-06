using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;

namespace CSF.Screenplay.Web.Actions
{
  public class DeleteCookie : Performable
  {
    string cookieName;

    protected override string GetReport(INamed actor)
      => $"{actor.Name} deletes the browser cookie '{cookieName}'.";

    protected override void PerformAs(IPerformer actor)
    {
      var browseTheWeb = actor.GetAbility<BrowseTheWeb>();

      var cookies = browseTheWeb.WebDriver.Manage().Cookies;

      cookies.DeleteCookieNamed(cookieName);
    }

    public DeleteCookie(string cookieName)
    {
      if(cookieName == null)
        throw new ArgumentNullException(nameof(cookieName));

      this.cookieName = cookieName;
    }
  }
}
