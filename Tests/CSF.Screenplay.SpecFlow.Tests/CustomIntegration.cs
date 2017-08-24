using System;
using CSF.Screenplay.Integration;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.SpecFlow.Tests
{
  public class CustomIntegration : ScreenplayIntegration
  {
    protected override void CustomiseIntegration(IScreenplayIntegrationHelper integrationHelper)
    {
      integrationHelper.UseCast();
      integrationHelper.UseReporter(config => {
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
