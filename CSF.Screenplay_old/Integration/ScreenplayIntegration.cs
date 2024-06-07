using System;
using CSF.FlexDi;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Integration
{
  /// <summary>
  /// Base type for custom screenplay integrations.  This is suitable for subclassing in custom integrations.
  /// </summary>
  public class ScreenplayIntegration : IScreenplayIntegration
  {
    #region fields

    readonly IIntegrationConfigBuilder builder;
    readonly TestRunEvents testRunEvents;
    readonly Lazy<IContainer> rootContainer;

    #endregion

    #region protected properties

    /// <summary>
    /// Gets the root dependency injection container which carries .
    /// </summary>
    /// <value>The root container.</value>
    protected IContainer RootContainer => rootContainer.Value;

    #endregion

    #region public API

    /// <summary>
    /// Executed once, before the first scenario in the test run is executed.  Note that
    /// all services must have already been registered prior to executing this method.
    /// </summary>
    public void BeforeExecutingFirstScenario()
    {
      foreach(var callback in builder.BeforeFirstScenario)
        callback(testRunEvents, RootContainer);
      
      testRunEvents.NotifyBeginTestRun();
    }

    /// <summary>
    /// Executed before each scenario in a test run.
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    public void BeforeScenario(IScenario scenario)
    {
      foreach(var callback in builder.BeforeScenario)
        callback(scenario);

      if(scenario is ICanBeginAndEndScenario)
        ((ICanBeginAndEndScenario) scenario).Begin();
    }

    /// <summary>
    /// Executed after each scenario in a test run.
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    /// <param name="success">If set to <c>true</c> success.</param>
    public void AfterScenario(IScenario scenario, bool? success)
    {
      scenario.Success = success;

      if(scenario is ICanBeginAndEndScenario)
        ((ICanBeginAndEndScenario) scenario).End(success);

      foreach(var callback in builder.AfterScenario)
        callback(scenario);

      scenario.Dispose();
    }

    /// <summary>
    /// Executed after the last scenario in a test run.
    /// </summary>
    public void AfterExecutedLastScenario()
    {
      testRunEvents.NotifyCompleteTestRun();

      foreach(var callback in builder.AfterLastScenario)
        callback(RootContainer);

      RootContainer.Dispose();
    }

    /// <summary>
    /// Gets the scenario factory.
    /// </summary>
    /// <returns>The scenario factory.</returns>
    public IScenarioFactory GetScenarioFactory()
      => new ScenarioFactory(RootContainer, builder.ServiceRegistrations.PerScenario);

    #endregion

    #region private methods

    IContainer CreateRootContainer()
    {
      return Container
        .CreateBuilder()
        .DoNotMakeAllResolutionOptional()
        .DoNotResolveUnregisteredTypes()
        .SelfRegisterAResolver()
        .DoNotSelfRegisterTheRegistry()
        .SupportResolvingLazyInstances()
        .DoNotSupportResolvingNamedInstanceDictionaries()
        .ThrowOnCircularDependencies()
        .UseInstanceCache()
        .DoNotUseNonPublicConstructors()
        .Build();
    }

    void AddRootContainerRegistrations(IReceivesRegistrations container)
    {
      container.AddRegistrations(builder.ServiceRegistrations.PerTestRun);
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Integration.ScreenplayIntegration"/> class.
    /// </summary>
    /// <param name="configBuilder">Config builder.</param>
    /// <param name="rootContainer">The root DI container.</param>
    public ScreenplayIntegration(IIntegrationConfigBuilder configBuilder,
                                 IContainer rootContainer = null)
    {
      if(configBuilder == null)
        throw new ArgumentNullException(nameof(configBuilder));

      builder = configBuilder;
      this.rootContainer = new Lazy<IContainer>(() => {
        var output = rootContainer ?? CreateRootContainer();
        AddRootContainerRegistrations(output);
        return output;
      });
      testRunEvents = new TestRunEvents();
    }

    #endregion
  }
}
