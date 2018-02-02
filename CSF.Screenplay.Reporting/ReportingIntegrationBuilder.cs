using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Integration;
using CSF.Screenplay.Reporting.Models;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Builder type which permits the configuration of reporting throughout a Screenplay test run.
  /// </summary>
  public class ReportingIntegrationBuilder
  {
    #region fields

    IReporter reporterToUse;
    string name, castName;
    bool subscribeToCastActorCreation, subscribeToCastActorAddition;
    Action<IReporter> afterTestRunCallback;
    Action<IObjectFormattingService,Report> writeReportCallback;
    readonly IObjectFormatterRegistry formatterRegistry;

    #endregion

    #region builder methods

    /// <summary>
    /// Indicates that a given reporter should be used throughout the test run.
    /// </summary>
    /// <param name="reporter">Reporter.</param>
    public ReportingIntegrationBuilder WithReporter(IReporter reporter)
    {
      if(reporter == null)
        throw new ArgumentNullException(nameof(reporter));

      reporterToUse = reporter;
      return this;
    }

    /// <summary>
    /// Adds an object formatter to the given reporter.
    /// </summary>
    /// <returns>The formatter.</returns>
    /// <typeparam name="T">The object formatter type.</typeparam>
    public ReportingIntegrationBuilder WithFormatter<T>() where T : IObjectFormatter,new()
    {
      return WithFormatter(new T());
    }

    /// <summary>
    /// Adds an object formatter to the given reporter.
    /// </summary>
    /// <returns>The formatter.</returns>
    /// <param name="formatter">Formatter.</param>
    public ReportingIntegrationBuilder WithFormatter(IObjectFormatter formatter)
    {
      formatterRegistry.AddFormatter(formatter);
      return this;
    }

    /// <summary>
    /// Names the reporter instance that the current builder is configuring.
    /// </summary>
    /// <param name="name">Name.</param>
    public ReportingIntegrationBuilder Name(string name) 
    {
      this.name = name;
      return this;
    }

    /// <summary>
    /// Causes the builder to subscribe to actors whenever they are created within a cast instance.
    /// If actors are merely added to the cast (but created externally) then the reporter will not subscribe to them.
    /// Requires a cast to be in use.
    /// </summary>
    /// <param name="castName">Cast name.</param>
    public ReportingIntegrationBuilder SubscribeToActorsCreatedInCast(string castName = null)
    {
      subscribeToCastActorCreation = true;
      subscribeToCastActorAddition = false;
      this.castName = castName;
      return this;
    }

    /// <summary>
    /// Causes the builder to subscribe to actors whenever they are added to a cast instance.
    /// This will cause the reporter to subscribe to all actors participating in the cast, those created within it and
    /// also those added to it externally.
    /// Requires a cast to be in use.
    /// </summary>
    /// <param name="castName">Cast name.</param>
    public ReportingIntegrationBuilder SubscribeToActorsAddedToCast(string castName = null)
    {
      subscribeToCastActorCreation = false;
      subscribeToCastActorAddition = true;
      this.castName = castName;
      return this;
    }

    /// <summary>
    /// Provides a callback with which the reporter may take actions after the test run is complete.
    /// </summary>
    /// <returns>The test run.</returns>
    /// <param name="callback">Callback.</param>
    public ReportingIntegrationBuilder AfterTestRun(Action<IReporter> callback)
    {
      this.afterTestRunCallback = callback;
      return this;
    }

    /// <summary>
    /// Provides a callback exposing the report created by the reporter, from which you may write that report to
    /// whatever destination you wish.
    /// Requires that the reporter in use imlpements <see cref="IModelBuildingReporter"/> (this is the default behaviour).
    /// </summary>
    /// <returns>The report.</returns>
    /// <param name="callback">Callback.</param>
    public ReportingIntegrationBuilder WriteReport(Action<Report> callback)
    {
      writeReportCallback = (formatService, report) => callback(report);
      return this;
    }

    /// <summary>
    /// Provides a callback exposing the report created by the reporter, from which you may write that report to
    /// whatever destination you wish.
    /// Requires that the reporter in use imlpements <see cref="IModelBuildingReporter"/> (this is the default behaviour).
    /// </summary>
    /// <returns>The report.</returns>
    /// <param name="callback">Callback.</param>
    public ReportingIntegrationBuilder WriteReport(Action<IObjectFormattingService,Report> callback)
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
      RegisterObjectFormatters(integration);
      SubscribeToCast(integration);
      SubscribeToBeginAndEndTestRun(integration);
      SubscribeToScenario(integration);
      WriteReport(integration);
    }

    void RegisterObjectFormatters(IIntegrationConfigBuilder integration)
    {
      integration.ServiceRegistrations.PerTestRun.Add(h => {
        h.RegisterInstance(formatterRegistry).As<IObjectFormatterRegistry>().WithName(name);
        var formatService = new ObjectFormattingService(formatterRegistry);
        h.RegisterInstance(formatService).As<IObjectFormattingService>().WithName(name);
      });
    }

    void RegisterReporter(IIntegrationConfigBuilder integration)
    {
      var reporter = reporterToUse ?? new ReportBuildingReporter();

      integration.ServiceRegistrations.PerTestRun.Add(h => {
        h.RegisterInstance(reporter).As<IReporter>().WithName(name);

        if(reporter is IModelBuildingReporter)
          h.RegisterInstance(reporter).As<IModelBuildingReporter>().WithName(name);
      });
    }

    void SubscribeToCast(IIntegrationConfigBuilder integration)
    {
      integration.BeforeScenario.Add((scenario) => {

        if(!subscribeToCastActorCreation && !subscribeToCastActorAddition)
          return;

        var cast = scenario.Resolver.Resolve<ICast>(castName);
        var reporter = scenario.Resolver.Resolve<IReporter>(name);

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
        var reporter = resolver.Resolve<IReporter>(name);
        reporter.Subscribe(events);
      });
    }

    void SubscribeToScenario(IIntegrationConfigBuilder integration)
    {
      integration.BeforeScenario.Add((scenario) => {
        var reporter = scenario.Resolver.Resolve<IReporter>(name);
        reporter.Subscribe(scenario);
      });

      integration.AfterScenario.Add((scenario) => {
        var reporter = scenario.Resolver.Resolve<IReporter>(name);
        reporter.Unsubscribe(scenario);
      });
    }

    void WriteReport(IIntegrationConfigBuilder integration)
    {
      integration.AfterLastScenario.Add((resolver) => {

        var reporter = resolver.Resolve<IReporter>(name);
        var modelReporter = reporter as IModelBuildingReporter;
        var formatter = resolver.Resolve<IObjectFormattingService>(name);

        if(modelReporter != null && writeReportCallback != null)
        {
          var report = modelReporter.GetReport();
          writeReportCallback(formatter, report);
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

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.ReportingIntegrationBuilder"/> class.
    /// </summary>
    public ReportingIntegrationBuilder()
    {
      formatterRegistry = new ObjectFormatterRegistry();
    }

    #endregion
  }
}
