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
    Action<IReporter> afterTestRunCallback;
    Action<Report> writeReportCallback;

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

    public ReporterIntegrationHelper AfterTestRun(Action<IReporter> callback)
    {
      this.afterTestRunCallback = callback;
      return this;
    }

    public ReporterIntegrationHelper WriteReport(Action<Report> callback)
    {
      writeReportCallback = callback;
      return this;
    }

    #endregion

    #region private methods

    internal void ApplyToIntegration(IIntegrationConfigBuilder integration)
    {
      if(integration == null)
        throw new ArgumentNullException(nameof(integration));

      RegisterReporter(integration);
      SubscribeToCast(integration);
      SubscribeToBeginAndEndTestRun(integration);
      SubscribeToScenario(integration);
      WriteReport(integration);
    }

    void RegisterReporter(IIntegrationConfigBuilder integration)
    {
      integration.RegisterServices.Add((builder) => {
        builder.RegisterReporter(reporterToUse, name);
      });
    }

    void SubscribeToCast(IIntegrationConfigBuilder integration)
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

    void SubscribeToBeginAndEndTestRun(IIntegrationConfigBuilder integration)
    {
      integration.BeforeFirstScenario.Add((events, resolver) => {
        var reporter = resolver.GetReporter(name);
        reporter.Subscribe(events);
      });
    }

    void SubscribeToScenario(IIntegrationConfigBuilder integration)
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

    void WriteReport(IIntegrationConfigBuilder integration)
    {
      integration.AfterLastScenario.Add((resolver) => {

        var reporter = resolver.GetReporter(name);
        var modelReporter = reporter as IModelBuildingReporter;

        if(modelReporter != null && writeReportCallback != null)
        {
          var report = modelReporter.GetReport();
          writeReportCallback(report);
          return;
        }

        if(reporter != null && afterTestRunCallback != null)
        {
          afterTestRunCallback(reporter);
          return;
        }
      });
    }

    #endregion

  }
}
