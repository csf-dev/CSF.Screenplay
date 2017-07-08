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
    readonly ApplicationUri uri;

    protected override string GetReport(INamed actor)
    {
      if(uri != null)
      {
        return $"{actor.Name} opens their browser at {uri.Uri.OriginalString}";
      }

      return $"{actor.Name} opens their browser on {page.GetName()}.";
    }

    protected override void PerformAs(IPerformer actor)
    {
      var ability = actor.GetAbility<BrowseTheWeb>();
      var targetUri = GetUri(ability);
      Navigate(targetUri, ability);
    }

    ApplicationUri GetAppUri()
    {
      return uri?? page.GetUri();
    }

    Uri GetUri(BrowseTheWeb ability)
    {
      var appUri = GetAppUri();
      var transformer = ability.UrlTransformer;
      return transformer.Transform(uri);
    }

    void Navigate(Uri targetUri, BrowseTheWeb ability)
    {
      var driver = ability.WebDriver;
      driver.Url = targetUri.AbsoluteUri;
    }

    public Open(Page page)
    {
      if(page == null)
        throw new ArgumentNullException(nameof(page));
 
      this.page = page;
    }

    public Open(string url)
    {
      if(url == null)
        throw new ArgumentNullException(nameof(url));

      this.uri = new ApplicationUri(url);
    }

    public Open(Uri uri)
    {
      if(uri == null)
        throw new ArgumentNullException(nameof(uri));

      this.uri = new ApplicationUri(uri);
    }

    public Open(ApplicationUri uri)
    {
      if(uri == null)
        throw new ArgumentNullException(nameof(uri));

      this.uri = uri;
    }
  }
}
