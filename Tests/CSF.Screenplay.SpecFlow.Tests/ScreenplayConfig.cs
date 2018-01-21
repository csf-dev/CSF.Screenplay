using CSF.Screenplay.Integration;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.SpecFlow;

[assembly: ScreenplayAssembly(typeof(CSF.Screenplay.SpecFlow.Tests.ScreenplayConfig))]

namespace CSF.Screenplay.SpecFlow.Tests
{
  public class ScreenplayConfig : IIntegrationConfig
  {
    public void Configure(IIntegrationConfigBuilder builder)
    {
      builder.UseCast();
      builder.UseStage();
      builder.UseReporting(config => {
        config
          .SubscribeToActorsCreatedInCast()
          .WriteReport(report => {
          var path = "SpecFlow.report.txt";
          TextReportWriter.WriteToFile(report, path);
        });
      });
    }
  }
}
