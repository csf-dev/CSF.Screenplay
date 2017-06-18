using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Actions
{
  public class Open : Performable
  {
    readonly Page page;
    readonly string url;

    protected override string GetReport(INamed actor)
    {
      if(page != null)
        return $"{actor.Name} opens their browser on {page.GetName()}.";

      return $"{actor.Name} opens their browser at '{url}'.";
    }

    protected override void PerformAs(IPerformer actor)
    {
      var driver = actor.GetAbility<BrowseTheWeb>().WebDriver;
      driver.Url = GetUrl();
    }

    string GetUrl()
    {
      if(page != null)
        return page.GetUrl();

      return url;
    }

    public Open(Page page = null, string url = null)
    {
      if(page == null && url == null)
        throw new ArgumentException("Both page and URL cannot be null.");
      
      this.page = page;
      this.url = url;
    }
  }
}
