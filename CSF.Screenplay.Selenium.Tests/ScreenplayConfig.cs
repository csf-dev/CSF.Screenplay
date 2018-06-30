using System;
using System.IO;
using CSF.Screenplay.Integration;
using CSF.Screenplay.NUnit;
using CSF.Screenplay.ReportFormatting;
using CSF.Screenplay.Reporting;
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
          .WithScenarioRenderer(JsonScenarioRenderer.CreateForFile("NUnit.report.txt"))
          .WithFormattingStrategy<StringCollectionFormattingStrategy>()
          .WithFormattingStrategy<OptionCollectionFormatter>()
          .WithFormattingStrategy<ElementCollectionFormatter>()
          .WithFormattingStrategy<TargetNameFormatter>();
      });
      builder.UseSharedUriTransformer(new RootUriPrependingTransformer("http://localhost:8080/"));
      builder.UseWebDriverFromConfiguration();
      builder.UseWebBrowser();
      builder.UseBrowserFlags();
      builder.SaveScreenshotsInDirectoryPerScenario(GetScreenshotRoot());
    }

    string GetScreenshotRoot() => Path.Combine(Environment.CurrentDirectory, "saved-screenshots");
  }
}
