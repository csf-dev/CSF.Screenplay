using System;
namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Factory service which creates instances of <see cref="IScenario"/>.
  /// </summary>
  public interface IScenarioFactory
  {
    /// <summary>
    /// Gets the scenario.
    /// </summary>
    /// <returns>The scenario.</returns>
    /// <param name="featureId">Feature identifier.</param>
    /// <param name="scenarioId">Scenario identifier.</param>
    IScenario GetScenario(IdAndName featureId, IdAndName scenarioId);
  }
}
