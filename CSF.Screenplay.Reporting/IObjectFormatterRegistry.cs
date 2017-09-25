using System;
using System.Collections.Generic;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Registry type which holds a collection of object formatters and provides access to them.
  /// </summary>
  public interface IObjectFormatterRegistry
  {
    /// <summary>
    /// Adds a formatter to the collection.
    /// </summary>
    /// <param name="formatter">Formatter.</param>
    void AddFormatter(IObjectFormatter formatter);

    /// <summary>
    /// Gets a collection of all of the registered formatters.
    /// </summary>
    /// <returns>The all formatters.</returns>
    IReadOnlyCollection<IObjectFormatter> GetAllFormatters();
  }
}
