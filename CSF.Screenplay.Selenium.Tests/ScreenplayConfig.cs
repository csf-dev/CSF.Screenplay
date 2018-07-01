using System;
using System.IO;
using CSF.Screenplay.Integration;
using CSF.Screenplay.NUnit;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Reporting.Models;
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.Reporting;
using CSF.Screenplay.Selenium.Tests;

[assembly: ScreenplayAssembly(typeof(ScreenplayConfig))]

namespace CSF.Screenplay.Selenium.Tests
{
  public class ScreenplayConfig : IIntegrationConfig
  {
    public void Configure(IIntegrationConfigBuilder builder)
    {
      builder.UseReporting(config => {
        config
          .SubscribeToActorsCreatedInCast()
          .WriteReport(WriteReport)
          .WithFormatter<StringArrayFormatter>()
          .WithFormatter<OptionCollectionFormatter>()
          .WithFormatter<ElementCollectionFormatter>()
          .WithFormatter<TargetNameFormatter>();
      });
      builder.UseSharedUriTransformer(new RootUriPrependingTransformer("http://localhost:8080/"));
      builder.UseWebDriverFromConfiguration();
      builder.UseWebBrowser();
      builder.UseBrowserFlags();
      builder.SaveScreenshotsInDirectoryPerScenario(GetScreenshotRoot());
    }

    string GetScreenshotRoot() => Path.Combine(Environment.CurrentDirectory, "saved-screenshots");

    void WriteReport(IObjectFormattingService formatter, Report report)
    {
      var path = "NUnit.report.json";
      TextReportWriter.WriteToFile(report, path, formatter);
    }
  }
}
