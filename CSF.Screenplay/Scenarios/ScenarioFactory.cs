using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Concrete implementation of <see cref="IScenarioFactory"/>.
  /// </summary>
  public class ScenarioFactory : IScenarioFactory
  {
    readonly IReadOnlyCollection<IServiceRegistration> registrations;

    /// <summary>
    /// Gets the scenario.
    /// </summary>
    /// <returns>The scenario.</returns>
    /// <param name="featureId">Feature identifier.</param>
    /// <param name="scenarioId">Scenario identifier.</param>
    public ScreenplayScenario GetScenario(IdAndName featureId, IdAndName scenarioId)
    {
      return new ScreenplayScenario(featureId, scenarioId, registrations);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Scenarios.ScenarioFactory"/> class.
    /// </summary>
    /// <param name="registrations">Service registrations.</param>
    public ScenarioFactory(IReadOnlyCollection<IServiceRegistration> registrations)
    {
      if(registrations == null)
        throw new ArgumentNullException(nameof(registrations));

      this.registrations = registrations;
    }
  }
}
