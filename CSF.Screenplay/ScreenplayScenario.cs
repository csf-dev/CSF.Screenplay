using System;
using System.Collections.Generic;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay
{
  /// <summary>
  /// Represents a single scenario within Screenplay-based test.
  /// </summary>
  public class ScreenplayScenario : ServiceResolver, IScreenplayScenario, IEquatable<ScreenplayScenario>
  {
    readonly Guid identity;

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
    /// Determines whether the specified <see cref="CSF.Screenplay.ScreenplayScenario"/> is equal to the current <see cref="T:CSF.Screenplay.ScreenplayScenario"/>.
    /// </summary>
    /// <param name="other">The <see cref="CSF.Screenplay.ScreenplayScenario"/> to compare with the current <see cref="T:CSF.Screenplay.ScreenplayScenario"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="CSF.Screenplay.ScreenplayScenario"/> is equal to the current
    /// <see cref="T:CSF.Screenplay.ScreenplayScenario"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(ScreenplayScenario other)
    {
      if(ReferenceEquals(other, null))
        return false;
      if(ReferenceEquals(other, this))
        return true;

      return identity == other.identity;
    }

    /// <summary>
    /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:CSF.Screenplay.ScreenplayScenario"/>.
    /// </summary>
    /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:CSF.Screenplay.ScreenplayScenario"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
    /// <see cref="T:CSF.Screenplay.ScreenplayScenario"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj)
    {
      return Equals(obj as ScreenplayScenario);
    }

    /// <summary>
    /// Serves as a hash function for a <see cref="T:CSF.Screenplay.ScreenplayScenario"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode()
    {
      return identity.GetHashCode();
    }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:CSF.Screenplay.ScreenplayScenario"/>.
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:CSF.Screenplay.ScreenplayScenario"/>.</returns>
    public override string ToString() => $"[Screenplay scenario:{identity.ToString()}]";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.ScreenplayScenario"/> class.
    /// </summary>
    /// <param name="featureId">Feature identifier.</param>
    /// <param name="scenarioId">Scenario identifier.</param>
    /// <param name="registrations">Service registrations.</param>
    public ScreenplayScenario(IdAndName featureId,
                              IdAndName scenarioId,
                              IReadOnlyCollection<IServiceRegistration> registrations) : base(registrations)
    {
      if(featureId == null)
        throw new ArgumentNullException(nameof(featureId));
      if(scenarioId == null)
        throw new ArgumentNullException(nameof(scenarioId));

      identity = Guid.NewGuid();
      FeatureId = featureId;
      ScenarioId = scenarioId;

      Services.Add(new ServiceMetadata(typeof(IScenarioName), null, ServiceLifetime.PerScenario),
                   new Lazy<object>(() => this));
    }
  }
}
