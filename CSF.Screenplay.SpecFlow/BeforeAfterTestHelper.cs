using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace CSF.Screenplay.SpecFlow
{
  class BeforeAfterTestHelper
  {
    static IDictionary<ScenarioAndFeatureKey,Guid> scenarioIds;
    static IDictionary<FeatureContext,Guid> featureIds;

    internal void BeforeScenario(ScreenplayContext context, ScenarioContext scenario, FeatureContext feature)
    {
      BeginScenario(context, scenario, feature);
      DismissCast(context);
    }

    internal void AfterScenario(ScreenplayContext context, ScenarioContext scenario, FeatureContext feature)
    {
      EndScenario(context, scenario, feature);
    }

    void BeginScenario(ScreenplayContext context, ScenarioContext scenario, FeatureContext feature)
    {
      var scenarioId = GetScenarioId(scenario, feature);
      var featureId = GetFeatureId(feature);

      context.OnBeginScenario(scenarioId.ToString(),
                              scenario.ScenarioInfo.Title,
                              featureId.ToString(),
                              feature.FeatureInfo.Title);
    }

    void EndScenario(ScreenplayContext context, ScenarioContext scenario, FeatureContext feature)
    {
      var success = scenario.TestError == null;
      context.OnEndScenario(success);
    }

    void DismissCast(ScreenplayContext context)
    {
      var cast = context.GetCast();
      if(cast == null)
        return;

      cast.Dismiss();
    }

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

    static BeforeAfterTestHelper()
    {
      scenarioIds = new ConcurrentDictionary<ScenarioAndFeatureKey,Guid>();
      featureIds = new ConcurrentDictionary<FeatureContext,Guid>();
    }
  }
}
