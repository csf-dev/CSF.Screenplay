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
    readonly IReporter reporter;

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
      reporter.GainAbility(this, ability);
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
      reporter.GainAbility(this, ability);
    }

    /// <summary>
    /// Adds an ability object to the current instance.
    /// </summary>
    /// <param name="ability">The ability.</param>
    public virtual void IsAbleTo(IAbility ability)
    {
      abilityStore.Add(ability);
      reporter.GainAbility(this, ability);
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
        reporter.Begin(this, performable);
        performable.PerformAs(this);
        reporter.Success(this, performable);
      }
      catch(Exception ex)
      {
        reporter.Failure(this, performable, ex);
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
        reporter.Begin(this, performable);
        result = performable.PerformAs(this);
        reporter.Result(this, performable, result);
        reporter.Success(this, performable);
      }
      catch(Exception ex)
      {
        reporter.Failure(this, performable, ex);
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

    IReporter GetDefaultReporter()
    {
      return new TraceReporter();
    }

    #endregion

    #region explicit IPerformer and IActor implementations

    void IGivenActor.WasAbleTo(IPerformable performable)
    {
      Perform(performable);
    }

    void IWhenActor.AttemptsTo(IPerformable performable)
    {
      Perform(performable);
    }

    void IThenActor.Should(IPerformable performable)
    {
      Perform(performable);
    }

    TResult IGivenActor.WasAbleTo<TResult>(IPerformable<TResult> performable)
    {
      return Perform(performable);
    }

    TResult IWhenActor.AttemptsTo<TResult>(IPerformable<TResult> performable)
    {
      return Perform(performable);
    }

    TResult IThenActor.Should<TResult>(IPerformable<TResult> performable)
    {
      return Perform(performable);
    }

    TResult IGivenActor.Saw<TResult>(IQuestion<TResult> question)
    {
      return Perform(question);
    }

    TResult IWhenActor.Sees<TResult>(IQuestion<TResult> question)
    {
      return Perform(question);
    }

    TResult IThenActor.ShouldSee<TResult>(IQuestion<TResult> question)
    {
      return Perform(question);
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
    public Actor(string name) : this(name, null) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="Actor"/> class.
    /// </summary>
    /// <param name="name">The actor's name.</param>
    /// <param name="reporter">A reporter instance to use.</param>
    public Actor(string name, IReporter reporter = null)
    {
      if(name == null)
        throw new ArgumentNullException(nameof(name));

      this.reporter = reporter?? GetDefaultReporter();
      this.name = name;

      abilityStore = new AbilityStore();
    }

    #endregion
  }
}
