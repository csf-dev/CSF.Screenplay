using System;
using System.Collections.Generic;
using CSF.FlexDi;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Integration
{
  /// <summary>
  /// Default implementation of the <see cref="IIntegrationConfigBuilder"/>.
  /// </summary>
  public class IntegrationConfigurationBuilder : IIntegrationConfigBuilder
  {
    /// <summary>
    /// Gets the callbacks which are used to register Screenplay-related services.
    /// </summary>
    /// <value>The service registrations.</value>
    public ServiceRegistrations ServiceRegistrations { get; private set; }

    /// <summary>
    /// Gets a collection of callbacks which are executed before the first scenario in a test run.
    /// </summary>
    /// <value>The before first scenario.</value>
    public IList<Action<IProvidesTestRunEvents,IResolvesServices>> BeforeFirstScenario { get; private set; }

    /// <summary>
    /// Gets a collection of callbacks which are executed after the last scenario in a test run.
    /// </summary>
    /// <value>The after last scenario.</value>
    public IList<Action<IResolvesServices>> AfterLastScenario { get; private set; }

    /// <summary>
    /// Gets a collection of callbacks which are executed before each scenario.
    /// </summary>
    /// <value>The before scenario.</value>
    public IList<Action<IScenario>> BeforeScenario { get; private set; }

    /// <summary>
    /// Gets a collection of callbacks which are executed after each scenario.
    /// </summary>
    /// <value>The after scenario.</value>
    public IList<Action<IScenario>> AfterScenario { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IntegrationConfigurationBuilder"/> class.
    /// </summary>
    public IntegrationConfigurationBuilder()
    {
      ServiceRegistrations = new ServiceRegistrations();
      BeforeFirstScenario = new List<Action<IProvidesTestRunEvents,IResolvesServices>>();
      AfterLastScenario = new List<Action<IResolvesServices>>();
      BeforeScenario = new List<Action<IScenario>>();
      AfterScenario = new List<Action<IScenario>>();
    }
  }
}
