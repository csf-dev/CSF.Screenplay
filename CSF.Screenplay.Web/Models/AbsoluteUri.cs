using System;
namespace CSF.Screenplay.Web.Models
{
  public class AbsoluteUri : AppUri, IUriProvider
  {
    public AbsoluteUri(string uri, string siteName = null) : base(new Uri(uri, UriKind.Absolute), siteName) {}

    public AbsoluteUri(Uri uri, string siteName = null) : base(uri, siteName) {}
  }
}
