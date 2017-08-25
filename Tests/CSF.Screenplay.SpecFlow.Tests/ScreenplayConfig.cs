using CSF.Screenplay.Integration;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay.SpecFlow.Tests
{
  public class ScreenplayConfig : IIntegrationConfig
  {
    public void Configure(IIntegrationConfigBuilder builder)
    {
      builder.UseCast();
      builder.UseReporter(config => {
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
