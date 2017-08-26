using System;
namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Represents a single scenario within Screenplay-based test.
  /// </summary>
  public interface IScreenplayScenario : IServiceResolver,IScenarioName
  {
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
