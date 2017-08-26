using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using CSF.Screenplay.Integration;
using CSF.Screenplay.Scenarios;
using TechTalk.SpecFlow;

namespace CSF.Screenplay.SpecFlow
{
  /// <summary>
  /// Helper type which encapsulates the mappings/relationships between a Screenplay scenario and SpecFlow
  /// contexts.
  /// </summary>
  public class ScenarioAdapter
  {
    static IDictionary<ScenarioAndFeatureKey, Guid> scenarioIds;
    static IDictionary<FeatureContext, Guid> featureIds;

    readonly ScenarioContext scenarioContext;
    readonly FeatureContext featureContext;
    readonly IScreenplayIntegration integration;

    /// <summary>
    /// Creates a Screenplay scenario.
    /// </summary>
    /// <returns>The scenario.</returns>
    public IScreenplayScenario CreateScenario()
    {
      var factory = integration.GetScenarioFactory();
      return factory.GetScenario(FeatureIdAndName, ScenarioIdAndName);
    }

    IdAndName FeatureIdAndName
      => new IdAndName(GetFeatureId(featureContext).ToString(), featureContext.FeatureInfo.Title);

    IdAndName ScenarioIdAndName
      => new IdAndName(GetScenarioId(scenarioContext, featureContext).ToString(), scenarioContext.ScenarioInfo.Title);

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

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.SpecFlow.ScenarioAdapter"/> class.
    /// </summary>
    /// <param name="scenarioContext">Scenario context.</param>
    /// <param name="featureContext">Feature context.</param>
    /// <param name="integration">Integration.</param>
    public ScenarioAdapter(ScenarioContext scenarioContext,
                           FeatureContext featureContext,
                           IScreenplayIntegration integration)
    {
      if(integration == null)
        throw new ArgumentNullException(nameof(integration));
      if(featureContext == null)
        throw new ArgumentNullException(nameof(featureContext));
      if(scenarioContext == null)
        throw new ArgumentNullException(nameof(scenarioContext));

      this.integration = integration;
      this.featureContext = featureContext;
      this.scenarioContext = scenarioContext;
    }

    /// <summary>
    /// Initializes the <see cref="T:CSF.Screenplay.SpecFlow.ScenarioAdapter"/> class.
    /// </summary>
    static ScenarioAdapter()
    {
      scenarioIds = new ConcurrentDictionary<ScenarioAndFeatureKey, Guid>();
      featureIds = new ConcurrentDictionary<FeatureContext, Guid>();
    }

    /// <summary>
    /// Gets a value which indicates whether or not a given scenario was a success or not.
    /// </summary>
    /// <returns><c>true</c>, if the scenario was a success, <c>false</c> otherwise.</returns>
    /// <param name="scenario">Scenario.</param>
    public static bool GetScenarioSuccess(ScenarioContext scenario) => scenario.TestError == null;
  }
}
