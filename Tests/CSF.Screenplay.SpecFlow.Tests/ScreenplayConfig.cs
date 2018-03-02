using CSF.Screenplay.Integration;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.SpecFlow;
using CSF.Screenplay.SpecFlow.Tests.Abilities;

[assembly: ScreenplayAssembly(typeof(CSF.Screenplay.SpecFlow.Tests.ScreenplayConfig))]

namespace CSF.Screenplay.SpecFlow.Tests
{
  public class ScreenplayConfig : IIntegrationConfig
  {
    public void Configure(IIntegrationConfigBuilder builder)
    {
      builder.UseReporting(config => {
        config
          .SubscribeToActorsCreatedInCast()
          .WithFormatter<Stopwatch.TimeSpanFormatter>()
          .WriteReport((formatter, report) => {
          var path = "SpecFlow.report.txt";
          TextReportWriter.WriteToFile(report, path, formatter);
        });
      });

      builder.ServiceRegistrations.PerScenario.Add(b => {
        b.RegisterType<AddNumbers>();
      });
    }
  }
}
