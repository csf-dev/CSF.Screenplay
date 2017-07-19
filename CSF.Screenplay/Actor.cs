using System;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay
{
  /// <summary>
  /// The main implementation of <see cref="IActor"/>. which represents an actor/participant/persona within a test
  /// scenario.
  /// </summary>
  /// <remarks>
  /// <para>
  /// Actors may be given abilities, which provide the actions which they can take.
  /// In order to have them use those abilities to perform actions, you should use the static members of
  /// <see cref="StepComposer"/>.  It is recommended to use these via a <c>using static CSF.Screenplay.StepComposer;</c>
  /// statement.  This permits usages such as:
  /// </para>
  /// <code>
  /// Given(joe).WasAbleTo(takeOutTheTrash);
  /// </code>
  /// </remarks>
  public class Actor : IActor
  {
    #region fields

    readonly IAbilityStore abilityStore;
    readonly string name;

    #endregion

    #region properties

    /// <summary>
    /// Gets the name of the current actor.
    /// </summary>
    /// <value>The name.</value>
    public virtual string Name => name;

    #endregion

    #region methods

    /// <summary>
    /// Adds an ability of the indicated type to the current instance.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This overload requires that the ability type has a public parameterless constructor.
    /// An instance of the ability type is created/instantiated as part of this operation.
    /// </para>
    /// </remarks>
    /// <typeparam name="TAbility">The desired ability type.</typeparam>
    public virtual void IsAbleTo<TAbility>() where TAbility : IAbility,new()
    {
      var ability = abilityStore.Add(typeof(TAbility));
      InvokeGainedAbility(ability);
    }

    /// <summary>
    /// Adds an ability of the indicated type to the current instance.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This overload requires that the ability type has a public parameterless constructor.
    /// An instance of the ability type is created/instantiated as part of this operation.
    /// </para>
    /// </remarks>
    /// <param name="abilityType">The desired ability type.</param>
    public virtual void IsAbleTo(Type abilityType)
    {
      var ability = abilityStore.Add(abilityType);
      InvokeGainedAbility(ability);
    }

    /// <summary>
    /// Adds an ability object to the current instance.
    /// </summary>
    /// <param name="ability">The ability.</param>
    public virtual void IsAbleTo(IAbility ability)
    {
      abilityStore.Add(ability);
      InvokeGainedAbility(ability);
    }

    /// <summary>
    /// Performs an action or task.
    /// </summary>
    /// <param name="performable">The performable item to execute.</param>
    protected virtual void Perform(IPerformable performable)
    {
      if(ReferenceEquals(performable, null))
        throw new ArgumentNullException(nameof(performable));

      try
      {
        InvokeBeginPerformance(performable);
        performable.PerformAs(this);
        InvokeEndPerformance(performable);
      }
      catch(Exception ex)
      {
        InvokePerformanceFailed(performable, ex);
        throw;
      }
    }

    /// <summary>
    /// Performs an action, task or asks a question which returns a result value.
    /// </summary>
    /// <returns>The result of performing the item</returns>
    /// <param name="performable">The performable item to execute.</param>
    /// <typeparam name="TResult">The result type.</typeparam>
    protected virtual TResult Perform<TResult>(IPerformable<TResult> performable)
    {
      if(ReferenceEquals(performable, null))
        throw new ArgumentNullException(nameof(performable));

      TResult result;

      try
      {
        InvokeBeginPerformance(performable);
        result = performable.PerformAs(this);
        InvokePerformanceResult(performable, result);
        InvokeEndPerformance(performable);
      }
      catch(Exception ex)
      {
        InvokePerformanceFailed(performable, ex);
        throw;
      }

      return result;
    }

    /// <summary>
    /// Determines whether or not the given performer has an ability or not.
    /// </summary>
    /// <returns><c>true</c>, if the performer has the ability, <c>false</c> otherwise.</returns>
    /// <typeparam name="TAbility">The desired ability type.</typeparam>
    protected virtual bool HasAbility<TAbility>() where TAbility : IAbility
    {
      return abilityStore.HasAbility<TAbility>();
    }

    /// <summary>
    /// Gets an ability of the noted type.
    /// </summary>
    /// <returns>The ability.</returns>
    /// <typeparam name="TAbility">The desired ability type.</typeparam>
    protected virtual TAbility GetAbility<TAbility>() where TAbility : IAbility
    {
      var ability = abilityStore.GetAbility<TAbility>();

      if(ReferenceEquals(ability, null))
        throw new MissingAbilityException($"{Name} does not have the ability {typeof(TAbility).Name}.");

      return ability;
    }


    #endregion

    #region events and invokers

    /// <summary>
    /// Occurs when the actor begins a performance.
    /// </summary>
    public event EventHandler<BeginPerformanceEventArgs> BeginPerformance;

    /// <summary>
    /// Occurs when an actor ends a performance.
    /// </summary>
    public event EventHandler<EndSuccessfulPerformanceEventArgs> EndPerformance;

    /// <summary>
    /// Occurs when an actor receives a result from a performance.
    /// </summary>
    public event EventHandler<PerformanceResultEventArgs> PerformanceResult;

    /// <summary>
    /// Occurs when a performance fails with an exception.
    /// </summary>
    public event EventHandler<PerformanceFailureEventArgs> PerformanceFailed;

    /// <summary>
    /// Occurs when an actor gains a new ability.
    /// </summary>
    public event EventHandler<GainAbilityEventArgs> GainedAbility;

    /// <summary>
    /// Occurs when the actor begins a 'Given' task, action or question.
    /// </summary>
    public event EventHandler<ActorEventArgs> BeginGiven;

    /// <summary>
    /// Occurs when the actor finishes a 'Given' task, action or question.
    /// </summary>
    public event EventHandler<ActorEventArgs> EndGiven;

    /// <summary>
    /// Occurs when the actor begins a 'When' task, action or question.
    /// </summary>
    public event EventHandler<ActorEventArgs> BeginWhen;

    /// <summary>
    /// Occurs when the actor finishes a 'When' task, action or question.
    /// </summary>
    public event EventHandler<ActorEventArgs> EndWhen;

    /// <summary>
    /// Occurs when the actor begins a 'Then' task, action or question (usually a question).
    /// </summary>
    public event EventHandler<ActorEventArgs> BeginThen;

    /// <summary>
    /// Occurs when the actor finishes a 'Then' task, action or question (usually a question).
    /// </summary>
    public event EventHandler<ActorEventArgs> EndThen;

    /// <summary>
    /// Invokes the begin performance event.
    /// </summary>
    /// <param name="performable">Performable.</param>
    protected virtual void InvokeBeginPerformance(IPerformable performable)
    {
      var args = new BeginPerformanceEventArgs(this, performable);
      BeginPerformance?.Invoke(this, args);
    }

    /// <summary>
    /// Invokes the end performance event.
    /// </summary>
    /// <param name="performable">Performable.</param>
    protected virtual void InvokeEndPerformance(IPerformable performable)
    {
      var args = new EndSuccessfulPerformanceEventArgs(this, performable);
      EndPerformance?.Invoke(this, args);
    }

    /// <summary>
    /// Invokes the performance result event.
    /// </summary>
    /// <param name="performable">Performable.</param>
    /// <param name="result">Result.</param>
    protected virtual void InvokePerformanceResult(IPerformable performable, object result)
    {
      var args = new PerformanceResultEventArgs(this, performable, result);
      PerformanceResult?.Invoke(this, args);
    }

    /// <summary>
    /// Invokes the performance failed event.
    /// </summary>
    /// <param name="performable">Performable.</param>
    /// <param name="exception">Exception.</param>
    protected virtual void InvokePerformanceFailed(IPerformable performable, Exception exception)
    {
      var args = new PerformanceFailureEventArgs(this, performable, exception);
      PerformanceFailed?.Invoke(this, args);
    }

    /// <summary>
    /// Invokes the gained ability event.
    /// </summary>
    /// <param name="ability">Ability.</param>
    protected virtual void InvokeGainedAbility(IAbility ability)
    {
      var args = new GainAbilityEventArgs(this, ability);
      GainedAbility?.Invoke(this, args);
    }

    /// <summary>
    /// Invokes the begin-given event.
    /// </summary>
    protected virtual void InvokeBeginGiven()
    {
      var args = new ActorEventArgs(this);
      BeginGiven?.Invoke(this, args);
    }

    /// <summary>
    /// Invokes the end-given event.
    /// </summary>
    protected virtual void InvokeEndGiven()
    {
      var args = new ActorEventArgs(this);
      EndGiven?.Invoke(this, args);
    }

    /// <summary>
    /// Invokes the begin-when event.
    /// </summary>
    protected virtual void InvokeBeginWhen()
    {
      var args = new ActorEventArgs(this);
      BeginWhen?.Invoke(this, args);
    }

    /// <summary>
    /// Invokes the end-when event.
    /// </summary>
    protected virtual void InvokeEndWhen()
    {
      var args = new ActorEventArgs(this);
      EndWhen?.Invoke(this, args);
    }

    /// <summary>
    /// Invokes the begin-then event.
    /// </summary>
    protected virtual void InvokeBeginThen()
    {
      var args = new ActorEventArgs(this);
      BeginThen?.Invoke(this, args);
    }

    /// <summary>
    /// Invokes the end-then event.
    /// </summary>
    protected virtual void InvokeEndThen()
    {
      var args = new ActorEventArgs(this);
      EndThen?.Invoke(this, args);
    }

    #endregion

    #region explicit IPerformer and IActor implementations

    void IGivenActor.WasAbleTo(IPerformable performable)
    {
      InvokeBeginGiven();
      Perform(performable);
      InvokeEndGiven();
    }

    void IWhenActor.AttemptsTo(IPerformable performable)
    {
      InvokeBeginWhen();
      Perform(performable);
      InvokeEndWhen();
    }

    void IThenActor.Should(IPerformable performable)
    {
      InvokeBeginThen();
      Perform(performable);
      InvokeEndThen();
    }

    TResult IGivenActor.WasAbleTo<TResult>(IPerformable<TResult> performable)
    {
      InvokeBeginGiven();
      var result = Perform(performable);
      InvokeEndGiven();
      return result;
    }

    TResult IWhenActor.AttemptsTo<TResult>(IPerformable<TResult> performable)
    {
      InvokeBeginWhen();
      var result = Perform(performable);
      InvokeEndWhen();
      return result;
    }

    TResult IThenActor.Should<TResult>(IPerformable<TResult> performable)
    {
      InvokeBeginThen();
      var result = Perform(performable);
      InvokeEndThen();
      return result;
    }

    TResult IGivenActor.Saw<TResult>(IQuestion<TResult> question)
    {
      InvokeBeginGiven();
      var result = Perform(question);
      InvokeEndGiven();
      return result;
    }

    TResult IWhenActor.Sees<TResult>(IQuestion<TResult> question)
    {
      InvokeBeginWhen();
      var result = Perform(question);
      InvokeEndWhen();
      return result;
    }

    TResult IThenActor.ShouldSee<TResult>(IQuestion<TResult> question)
    {
      InvokeBeginThen();
      var result = Perform(question);
      InvokeEndThen();
      return result;
    }

    bool IPerformer.HasAbility<TAbility>()
    {
      return HasAbility<TAbility>();
    }

    TAbility IPerformer.GetAbility<TAbility>()
    {
      return GetAbility<TAbility>();
    }

    void IPerformer.Perform(IPerformable performable)
    {
      Perform(performable);
    }

    TResult IPerformer.Perform<TResult>(IPerformable<TResult> performable)
    {
      return Perform(performable);
    }

    #endregion

    #region IDisposable implementation

    bool disposed;

    /// <summary>
    /// Gets a value indicating whether this <see cref="Actor"/> is disposed.
    /// </summary>
    /// <value><c>true</c> if disposed; otherwise, <c>false</c>.</value>
    protected bool Disposed => disposed;

    /// <summary>
    /// Performs disposal of the current instance.
    /// </summary>
    /// <param name="disposing">If set to <c>true</c> then we are explicitly disposing.</param>
    protected virtual void Dispose(bool disposing)
    {
      if(!disposed)
      {
        if(disposing)
        {
          abilityStore.Dispose();
        }

        disposed = true;
      }
    }

    void IDisposable.Dispose()
    {
      Dispose(true);
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="Actor"/> class.
    /// </summary>
    /// <param name="name">The actor's name.</param>
    public Actor(string name)
    {
      if(name == null)
        throw new ArgumentNullException(nameof(name));

      this.name = name;
      abilityStore = new AbilityStore();
    }

    #endregion
  }
}
