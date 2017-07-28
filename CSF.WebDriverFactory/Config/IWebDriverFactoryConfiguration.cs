using System;
using System.Collections.Generic;

namespace CSF.WebDriverFactory.Config
{
  /// <summary>
  /// Represents configuration information which will enable calling code to get a <see cref="IWebDriverFactory"/>.
  /// </summary>
  public interface IWebDriverFactoryConfiguration
  {
    /// <summary>
    /// Gets the <c>System.Type</c> of web driver factory desired.
    /// </summary>
    /// <returns>The factory type.</returns>
    Type GetFactoryType();

    /// <summary>
    /// Gets a collection of name/value pairs which indicate public settable properties on the factory instance
    /// and values to set into them.
    /// </summary>
    /// <returns>The factory properties.</returns>
    IDictionary<string,string> GetFactoryProperties();
  }
}
