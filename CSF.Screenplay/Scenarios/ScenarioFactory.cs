using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Scenarios
{
  public class ScenarioFactory : IScenarioFactory
  {
    readonly ServiceRegistry registry;

    public ScreenplayScenario GetScenario(IdAndName featureId, IdAndName scenarioId)
    {
      var resolver = registry.GetResolver();
      return new ScreenplayScenario(featureId, scenarioId, resolver);
    }

    public ScenarioFactory(ServiceRegistry registry)
    {
      if(registry == null)
        throw new ArgumentNullException(nameof(registry));

      this.registry = registry;
    }
  }
}
