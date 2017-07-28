using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Builders
{
  /// <summary>
  /// Builds an action representing an actor opening their browser directly on a page or URL.
  /// </summary>
  public class OpenTheirBrowserOn
  {
    /// <summary>
    /// Opens the browser on a page indicated by the given generic parameter.
    /// </summary>
    /// <returns>A performable action instance.</returns>
    /// <typeparam name="TPage">The page type, indicating the page to open.</typeparam>
    public static IPerformable ThePage<TPage>() where TPage : Page,new()
    {
      var page = new TPage();
      return ThePage(page);
    }

    /// <summary>
    /// Opens the browser on the given page.
    /// </summary>
    /// <returns>A performable action instance.</returns>
    /// <param name="page">Page.</param>
    public static IPerformable ThePage(Page page)
    {
      return new Actions.Open(page);
    }

    /// <summary>
    /// Opens the browser on the given string URL.
    /// </summary>
    /// <returns>A performable action instance.</returns>
    /// <param name="url">URL.</param>
    public static IPerformable TheUrl(string url)
    {
      return new Actions.Open(url);
    }

    /// <summary>
    /// Opens the browser on the given URI.
    /// </summary>
    /// <returns>A performable action instance.</returns>
    /// <param name="uri">URI.</param>
    public static IPerformable TheUrl(IUriProvider uri)
    {
      return new Actions.Open(uri);
    }
  }
}
