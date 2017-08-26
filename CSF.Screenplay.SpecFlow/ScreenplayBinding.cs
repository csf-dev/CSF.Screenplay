using System;
using TechTalk.SpecFlow;
using BoDi;
using CSF.Screenplay.Scenarios;
using System.Collections.Generic;
using System.Collections.Concurrent;
using CSF.Screenplay.Integration;

namespace CSF.Screenplay.SpecFlow
{
  /// <summary>
  /// Binding type for the SpecFlow/Screenplay integration.
  /// </summary>
  [Binding]
  public class ScreenplayBinding
  {
    static IDictionary<ScenarioAndFeatureKey, Guid> scenarioIds;
    static IDictionary<FeatureContext, Guid> featureIds;
    static IScreenplayConfiguration cachedConfiguration;
    static IScreenplayIntegration cachedIntegration;

    readonly IObjectContainer container;

    /// <summary>
    /// Executed before each scenario.
    /// </summary>
    [Before]
    public void BeforeScenario()
    {
      var scenarioContext = container.Resolve<ScenarioContext>();
      var featureContext = container.Resolve<FeatureContext>();

      var scenario = CreateScenario(scenarioContext, featureContext);
      container.RegisterInstanceAs(scenario);

      GetIntegration().BeforeScenario(scenario);
    }

    /// <summary>
    /// Executed after each scenario.
    /// </summary>
    [After]
    public void AfterScenario()
    {
      var scenario = GetScenario();
      var success = GetScenarioSuccess(container.Resolve<ScenarioContext>());
      GetIntegration().AfterScenario(scenario, success);
    }

    /// <summary>
    /// Executed before a test run.
    /// </summary>
    [BeforeTestRun]
    public static void BeforeTestRun()
    {
      GetIntegration().BeforeExecutingFirstScenario();
    }

    /// <summary>
    /// Executed after a test run.
    /// </summary>
    [AfterTestRun]
    public static void AfterTestRun()
    {
      GetIntegration().AfterExecutedLastScenario();
    }

    IScreenplayScenario GetScenario() => container.Resolve<IScreenplayScenario>();

    IScreenplayScenario CreateScenario(ScenarioContext scenarioContext, FeatureContext featureContext)
    {
      var featureName = GetFeatureName(featureContext);
      var scenarioName = GetScenarioName(scenarioContext, featureContext);
      var factory = GetScenarioFactory();
      return factory.GetScenario(featureName, scenarioName);
    }

    IScenarioFactory GetScenarioFactory() => GetIntegration().GetScenarioFactory();

    IdAndName GetFeatureName(FeatureContext ctx)
      => new IdAndName(GetFeatureId(ctx).ToString(), ctx.FeatureInfo.Title);

    IdAndName GetScenarioName(ScenarioContext sCtx, FeatureContext fCtx)
      => new IdAndName(GetScenarioId(sCtx, fCtx).ToString(), sCtx.ScenarioInfo.Title);

    Guid GetFeatureId(FeatureContext feature)
    {
      if(!featureIds.ContainsKey(feature))
        featureIds.Add(feature, Guid.NewGuid());

      return featureIds[feature];
    }

    Guid GetScenarioId(ScenarioContext scenario, FeatureContext feature)
    {
      var key = new ScenarioAndFeatureKey(scenario, feature);

      if(!scenarioIds.ContainsKey(key))
        scenarioIds.Add(key, Guid.NewGuid());

      return scenarioIds[key];
    }

    bool GetScenarioSuccess(ScenarioContext scenario)
      => scenario.TestError == null;

    static IScreenplayIntegration GetIntegration()
    {
      if(cachedIntegration == null)
      {
        cachedIntegration = CreateIntegration();
      }

      return cachedIntegration;
    }

    static IScreenplayConfiguration ScreenplayAppConfiguration
    {
      get {
        if(cachedConfiguration == null)
        {
          var reader = new Configuration.ConfigurationReader();
          cachedConfiguration = reader.ReadSection<SpecFlowScreenplayConfiguration>();
        }

        return cachedConfiguration;
      }
    }

    static IScreenplayIntegration CreateIntegration()
    {
      if(ScreenplayAppConfiguration == null)
        throw new InvalidOperationException("The SpecFlow/Screenplay configuration must be provided."); 
      
      var integrationConfigType = ScreenplayAppConfiguration.GetIntegrationConfigType();
      return new IntegrationFactory().Create(integrationConfigType);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.SpecFlow.ScreenplayBinding"/> class.
    /// </summary>
    /// <param name="container">Container.</param>
    public ScreenplayBinding(IObjectContainer container)
    {
      this.container = container;
    }

    /// <summary>
    /// Initializes the <see cref="T:CSF.Screenplay.SpecFlow.ScreenplayBinding"/> class.
    /// </summary>
    static ScreenplayBinding()
    {
      scenarioIds = new ConcurrentDictionary<ScenarioAndFeatureKey, Guid>();
      featureIds = new ConcurrentDictionary<FeatureContext, Guid>();
    }
  }
}
