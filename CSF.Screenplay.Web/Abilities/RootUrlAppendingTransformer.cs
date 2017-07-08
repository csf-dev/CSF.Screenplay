using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Abilities
{
  public class RootUrlAppendingTransformer : IUrlTransformer
  {
    readonly Uri rootUri;

    public Uri Transform(ApplicationUri uri)
    {
      if(uri == null)
        throw new ArgumentNullException(nameof(uri));

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
