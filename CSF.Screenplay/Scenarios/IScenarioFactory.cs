using System;
namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Factory service which creates instances of <see cref="IScreenplayScenario"/>.
  /// </summary>
  public interface IScenarioFactory
  {
    /// <summary>
    /// Gets the scenario.
    /// </summary>
    /// <returns>The scenario.</returns>
    /// <param name="featureId">Feature identifier.</param>
    /// <param name="scenarioId">Scenario identifier.</param>
    IScreenplayScenario GetScenario(IdAndName featureId, IdAndName scenarioId);
  }
}
