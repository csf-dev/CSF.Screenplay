using CSF.Screenplay.Integration;
using CSF.Screenplay.JsonApis.ObjectFormatters;
using CSF.Screenplay.JsonApis.Tests;
using CSF.Screenplay.NUnit;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Reporting.Models;

[assembly: ScreenplayAssembly(typeof(ScreenplayConfig))]

namespace CSF.Screenplay.JsonApis.Tests
{
  public class ScreenplayConfig : IIntegrationConfig
  {
    public void Configure(IIntegrationConfigBuilder builder)
    {
      builder.UseCast();
      builder.UseReporting(config => {
        config
          .SubscribeToActorsCreatedInCast()
          .WithFormatter<TimeoutExceptionFormatter>()
          .WithFormatter<JsonApiExceptionFormatter>()
          .WriteReport(WriteReport);
      });
    }

    void WriteReport(IObjectFormattingService formatter, Report report)
    {
      var path = "JsonApis.report.txt";
      TextReportWriter.WriteToFile(report, path, formatter);
    }
  }
}
