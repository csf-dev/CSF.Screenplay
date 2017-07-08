using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Abilities
{
  public class RootUrlAppendingTransformer : IUriTransformer
  {
    readonly Uri rootUri;

    public Uri Transform(IUriProvider uri)
    {
      if(uri == null)
        throw new ArgumentNullException(nameof(uri));

      if(uri is AbsoluteUri)
        return uri.Uri;

      return new Uri(rootUri, uri.Uri);
    }

    public RootUrlAppendingTransformer(Uri rootUri)
    {
      if(rootUri == null)
        throw new ArgumentNullException(nameof(rootUri));

      this.rootUri = rootUri;
    }

    public RootUrlAppendingTransformer(string rootUri)
    {
      if(rootUri == null)
        throw new ArgumentNullException(nameof(rootUri));

      this.rootUri = new Uri(rootUri);
    }
  }
}
