using System;
using System.Collections.Generic;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Integration
{
  /// <summary>
  /// Default implementation of the <see cref="IIntegrationConfigBuilder"/>.
  /// </summary>
  public class IntegrationConfigurationBuilder : IIntegrationConfigBuilder
  {
    /// <summary>
    /// Gets a collection of callbacks which are used to register Screenplay-related services.
    /// </summary>
    /// <value>The service registrations.</value>
    public IList<Action<IServiceRegistryBuilder>> RegisterServices { get; private set; }

    /// <summary>
    /// Gets a collection of callbacks which are executed before the first scenario in a test run.
    /// </summary>
    /// <value>The before first scenario.</value>
    public IList<Action<IProvidesTestRunEvents,IServiceResolver>> BeforeFirstScenario { get; private set; }

    /// <summary>
    /// Gets a collection of callbacks which are executed after the last scenario in a test run.
    /// </summary>
    /// <value>The after last scenario.</value>
    public IList<Action<IServiceResolver>> AfterLastScenario { get; private set; }

    /// <summary>
    /// Gets a collection of callbacks which are executed before each scenario.
    /// </summary>
    /// <value>The before scenario.</value>
    public IList<Action<IScreenplayScenario>> BeforeScenario { get; private set; }

    /// <summary>
    /// Gets a collection of callbacks which are executed after each scenario.
    /// </summary>
    /// <value>The after scenario.</value>
    public IList<Action<IScreenplayScenario>> AfterScenario { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IntegrationConfigurationBuilder"/> class.
    /// </summary>
    public IntegrationConfigurationBuilder()
    {
      RegisterServices = new List<Action<IServiceRegistryBuilder>>();
      BeforeFirstScenario = new List<Action<IProvidesTestRunEvents,IServiceResolver>>();
      AfterLastScenario = new List<Action<IServiceResolver>>();
      BeforeScenario = new List<Action<IScreenplayScenario>>();
      AfterScenario = new List<Action<IScreenplayScenario>>();
    }
  }
}
