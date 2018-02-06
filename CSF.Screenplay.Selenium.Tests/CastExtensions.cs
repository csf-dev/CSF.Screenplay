using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.Selenium.Abilities;

namespace CSF.Screenplay.Selenium.Tests
{
  public static class CastExtensions
  {
    public static IActor GetJoe(this ICast cast, Lazy<BrowseTheWeb> webBrowser)
    {
      if(cast == null)
        throw new ArgumentNullException(nameof(cast));
      if(webBrowser == null)
        throw new ArgumentNullException(nameof(webBrowser));

      return cast.Get("Joe", a => {
        a.IsAbleTo(webBrowser.Value);
      });
    }
  }
}
