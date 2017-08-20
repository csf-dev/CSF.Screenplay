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
    readonly ServiceRegistry registry;

    /// <summary>
    /// Gets the scenario.
    /// </summary>
    /// <returns>The scenario.</returns>
    /// <param name="featureId">Feature identifier.</param>
    /// <param name="scenarioId">Scenario identifier.</param>
    public ScreenplayScenario GetScenario(IdAndName featureId, IdAndName scenarioId)
    {
      var resolver = registry.GetResolver();
      return new ScreenplayScenario(featureId, scenarioId, resolver);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Scenarios.ScenarioFactory"/> class.
    /// </summary>
    /// <param name="registry">Registry.</param>
    public ScenarioFactory(ServiceRegistry registry)
    {
      if(registry == null)
        throw new ArgumentNullException(nameof(registry));

      this.registry = registry;
    }
  }
}
