using System;
namespace CSF.Screenplay.Reporting.Trace
{
  /// <summary>
  /// Reports the beginning of a new scenario.
  /// </summary>
  public class BeginScenarioReport
  {
    /// <summary>
    /// Gets the scenario identifier.
    /// </summary>
    /// <value>The scenario identifier.</value>
    public string ScenarioId { get; private set; }

    /// <summary>
    /// Gets the name of the scenario.
    /// </summary>
    /// <value>The name of the scenario.</value>
    public string ScenarioName { get; private set; }

    /// <summary>
    /// Gets the feature identifier.
    /// </summary>
    /// <value>The feature identifier.</value>
    public string FeatureId { get; private set; }

    /// <summary>
    /// Gets the name of the feature.
    /// </summary>
    /// <value>The name of the feature.</value>
    public string FeatureName { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Trace.BeginScenarioReport"/> class.
    /// </summary>
    /// <param name="scenarioId">Scenario identifier.</param>
    /// <param name="scenarioName">Scenario name.</param>
    /// <param name="featureId">Feature identifier.</param>
    /// <param name="featureName">Feature name.</param>
    public BeginScenarioReport(string scenarioId,
                               string scenarioName = null,
                               string featureId = null,
                               string featureName = null)
    {
      if(scenarioId == null)
        throw new ArgumentNullException(nameof(scenarioId));

      ScenarioId = scenarioId;
      ScenarioName = scenarioName;
      FeatureId = featureId;
      FeatureName = featureName;
    }
  }
}
