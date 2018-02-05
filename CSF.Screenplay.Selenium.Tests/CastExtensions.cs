using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.Selenium.Abilities;

namespace CSF.Screenplay.Selenium.Tests
{
  public static class CastExtensions
  {
    public static IActor GetJoe(this ICast cast, Func<BrowseTheWeb> webBrowserFactory)
    {
      if(cast == null)
        throw new ArgumentNullException(nameof(cast));
      if(webBrowserFactory == null)
        throw new ArgumentNullException(nameof(webBrowserFactory));

      return cast.Get("Joe", a => {
        var browseTheWeb = webBrowserFactory();
        a.IsAbleTo(browseTheWeb);
      });
    }
  }
}
