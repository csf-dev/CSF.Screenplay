using System;
using CSF.Screenplay.Selenium.Models;

namespace CSF.Screenplay.Selenium.Abilities
{
  /// <summary>
  /// A service which is able to transform a URI provided by a <see cref="IUriProvider"/> into a different
  /// <c>System.Uri</c> instance.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This type is particularly useful when application URIs are given as relative URI fragments.  Then, a transformer
  /// may be used to 'complete' those and make them full/absolute URI instances.
  /// Such a transformer may be configured to work with the individual test environment upon which the tests are
  /// executing.  The relative URIs remain the same across all environments, but they are completed into absolute
  /// URIs by <see cref="IUriTransformer"/> implementations according to configuration.
  /// </para>
  /// </remarks>
  public interface IUriTransformer
  {
    /// <summary>
    /// Transform the URI exposed by the given <see cref="IUriProvider"/> into a <c>System.Uri</c> instance.
    /// </summary>
    /// <param name="uriProvider">The URI provider.</param>
    Uri Transform(IUriProvider uriProvider);
  }
}
