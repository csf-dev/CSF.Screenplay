using System;
namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Represents a single scenario within Screenplay-based test.
  /// </summary>
  public interface IScreenplayScenario : IServiceResolver
  {
    /// <summary>
    /// Gets identifying information about the current feature under test.
    /// </summary>
    /// <value>The feature identifier.</value>
    IdAndName FeatureId { get; }

    /// <summary>
    /// Gets identifying information about the current scenario which is being tested.
    /// </summary>
    /// <value>The scenario identifier.</value>
    IdAndName ScenarioId { get; }

    /// <summary>
    /// Occurs when the scenario begins.
    /// </summary>
    event EventHandler<BeginScenarioEventArgs> BeginScenario;

    /// <summary>
    /// Occurs when the scenario ends.
    /// </summary>
    event EventHandler<EndScenarioEventArgs> EndScenario;
  }
}
