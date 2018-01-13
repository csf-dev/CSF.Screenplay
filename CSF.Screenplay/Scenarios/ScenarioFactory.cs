using System;
using System.Collections.Generic;
using System.Linq;
using CSF.MicroDi;
using CSF.MicroDi.Builders;

namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Concrete implementation of <see cref="IScenarioFactory"/>.
  /// </summary>
  public class ScenarioFactory : IScenarioFactory
  {
    readonly IContainer rootContainer;
    readonly IEnumerable<Action<IRegistrationHelper>> perScenarioRegistrations;

    /// <summary>
    /// Gets the scenario.
    /// </summary>
    /// <returns>The scenario.</returns>
    /// <param name="featureId">Feature identifier.</param>
    /// <param name="scenarioId">Scenario identifier.</param>
    public IScreenplayScenario GetScenario(IdAndName featureId, IdAndName scenarioId)
    {
      var scenarioContainer = CreateScenarioContainer();
      return new ScreenplayScenario(featureId, scenarioId, scenarioContainer);
    }

    IContainer CreateScenarioContainer()
    {
      var output = rootContainer.CreateChildContainer();
      output.AddRegistrations(perScenarioRegistrations);
      return output;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Scenarios.ScenarioFactory"/> class.
    /// </summary>
    public ScenarioFactory(IContainer rootContainer,
                           IEnumerable<Action<IRegistrationHelper>> perScenarioRegistrations)
    {
      if(rootContainer == null)
        throw new ArgumentNullException(nameof(rootContainer));
      
      this.perScenarioRegistrations = perScenarioRegistrations ?? Enumerable.Empty<Action<IRegistrationHelper>>();
      this.rootContainer = rootContainer;
    }
  }
}
