using System;
namespace CSF.Screenplay.Scenarios
{
  public interface IScreenplayScenario : IServiceResolver
  {
    IdAndName FeatureId { get; }

    IdAndName ScenarioId { get; }

    event EventHandler<BeginScenarioEventArgs> BeginScenario;

    event EventHandler<EndScenarioEventArgs> EndScenario;
  }
}
