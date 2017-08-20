using System;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay
{
  /// <summary>
  /// Represents a single scenario within Screenplay-based test.
  /// </summary>
  public class ScreenplayScenario : IScreenplayScenario
  {
    readonly ServiceResolver resolver;

    /// <summary>
    /// Gets identifying information about the current feature under test.
    /// </summary>
    /// <value>The feature identifier.</value>
    public IdAndName FeatureId { get; private set; }

    /// <summary>
    /// Gets identifying information about the current scenario which is being tested.
    /// </summary>
    /// <value>The scenario identifier.</value>
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

    /// <summary>
    /// Notifies subscribers that the scenario has begun.
    /// </summary>
    public void Begin()
    {
      OnBeginScenario();
    }

    /// <summary>
    /// Notifies subscribers that the scenario has ended.
    /// </summary>
    /// <param name="success">If set to <c>true</c> then the scenario is a success.</param>
    public void End(bool success)
    {
      OnEndScenario(success);
    }

    /// <summary>
    /// Occurs when the scenario begins.
    /// </summary>
    public event EventHandler<BeginScenarioEventArgs> BeginScenario;

    /// <summary>
    /// Occurs when the scenario ends.
    /// </summary>
    public event EventHandler<EndScenarioEventArgs> EndScenario;

    /// <summary>
    /// Event invoker for <see cref="BeginScenario"/>.
    /// </summary>
    protected virtual void OnBeginScenario()
    {
      var args = new BeginScenarioEventArgs {
        FeatureId = FeatureId,
        ScenarioId = ScenarioId,
      };
      BeginScenario?.Invoke(this, args);
    }

    /// <summary>
    /// Event invoker for <see cref="EndScenario"/>.
    /// </summary>
    protected virtual void OnEndScenario(bool success)
    {
      var args = new EndScenarioEventArgs {
        FeatureId = FeatureId,
        ScenarioId = ScenarioId,
        ScenarioIsSuccess = success,
      };
      EndScenario?.Invoke(this, args);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.ScreenplayScenario"/> class.
    /// </summary>
    /// <param name="featureId">Feature identifier.</param>
    /// <param name="scenarioId">Scenario identifier.</param>
    /// <param name="resolver">Resolver.</param>
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
