using System;
namespace CSF.Screenplay.SpecFlow
{
  /// <summary>
  /// Configuration for the SpecFlow/Screenplay integration.
  /// </summary>
  public interface IScreenplayConfiguration
  {
    /// <summary>
    /// Gets the assembly-qualified type name for the Screenplay Integration class to be used.
    /// </summary>
    /// <value>The name of the integration assembly qualified.</value>
    string IntegrationAssemblyQualifiedName { get; }
  }
}
