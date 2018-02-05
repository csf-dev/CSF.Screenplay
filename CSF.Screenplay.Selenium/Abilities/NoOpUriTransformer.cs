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

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Web.Abilities.NoOpUriTransformer"/> class.
    /// </summary>
    NoOpUriTransformer() {}

    /// <summary>
    /// Gets a default/singleton instance.
    /// </summary>
    /// <value>The default.</value>
    public static IUriTransformer Default { get; private set; }

    /// <summary>
    /// Initializes the <see cref="T:CSF.Screenplay.Web.Abilities.NoOpUriTransformer"/> class.
    /// </summary>
    static NoOpUriTransformer()
    {
      Default = new NoOpUriTransformer();
    }
  }
}
