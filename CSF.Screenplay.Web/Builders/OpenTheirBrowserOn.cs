using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Builders
{
  public class OpenTheirBrowserOn
  {
    public static IPerformable ThePage<TPage>() where TPage : Page,new()
    {
      var page = new TPage();
      return ThePage(page);
    }

    public static IPerformable ThePage(Page page)
    {
      return new Actions.Open(page);
    }

    public static IPerformable TheUrl(string url)
    {
      return new Actions.Open(url);
    }

    public static IPerformable TheUrl(IUriProvider uri)
    {
      return new Actions.Open(uri);
    }
  }
}
