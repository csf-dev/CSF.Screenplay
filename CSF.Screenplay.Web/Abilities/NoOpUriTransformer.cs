using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Abilities
{
  /// <summary>
  /// Implementation of <see cref="IUriTransformer"/> which performs a no-op transformation upon the URI.
  /// </summary>
  public class NoOpUriTransformer : IUriTransformer
  {
    /// <summary>
    /// Transform the URI exposed by the given <see cref="IUriProvider"/> into a <c>System.Uri</c> instance.
    /// </summary>
    /// <param name="uriProvider">The URI provider.</param>
    public Uri Transform(IUriProvider uriProvider) => uriProvider.Uri;
  }
}
