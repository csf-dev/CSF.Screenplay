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
    readonly IServiceResolver resolver;

    /// <summary>
    /// Gets the scenario.
    /// </summary>
    /// <returns>The scenario.</returns>
    /// <param name="featureId">Feature identifier.</param>
    /// <param name="scenarioId">Scenario identifier.</param>
    public ScreenplayScenario GetScenario(IdAndName featureId, IdAndName scenarioId)
    {
      return new ScreenplayScenario(featureId, scenarioId, resolver);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Scenarios.ScenarioFactory"/> class.
    /// </summary>
    /// <param name="resolver">Service resolver.</param>
    public ScenarioFactory(IServiceResolver resolver)
    {
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      this.resolver = resolver;
    }
  }
}
