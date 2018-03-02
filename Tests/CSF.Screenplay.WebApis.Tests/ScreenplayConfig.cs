using System;
using CSF.Screenplay.Integration;
using CSF.Screenplay.NUnit;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Reporting.Models;
using CSF.Screenplay.WebApis.ObjectFormatters;

[assembly: ScreenplayAssembly(typeof(CSF.Screenplay.WebApis.Tests.ScreenplayConfig))]

namespace CSF.Screenplay.WebApis.Tests
{
  public class ScreenplayConfig : IIntegrationConfig
  {
    public void Configure(IIntegrationConfigBuilder builder)
    {
      builder.UseWebApis("http://localhost:8080/api/");
      builder.UseReporting(config => {
        config
          .SubscribeToActorsCreatedInCast()
          .WithFormatter<TimeoutExceptionFormatter>()
          .WithFormatter<WebApiExceptionFormatter>()
          .WriteReport(WriteReport);
      });
    }

    void WriteReport(IObjectFormattingService formatter, Report report)
    {
      try
      {
        var path = "JsonApis.report.txt";
        TextReportWriter.WriteToFile(report, path, formatter);
      }
      catch(Exception ex)
      {
        System.Console.WriteLine("Error writing the report");
        Console.WriteLine(ex);
      }
    }
  }
}
