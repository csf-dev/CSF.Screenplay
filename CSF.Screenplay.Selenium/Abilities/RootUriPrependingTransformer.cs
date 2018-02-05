using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Abilities
{
  /// <summary>
  /// Implementation of <see cref="IUriTransformer"/> which prepends a specified root URI in front of all
  /// relative URIs which are received.
  /// </summary>
  public class RootUriPrependingTransformer : IUriTransformer
  {
    readonly Uri rootUri;

    /// <summary>
    /// Transform the URI exposed by the given <see cref="IUriProvider"/> into a <c>System.Uri</c> instance.
    /// </summary>
    /// <param name="uriProvider">The URI provider.</param>
    public Uri Transform(IUriProvider uriProvider)
    {
      if(uriProvider == null)
        throw new ArgumentNullException(nameof(uriProvider));

      if(uriProvider.Uri.IsAbsoluteUri)
        return uriProvider.Uri;

      return new Uri(rootUri, uriProvider.Uri);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RootUriPrependingTransformer"/> class with a root URI.
    /// </summary>
    /// <param name="rootUri">Root URI.</param>
    public RootUriPrependingTransformer(Uri rootUri)
    {
      if(rootUri == null)
        throw new ArgumentNullException(nameof(rootUri));

      this.rootUri = rootUri;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RootUriPrependingTransformer"/> class with a root URI string.
    /// </summary>
    /// <param name="rootUri">Root URI.</param>
    public RootUriPrependingTransformer(string rootUri)
    {
      if(rootUri == null)
        throw new ArgumentNullException(nameof(rootUri));

      this.rootUri = new Uri(rootUri, UriKind.Absolute);
    }
  }
}
