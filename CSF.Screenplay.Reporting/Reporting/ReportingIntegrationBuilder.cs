using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Integration;
using CSF.Screenplay.ReportFormatting;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Builder type which permits the configuration of reporting throughout a Screenplay test run.
  /// </summary>
  public class ReportingIntegrationBuilder
  {
    #region fields

    Func<IFormatsObjectForReport,IHandlesReportableEvents> reporterToUse;
    IObservesScenarioCompletion scenarioCompletionObserver;
    string name, castName;
    bool subscribeToCastActorCreation, subscribeToCastActorAddition;
    readonly IObjectFormatingStrategyRegistry formatterRegistry;

    #endregion

    #region builder methods

    /// <summary>
    /// Indicates that a given reporter should be used throughout the test run.
    /// </summary>
    /// <param name="reporter">Reporter.</param>
    public ReportingIntegrationBuilder WithReporter(IHandlesReportableEvents reporter)
    {
      if(reporter == null)
        throw new ArgumentNullException(nameof(reporter));

      reporterToUse = formatter => reporter;
      return this;
    }

    /// <summary>
    /// Indicates that a given reporter should be used throughout the test run.
    /// </summary>
    /// <param name="reporterFactory">A factory method for a reporter.</param>
    public ReportingIntegrationBuilder WithReporter(Func<IFormatsObjectForReport,IHandlesReportableEvents> reporterFactory)
    {
      if(reporterFactory == null)
        throw new ArgumentNullException(nameof(reporterFactory));

      reporterToUse = reporterFactory;
      return this;
    }

    /// <summary>
    /// Indicates that when scenarios complete and their reports become available, the given object should render
    /// their results.
    /// </summary>
    /// <returns>The scenario renderer.</returns>
    /// <param name="renderer">Renderer.</param>
    public ReportingIntegrationBuilder WithScenarioRenderer(IObservesScenarioCompletion renderer)
    {
      scenarioCompletionObserver = renderer;
      return this;
    }

    /// <summary>
    /// Adds an object formatter to the given reporter.
    /// </summary>
    /// <returns>The formatter.</returns>
    /// <typeparam name="T">The object formatter type.</typeparam>
    public ReportingIntegrationBuilder WithFormattingStrategy<T>() where T : IHasObjectFormattingStrategyInfo,new()
    {
      return WithFormattingStrategy(new T());
    }

    /// <summary>
    /// Adds an object formatter to the given reporter.
    /// </summary>
    /// <returns>The formatter.</returns>
    /// <param name="strategy">Formatter.</param>
    public ReportingIntegrationBuilder WithFormattingStrategy(IHasObjectFormattingStrategyInfo strategy)
    {
      if(strategy == null)
        throw new ArgumentNullException(nameof(strategy));
      
      formatterRegistry.Add(strategy);
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
    }

    void RegisterObjectFormatters(IIntegrationConfigBuilder integration)
    {
      integration.ServiceRegistrations.PerTestRun.Add(h => {
        h.RegisterInstance(formatterRegistry).As<IObjectFormatingStrategyRegistry>().WithName(name);
        var objectFormatter = new StrategyBasedObjectFormatter(formatterRegistry);
        h.RegisterInstance(objectFormatter).As<IFormatsObjectForReport>().WithName(name);
      });
    }

    void RegisterReporter(IIntegrationConfigBuilder integration)
    {
      var reporter = reporterToUse ?? (formatter => new NoOpReportableEventHandler());

      integration.ServiceRegistrations.PerTestRun.Add(h => {
        h.RegisterFactory(reporter).As<IHandlesReportableEvents>().WithName(name);
        h.RegisterFactory(GetReportableEventObserverFactory()).As<IObservesReportableEvents>().WithName(name);
        h.RegisterFactory(GetScenarioCompletionProviderFactory()).As<IExposesCompletedScenarios>().WithName(name);

        if(scenarioCompletionObserver != null)
          h.RegisterInstance(scenarioCompletionObserver).As<IObservesScenarioCompletion>().WithName(name);
      });
    }

    Func<IHandlesReportableEvents, DelegatingReportableEventObserver> GetReportableEventObserverFactory()
      => handler => new DelegatingReportableEventObserver(handler);

    Func<FlexDi.IResolvesServices,IExposesCompletedScenarios> GetScenarioCompletionProviderFactory()
      => resolver => resolver.TryResolve<IHandlesReportableEvents>() as IExposesCompletedScenarios;

    void SubscribeToCast(IIntegrationConfigBuilder integration)
    {
      integration.BeforeScenario.Add((scenario) => {

        if(!subscribeToCastActorCreation && !subscribeToCastActorAddition)
          return;

        var cast = scenario.DiContainer.Resolve<ICast>(castName);
        var reporter = scenario.DiContainer.Resolve<IObservesReportableEvents>(name);

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
      integration.BeforeFirstScenario.Add(SubscribeObserverToReportEvents);
      integration.BeforeFirstScenario.Add(SubscribeRendererToCompletedScenrios);
    }

    void SubscribeObserverToReportEvents(Scenarios.IProvidesTestRunEvents events,
                                         FlexDi.IResolvesServices resolver)
    {
      var reporter = resolver.Resolve<IObservesReportableEvents>(name);
      reporter.Subscribe(events);
    }

    void SubscribeRendererToCompletedScenrios(Scenarios.IProvidesTestRunEvents events,
                                              FlexDi.IResolvesServices resolver)
    {
      var reportableScenarioProvider = resolver.TryResolve<IExposesCompletedScenarios>(name);
      var scenarioRenderer = resolver.TryResolve<IObservesScenarioCompletion>(name);

      if(reportableScenarioProvider == null || scenarioRenderer == null)
        return;

      scenarioRenderer.Subscribe(reportableScenarioProvider);
    }

    void SubscribeToScenario(IIntegrationConfigBuilder integration)
    {
      integration.BeforeScenario.Add((scenario) => {
        var reporter = scenario.DiContainer.Resolve<IObservesReportableEvents>(name);
        reporter.Subscribe(scenario);
      });

      integration.AfterScenario.Add((scenario) => {
        var reporter = scenario.DiContainer.Resolve<IObservesReportableEvents>(name);
        reporter.Unsubscribe(scenario);
      });
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.ReportingIntegrationBuilder"/> class.
    /// </summary>
    public ReportingIntegrationBuilder()
    {
      formatterRegistry = new ObjectFormattingStrategyRegistry();
      reporterToUse = formatter => new ReportBuildingReportableEventHandler(formatter);
    }

    #endregion
  }
}
