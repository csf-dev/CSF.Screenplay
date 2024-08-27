using System;
using TechTalk.SpecFlow;

namespace CSF.Screenplay.SpecFlow
{
  /// <summary>
  /// Describes a Feature and Scenario combination, uniquely identifying a scenario.
  /// </summary>
  public class ScenarioAndFeatureKey : IEquatable<ScenarioAndFeatureKey>
  {
    readonly int cachedHashCode;
    readonly ScenarioContext scenario;
    readonly FeatureContext feature;

    /// <summary>
    /// Gets the scenario.
    /// </summary>
    /// <value>The scenario.</value>
    public virtual ScenarioContext Scenario => scenario;

    /// <summary>
    /// Gets the feature.
    /// </summary>
    /// <value>The feature.</value>
    public virtual FeatureContext Feature => feature;

    /// <summary>
    /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:CSF.Screenplay.SpecFlow.ScenarioAndFeatureKey"/>.
    /// </summary>
    /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:CSF.Screenplay.SpecFlow.ScenarioAndFeatureKey"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
    /// <see cref="T:CSF.Screenplay.SpecFlow.ScenarioAndFeatureKey"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj)
    {
      return Equals(obj as ScenarioAndFeatureKey);
    }

    /// <summary>
    /// Determines whether the specified <see cref="CSF.Screenplay.SpecFlow.ScenarioAndFeatureKey"/> is equal to the
    /// current <see cref="T:CSF.Screenplay.SpecFlow.ScenarioAndFeatureKey"/>.
    /// </summary>
    /// <param name="other">The <see cref="CSF.Screenplay.SpecFlow.ScenarioAndFeatureKey"/> to compare with the current <see cref="T:CSF.Screenplay.SpecFlow.ScenarioAndFeatureKey"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="CSF.Screenplay.SpecFlow.ScenarioAndFeatureKey"/> is equal to the current
    /// <see cref="T:CSF.Screenplay.SpecFlow.ScenarioAndFeatureKey"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(ScenarioAndFeatureKey other)
    {
      if(ReferenceEquals(this, other))
        return true;
      if(ReferenceEquals(null, other))
        return false;

      return ReferenceEquals(Scenario, other.Scenario) && ReferenceEquals(Feature, other.Feature);
    }

    /// <summary>
    /// Serves as a hash function for a <see cref="T:CSF.Screenplay.SpecFlow.ScenarioAndFeatureKey"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode()
    {
      return cachedHashCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.SpecFlow.ScenarioAndFeatureKey"/> class.
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    /// <param name="feature">Feature.</param>
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
