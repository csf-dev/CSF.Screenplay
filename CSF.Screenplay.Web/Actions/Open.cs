using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Actions
{
  /// <summary>
  /// An action which represents a user opening their web browser directly onto a given page or URL.
  /// </summary>
  public class Open : Performable
  {
    readonly Page page;
    readonly IUriProvider uriProvider;

    /// <summary>
    /// Gets a human-readable report of the action.
    /// </summary>
    /// <returns>The report.</returns>
    /// <param name="actor">Actor.</param>
    protected override string GetReport(INamed actor)
    {
      if(page == null)
      {
        return $"{actor.Name} opens their browser at {uriProvider.Uri.OriginalString}";
      }

      return $"{actor.Name} opens their browser on {page.GetName()}.";
    }

    /// <summary>
    /// Performs the action as a given actor.
    /// </summary>
    /// <param name="actor">Actor.</param>
    protected override void PerformAs(IPerformer actor)
    {
      var ability = actor.GetAbility<BrowseTheWeb>();
      var targetUri = GetUri(ability);
      Navigate(targetUri, ability);
    }

    IUriProvider GetUriProvider()
    {
      return uriProvider?? page.GetUriProvider();
    }

    Uri GetUri(BrowseTheWeb ability)
    {
      var uri = GetUriProvider();
      var transformer = ability.UriTransformer;
      return transformer.Transform(uri);
    }

    void Navigate(Uri targetUri, BrowseTheWeb ability)
    {
      var driver = ability.WebDriver;
      driver.Url = targetUri.AbsoluteUri;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Open"/> class.
    /// </summary>
    /// <param name="page">Page.</param>
    public Open(Page page)
    {
      if(page == null)
        throw new ArgumentNullException(nameof(page));
 
      this.page = page;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Open"/> class.
    /// </summary>
    /// <param name="url">URL.</param>
    public Open(string url)
    {
      if(url == null)
        throw new ArgumentNullException(nameof(url));

      this.uriProvider = AppUri.Absolute(url);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Open"/> class.
    /// </summary>
    /// <param name="uri">URI.</param>
    public Open(Uri uri)
    {
      if(uri == null)
        throw new ArgumentNullException(nameof(uri));

      this.uriProvider = new AppUri(uri);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Open"/> class.
    /// </summary>
    /// <param name="uri">URI.</param>
    public Open(IUriProvider uri)
    {
      if(uri == null)
        throw new ArgumentNullException(nameof(uri));

      this.uriProvider = uri;
    }
  }
}
