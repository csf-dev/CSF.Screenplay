using System;
namespace CSF.Screenplay.SpecFlow
{
  /// <summary>
  /// Configuration for the SpecFlow/Screenplay integration.
  /// </summary>
  public interface IScreenplayConfiguration
  {
    /// <summary>
    /// Gets the <c>System.Type</c> of the Screenplay integration configuration type to use.
    /// </summary>
    /// <returns>The integration config type.</returns>
    Type GetIntegrationConfigType();
  }
}
