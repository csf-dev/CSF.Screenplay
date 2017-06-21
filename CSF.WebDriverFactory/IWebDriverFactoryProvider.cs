using System;
namespace CSF.WebDriverFactory
{
  /// <summary>
  /// Provides an entry point to this API, creating and returning an instance of <see cref="IWebDriverFactory"/>.
  /// </summary>
  public interface IWebDriverFactoryProvider
  {
    /// <summary>
    /// Gets the web driver factory.
    /// </summary>
    /// <returns>The factory.</returns>
    IWebDriverFactory GetFactory();
  }
}
