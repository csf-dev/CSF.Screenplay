using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay
{
  /// <summary>
  /// Represents a single scenario within Screenplay-based test.
  /// </summary>
  public interface IScreenplayScenario : IScenarioName, IDisposable
  {
    /// <summary>
    /// Gets a value which indicates whether or not the scenario was a success.
    /// </summary>
    /// <value>The success.</value>
    bool? Success { get; set; }

    /// <summary>
    /// Gets a MicroDi dependency resolver.
    /// </summary>
    /// <value>The resolver.</value>
    MicroDi.IResolvesServices Resolver { get; }

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
