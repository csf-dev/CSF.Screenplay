using System;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay
{
  public class ScreenplayScenario : IScreenplayScenario
  {
    readonly ServiceResolver resolver;

    public IdAndName FeatureId { get; private set; }

    public IdAndName ScenarioId { get; private set; }

    TService IServiceResolver.GetService<TService>(string name)
    {
      return resolver.GetService<TService>(name);
    }

    TService IServiceResolver.GetOptionalService<TService>(string name)
    {
      return resolver.GetOptionalService<TService>(name);
    }

    object IServiceResolver.GetService(ServiceMetadata metadata)
    {
      return resolver.GetService(metadata);
    }

    object IServiceResolver.GetOptionalService(ServiceMetadata metadata)
    {
      return resolver.GetOptionalService(metadata);
    }

    public void Begin()
    {
      OnBeginScenario();
    }

    public void End(bool success)
    {
      OnEndScenario(success);
    }

    public event EventHandler<BeginScenarioEventArgs> BeginScenario;

    public event EventHandler<EndScenarioEventArgs> EndScenario;

    protected virtual void OnBeginScenario()
    {
      var args = new BeginScenarioEventArgs {
        FeatureId = FeatureId,
        ScenarioId = ScenarioId,
      };
      BeginScenario?.Invoke(this, args);
    }

    protected virtual void OnEndScenario(bool success)
    {
      var args = new EndScenarioEventArgs {
        FeatureId = FeatureId,
        ScenarioId = ScenarioId,
        ScenarioIsSuccess = success,
      };
      EndScenario?.Invoke(this, args);
    }

    public ScreenplayScenario(IdAndName featureId,
                              IdAndName scenarioId,
                              ServiceResolver resolver)
    {
      if(featureId == null)
        throw new ArgumentNullException(nameof(featureId));
      if(scenarioId == null)
        throw new ArgumentNullException(nameof(scenarioId));
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      this.resolver = resolver;

      FeatureId = featureId;
      ScenarioId = scenarioId;
    }
  }
}
