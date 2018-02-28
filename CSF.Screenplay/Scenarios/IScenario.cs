using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Represents a single scenario within Screenplay-based test.
  /// </summary>
  public interface IScenario : IScenarioName, IDisposable
  {
    /// <summary>
    /// Gets a value which indicates whether or not the scenario was a success.
    /// </summary>
    /// <value>The success.</value>
    bool? Success { get; set; }

    /// <summary>
    /// Gets the FlexDi container.
    /// </summary>
    /// <value>The container.</value>
    FlexDi.IContainer DiContainer { get; }

    /// <summary>
    /// Creates a new actor with the given name.
    /// </summary>
    /// <returns>The actor.</returns>
    /// <param name="name">Name.</param>
    IActor CreateActor(string name);

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
