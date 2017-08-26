using System;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay
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
