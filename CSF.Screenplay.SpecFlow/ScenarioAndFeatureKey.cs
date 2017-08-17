using System;
using TechTalk.SpecFlow;

namespace CSF.Screenplay.SpecFlow
{
  public class ScenarioAndFeatureKey : IEquatable<ScenarioAndFeatureKey>
  {
    readonly int cachedHashCode;
    ScenarioContext scenario;
    FeatureContext feature;

    public virtual ScenarioContext Scenario => scenario;

    public virtual FeatureContext Feature => feature;

    public override bool Equals(object obj)
    {
      return Equals(obj as ScenarioAndFeatureKey);
    }

    public bool Equals(ScenarioAndFeatureKey other)
    {
      if(ReferenceEquals(this, other))
        return true;
      if(ReferenceEquals(null, other))
        return false;

      return ReferenceEquals(Scenario, other.Scenario) && ReferenceEquals(Feature, other.Feature);
    }

    public override int GetHashCode()
    {
      return cachedHashCode;
    }

    public ScenarioAndFeatureKey(ScenarioContext scenario, FeatureContext feature)
    {
      if(scenario == null)
        throw new ArgumentNullException(nameof(scenario));
      if(feature == null)
        throw new ArgumentNullException(nameof(feature));

      this.scenario = scenario;
      this.feature = feature;

      cachedHashCode = (scenario.ScenarioInfo.Title.GetHashCode()
                        ^ feature.FeatureInfo.Title.GetHashCode());
    }
  }
}
