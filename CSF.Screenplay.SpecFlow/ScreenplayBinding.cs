using System;
using TechTalk.SpecFlow;
using BoDi;
using CSF.Screenplay.Scenarios;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CSF.Screenplay.SpecFlow
{
  public class ScreenplayBinding
  {
    static IDictionary<ScenarioAndFeatureKey, Guid> scenarioIds;
    static IDictionary<FeatureContext, Guid> featureIds;

    readonly IObjectContainer container;

    protected IObjectContainer Container => container;

    [Before]
    public virtual void BeforeScenario()
    {
      var scenario = CreateScenario(ScenarioContext.Current, FeatureContext.Current);
      CustomiseScenario(scenario);
      Container.RegisterInstanceAs(scenario);
      scenario.Begin();
    }

    [After]
    public virtual void AfterScenario()
    {
      var scenario = GetScenario();

      var success = GetScenarioSuccess(ScenarioContext.Current);
      scenario.End(success);
    }

    protected virtual void CustomiseScenario(ScreenplayScenario scenario)
    {
      // Intentional no-op, subclasses may override this to customise the scenario
    }

    ScreenplayScenario GetScenario() => Container.Resolve<ScreenplayScenario>();

    ScreenplayScenario CreateScenario(ScenarioContext scenarioContext, FeatureContext featureContext)
    {
      var featureName = GetFeatureName(featureContext);
      var scenarioName = GetScenarioName(scenarioContext, featureContext);
      var factory = GetScenarioFactory();
      return factory.GetScenario(featureName, scenarioName);
    }

    IScenarioFactory GetScenarioFactory() => ScreenplayEnvironment.Default.GetScenarioFactory();

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

    static ScreenplayEnvironment GetEnvironment() => ScreenplayEnvironment.Default;

    public ScreenplayBinding(IObjectContainer container)
    {
      this.container = container;
    }

    static ScreenplayBinding()
    {
      scenarioIds = new ConcurrentDictionary<ScenarioAndFeatureKey, Guid>();
      featureIds = new ConcurrentDictionary<FeatureContext, Guid>();
    }
  }
}
