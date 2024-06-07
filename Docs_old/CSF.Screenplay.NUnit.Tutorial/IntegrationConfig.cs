using CSF.Screenplay.NUnit;
using CSF.Screenplay.Integration;

[assembly: ScreenplayAssembly(typeof(IntegrationConfig))]

public class IntegrationConfig : IIntegrationConfig
{
  public void Configure(IIntegrationConfigBuilder builder)
  {
  }
}