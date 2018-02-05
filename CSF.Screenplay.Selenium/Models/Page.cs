using System;

namespace CSF.Screenplay.Selenium.Models
{
  /// <summary>
  /// A page within a web application.
  /// </summary>
  public abstract class Page
  {
    /// <summary>
    /// Gets the human-readable name of the page.
    /// </summary>
    /// <returns>The name.</returns>
    public abstract string GetName();

    /// <summary>
    /// Gets a URI provider instance for the page.
    /// </summary>
    /// <returns>The URI provider.</returns>
    public abstract IUriProvider GetUriProvider();
  }
}
