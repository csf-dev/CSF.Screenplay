using System;
namespace CSF.Screenplay.Integration
{
  /// <summary>
  /// In order to integrate Screenplay with your testing framework of choice, you must write a class which
  /// implements this interface.  Your implementation will provide the integration information in order to make
  /// Screenplay work, including all of the extensions you wish to use.
  /// </summary>
  public interface IIntegrationConfig
  {
    /// <summary>
    /// Configures Screenplay using a configuration builder type.
    /// </summary>
    /// <param name="builder">Builder.</param>
    void Configure(IIntegrationConfigBuilder builder);
  }
}
