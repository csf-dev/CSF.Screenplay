using System;
namespace CSF.Screenplay.Web.Models
{
  public class AppUri : IUriProvider
  {
    public static readonly string DefaultSiteName = "Default";

    readonly Uri uri;
    readonly string siteName;

    public Uri Uri => uri;
    public string SiteName => siteName;

    public AppUri(string uri, string siteName = null)
    {
      if(uri == null)
        throw new ArgumentNullException(nameof(uri));

      this.uri = new Uri(uri, UriKind.Relative);
      this.siteName = siteName?? DefaultSiteName;
    }

    public AppUri(Uri uri, string siteName = null)
    {
      if(uri == null)
        throw new ArgumentNullException(nameof(uri));

      this.uri = uri;
      this.siteName = siteName?? DefaultSiteName;
    }
  }
}
