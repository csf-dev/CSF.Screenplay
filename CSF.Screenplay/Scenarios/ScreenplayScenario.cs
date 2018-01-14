using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Represents a single scenario within Screenplay-based test.
  /// </summary>
  public class ScreenplayScenario : IScreenplayScenario, IEquatable<ScreenplayScenario>, ICanBeginAndEndScenario
  {
    #region fields

    readonly Guid identity;
    readonly MicroDi.IContainer container;

    #endregion

    #region IScreenplayScenario implementation

    /// <summary>
    /// Gets a value which indicates whether or not the scenario was a success.
    /// </summary>
    /// <value>The success.</value>
    public bool? Success { get; set; }

    /// <summary>
    /// Gets a MicroDi dependency resolver.
    /// </summary>
    /// <value>The resolver.</value>
    public MicroDi.IResolvesServices Resolver => container;

    /// <summary>
    /// Gets a unique identity for the the current scenario instance.
    /// </summary>
    /// <value>The scenario identity.</value>
    public Guid Identity => identity;

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
    /// Occurs when the scenario begins.
    /// </summary>
    public event EventHandler<BeginScenarioEventArgs> BeginScenario;

    /// <summary>
    /// Occurs when the scenario ends.
    /// </summary>
    public event EventHandler<EndScenarioEventArgs> EndScenario;

    /// <summary>
    /// Creates a new actor with the given name.
    /// </summary>
    /// <returns>The actor.</returns>
    /// <param name="name">Name.</param>
    public virtual IActor CreateActor(string name) => new Actor(name, Identity);

    #endregion

    #region ICanBeginAndEndScenario implementation

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
    /// <param name="outcome">If set to <c>true</c> then the scenario is a success.</param>
    public void End(bool? outcome)
    {
      OnEndScenario(outcome);
    }

    #endregion

    #region public API

    /// <summary>
    /// Determines whether the specified <see cref="ScreenplayScenario"/> is equal to the current <see cref="T:CSF.Screenplay.ScreenplayScenario"/>.
    /// </summary>
    /// <param name="other">The <see cref="ScreenplayScenario"/> to compare with the current <see cref="T:CSF.Screenplay.ScreenplayScenario"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="ScreenplayScenario"/> is equal to the current
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

    #endregion

    #region event invokers

    /// <summary>
    /// Event invoker for <see cref="BeginScenario"/>.
    /// </summary>
    protected virtual void OnBeginScenario()
    {
      var args = new BeginScenarioEventArgs {
        FeatureId = FeatureId,
        ScenarioId = ScenarioId,
        ScenarioIdentity = Identity,
      };
      BeginScenario?.Invoke(this, args);
    }

    /// <summary>
    /// Event invoker for <see cref="EndScenario"/>.
    /// </summary>
    protected virtual void OnEndScenario(bool? outcome)
    {
      var args = new EndScenarioEventArgs {
        FeatureId = FeatureId,
        ScenarioId = ScenarioId,
        ScenarioOutcome = outcome,
        ScenarioIdentity = Identity,
      };
      EndScenario?.Invoke(this, args);
    }

    #endregion

    #region IDisposable Support

    bool disposedValue;

    /// <summary>
    /// Disposes the current instance.
    /// </summary>
    /// <param name="disposing">If set to <c>true</c> disposing.</param>
    protected virtual void Dispose(bool disposing)
    {
      if(!disposedValue)
      {
        if(disposing)
        {
          container.Dispose();
        }

        disposedValue = true;
      }
    }

    /// <summary>
    /// Releases all resource used by the <see cref="T:CSF.Screenplay.Scenarios.ScreenplayScenario"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose()"/> when you are finished using the
    /// <see cref="T:CSF.Screenplay.Scenarios.ScreenplayScenario"/>. The <see cref="Dispose()"/> method leaves the
    /// <see cref="T:CSF.Screenplay.Scenarios.ScreenplayScenario"/> in an unusable state. After calling
    /// <see cref="Dispose()"/>, you must release all references to the
    /// <see cref="T:CSF.Screenplay.Scenarios.ScreenplayScenario"/> so the garbage collector can reclaim the memory that
    /// the <see cref="T:CSF.Screenplay.Scenarios.ScreenplayScenario"/> was occupying.</remarks>
    public void Dispose()
    {
      Dispose(true);
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.ScreenplayScenario"/> class.
    /// </summary>
    /// <param name="featureId">Feature identifier.</param>
    /// <param name="scenarioId">Scenario identifier.</param>
    /// <param name="container">A MicroDi container instance.</param>
    public ScreenplayScenario(IdAndName featureId,
                              IdAndName scenarioId,
                              MicroDi.IContainer container)
    {
      if(container == null)
        throw new ArgumentNullException(nameof(container));
      if(featureId == null)
        throw new ArgumentNullException(nameof(featureId));
      if(scenarioId == null)
        throw new ArgumentNullException(nameof(scenarioId));

      identity = Guid.NewGuid();

      FeatureId = featureId;
      ScenarioId = scenarioId;
      this.container = container;

      this.container.AddRegistrations(h => {
        h.RegisterInstance(this).As<IScenarioName>();
      });
    }

    #endregion
  }
}
