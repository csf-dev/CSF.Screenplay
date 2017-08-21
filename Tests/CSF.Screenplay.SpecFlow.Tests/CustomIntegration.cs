using System;
using CSF.Screenplay.Integration;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.SpecFlow.Tests
{
  public class CustomIntegration : ScreenplayIntegration
  {
    protected override IServiceRegistrationProvider GetRegistrationProvider() => new ScreenplayRegistrations();

    protected override void AfterExecutedLastScenario(IServiceResolver serviceResolver)
    {
      WriteReport(serviceResolver);
    }

    protected override void BeforeExecutingFirstScenario(IProvidesTestRunEvents testRunEvents,
                                                         IServiceResolver serviceResolver)
    {
      SubscribeReporterToTestRunBeginAndEnd(testRunEvents, serviceResolver);
      SubscribeReporterToCastEvents(serviceResolver);
    }

    protected override void CustomiseScenario(ScreenplayScenario scenario)
    {
      SubscribeReporterToScenarioEvents(scenario);
    }

    protected override void AfterScenario(ScreenplayScenario scenario)
    {
      UnsubscribeReporterFromScenarioEvents(scenario);
      DismissCast(scenario);
    }

    void SubscribeReporterToTestRunBeginAndEnd(IProvidesTestRunEvents testRunEvents,
                                               IServiceResolver serviceResolver)
    {
      var reporter = serviceResolver.GetReporter();
      reporter.Subscribe(testRunEvents);
    }

    void SubscribeReporterToCastEvents(IServiceResolver serviceResolver)
    {
      var cast = serviceResolver.GetCast();
      var reporter = serviceResolver.GetReporter();

      cast.ActorCreated += (sender, e) => {
        reporter.Subscribe(e.Actor);
      };
    }

    void SubscribeReporterToScenarioEvents(ScreenplayScenario scenario)
    {
      var reporter = scenario.GetReporter();
      reporter.Subscribe(scenario);
    }

    void UnsubscribeReporterFromScenarioEvents(ScreenplayScenario scenario)
    {
      var reporter = scenario.GetReporter();
      reporter.Unsubscribe(scenario);
    }

    void DismissCast(ScreenplayScenario scenario)
    {
      var cast = scenario.GetCast();
      cast.Dismiss();
    }

    void WriteReport(IServiceResolver serviceResolver)
    {
      var reporter = serviceResolver.GetReportBuildingReporter();

      var path = "SpecFlow.report.txt";
      var report = reporter.GetReport();

      TextReportWriter.WriteToFile(report, path);
    }
  }
}
