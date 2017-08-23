using System;
using CSF.Screenplay.Integration;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.SpecFlow.Tests
{
  public class ScreenplayRegistrations : ServiceRegistrationProvider
  {
    protected override void RegisterServices(IServiceRegistryBuilder builder)
    {
      builder.RegisterDefaultModelBuildingReporter();
      builder.RegisterCast();
    }
  }
}
