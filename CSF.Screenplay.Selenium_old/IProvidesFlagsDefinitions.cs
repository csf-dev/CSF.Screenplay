using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;

namespace CSF.Screenplay.Selenium
{
  /// <summary>
  /// A service which can read and provide a collection of flags definitions, possibly from multiple sources.
  /// </summary>
  public interface IProvidesFlagsDefinitions
  {
    /// <summary>
    /// Gets the flags definitions.
    /// </summary>
    /// <returns>The flags definitions.</returns>
    IReadOnlyCollection<FlagsDefinition> GetFlagsDefinitions();
  }
}
