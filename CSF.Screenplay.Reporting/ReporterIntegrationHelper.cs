using System;
using CSF.Screenplay.Integration;
using CSF.Screenplay.Reporting.Models;

namespace CSF.Screenplay.Reporting
{
  public class ReporterIntegrationHelper
  {
    #region fields

    IReporter reporterToUse;
    string name, castName;
    bool subscribeToCastActorCreation, subscribeToCastActorAddition;
    Action<IReporter> reportWritingCallback;
    Action<Report> writeReportModelCallback;

    #endregion

    #region builder methods

    public ReporterIntegrationHelper WithReporter(IReporter reporter)
    {
      if(reporter == null)
        throw new ArgumentNullException(nameof(reporter));

      this.reporterToUse = reporter;
      return this;
    }

    public ReporterIntegrationHelper Name(string name) 
    {
      this.name = name;
      return this;
    }

    public ReporterIntegrationHelper SubscribeToActorsCreatedInCast(string castName = null)
    {
      subscribeToCastActorCreation = true;
      subscribeToCastActorAddition = false;
      this.castName = castName;
      return this;
    }

    public ReporterIntegrationHelper SubscribeToActorsAddedToCast(string castName = null)
    {
      subscribeToCastActorCreation = false;
      subscribeToCastActorAddition = true;
      this.castName = castName;
      return this;
    }

    public ReporterIntegrationHelper AfterTestRun(Action<IReporter> reportWriter)
    {
      if(reportWriter == null)
        throw new ArgumentNullException(nameof(reportWriter));

      this.reportWritingCallback = reportWriter;
      writeReportModelCallback = null;
      return this;
    }

    public ReporterIntegrationHelper WriteReport(Action<Report> reportModel)
    {
      if(reportModel == null)
        throw new ArgumentNullException(nameof(reportModel));

      reportWritingCallback = null;
      writeReportModelCallback = reportModel;
      return this;
    }

    #endregion

    #region private methods

    internal void ApplyToIntegration(IScreenplayIntegrationHelper integration)
    {
      if(integration == null)
        throw new ArgumentNullException(nameof(integration));

      RegisterReporter(integration);
      SubscribeToCast(integration);
      SubscribeToBeginAndEndTestRun(integration);
      SubscribeToScenario(integration);
      WriteReport(integration);
    }

    void RegisterReporter(IScreenplayIntegrationHelper integration)
    {
      integration.RegisterServices.Add((builder) => {
        builder.RegisterReporter(reporterToUse, name);
      });
    }

    void SubscribeToCast(IScreenplayIntegrationHelper integration)
    {
      integration.BeforeFirstScenario.Add((events, resolver) => {

        if(!subscribeToCastActorCreation && !subscribeToCastActorAddition)
          return;

        var cast = resolver.GetCast(castName);
        var reporter = resolver.GetReporter(name);

        if(subscribeToCastActorCreation)
        {
          cast.ActorCreated += (sender, e) => {
            reporter.Subscribe(e.Actor);
          };
        }
        else
        {
          cast.ActorAdded += (sender, e) => {
            reporter.Subscribe(e.Actor);
          };
        }
      });
    }

    void SubscribeToBeginAndEndTestRun(IScreenplayIntegrationHelper integration)
    {
      integration.BeforeFirstScenario.Add((events, resolver) => {
        var reporter = resolver.GetReporter(name);
        reporter.Subscribe(events);
      });
    }

    void SubscribeToScenario(IScreenplayIntegrationHelper integration)
    {
      integration.BeforeScenario.Add((scenario) => {
        var reporter = scenario.GetReporter(name);
        reporter.Subscribe(scenario);
      });

      integration.AfterScenario.Add((scenario) => {
        var reporter = scenario.GetReporter(name);
        reporter.Unsubscribe(scenario);
      });
    }

    void WriteReport(IScreenplayIntegrationHelper integration)
    {
      integration.AfterLastScenario.Add((resolver) => {

        var modelReporter = resolver.GetOptionalService<IModelBuildingReporter>(name);

        if(modelReporter != null && writeReportModelCallback != null)
        {
          var report = modelReporter.GetReport();
          writeReportModelCallback(report);
          return;
        }

        var reporter = resolver.GetOptionalService<IReporter>(name);

        if(reporter != null && reportWritingCallback != null)
        {
          reportWritingCallback(reporter);
          return;
        }
      });
    }

    #endregion

  }
}
