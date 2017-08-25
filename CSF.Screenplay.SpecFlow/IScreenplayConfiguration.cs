using System;
namespace CSF.Screenplay.SpecFlow
{
  /// <summary>
  /// Configuration for the SpecFlow/Screenplay integration.
  /// </summary>
  public interface IScreenplayConfiguration
  {
    Type GetIntegrationConfigType();
  }
}
