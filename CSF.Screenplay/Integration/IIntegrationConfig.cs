using System;
namespace CSF.Screenplay.Integration
{
  public interface IIntegrationConfig
  {
    void Configure(IIntegrationConfigBuilder builder);
  }
}
