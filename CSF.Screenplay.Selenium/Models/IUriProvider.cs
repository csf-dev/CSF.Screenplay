using System;
namespace CSF.Screenplay.Web.Models
{
  /// <summary>
  /// A type which is capable of providing a URI.
  /// </summary>
  public interface IUriProvider
  {
    /// <summary>
    /// Gets the URI.
    /// </summary>
    /// <value>The URI.</value>
    Uri Uri { get; }
  }
}
