using System;
using System.Collections.Generic;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Questions;
using CSF.Screenplay.Tasks;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay
{
  /// <summary>
  /// The main type which represents an actor in a test scenario.
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
  public class Actor : IGivenActor, IWhenActor, IThenActor, ICanReceiveAbilities, IDisposable
  {
    #region fields

    readonly ISet<IAbility> abilities;
    readonly string name;

    #endregion

    #region properties

    /// <summary>
    /// Gets the name of the current actor.
    /// </summary>
    /// <value>The name.</value>
    public string Name => name;

    #endregion

    #region IActor implementation

    void IGivenActor.WasAbleTo(ITask task)
    {
      Perform(task);
    }

    void IWhenActor.AttemptsTo(ITask task)
    {
      Perform(task);
    }

    void IThenActor.Should(ITask task)
    {
      Perform(task);
    }

    TResult IGivenActor.WasAbleTo<TResult>(ITask<TResult> task)
    {
      return Perform(task);
    }

    TResult IWhenActor.AttemptsTo<TResult>(ITask<TResult> task)
    {
      return Perform(task);
    }

    TResult IThenActor.Should<TResult>(ITask<TResult> task)
    {
      return Perform(task);
    }

    object IGivenActor.Saw(IQuestion question)
    {
      return GetAnswer(question);
    }

    TResult IGivenActor.Saw<TResult>(IQuestion<TResult> question)
    {
      return GetAnswer(question);
    }

    object IWhenActor.Sees(IQuestion question)
    {
      return GetAnswer(question);
    }

    TResult IWhenActor.Sees<TResult>(IQuestion<TResult> question)
    {
      return GetAnswer(question);
    }

    object IThenActor.ShouldSee(IQuestion question)
    {
      return GetAnswer(question);
    }

    TResult IThenActor.ShouldSee<TResult>(IQuestion<TResult> question)
    {
      return GetAnswer(question);
    }

    /// <summary>
    /// Performs the specified task.
    /// </summary>
    /// <param name="task">The task.</param>
    protected virtual void Perform(ITask task)
    {
      var provider = GetPerformer();
      task.PerformAs(provider);
    }

    /// <summary>
    /// Performs the specified task and gets its result.
    /// </summary>
    /// <returns>The result returned from performing the task.</returns>
    /// <param name="task">The task.</param>
    /// <typeparam name="TResult">The type of result instance expected from the task.</typeparam>
    protected virtual TResult Perform<TResult>(ITask<TResult> task)
    {
      var provider = GetPerformer();
      return task.PerformAs(provider);
    }

    /// <summary>
    /// Asks a question of the application and gets the answer.
    /// </summary>
    /// <returns>The answer.</returns>
    /// <param name="question">The question.</param>
    protected virtual object GetAnswer(IQuestion question)
    {
      var provider = GetPerformer();
      return question.GetAnswer(provider);
    }

    /// <summary>
    /// Asks a question of the application and gets the answer.
    /// </summary>
    /// <returns>The answer.</returns>
    /// <param name="question">The question.</param>
    /// <typeparam name="TResult">The type of answer/result type.</typeparam>
    protected virtual TResult GetAnswer<TResult>(IQuestion<TResult> question)
    {
      var provider = GetPerformer();
      return question.GetAnswer(provider);
    }

    #endregion

    #region ICanReceiveAbilities implementation

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
      var ability = Activator.CreateInstance<TAbility>();
      IsAbleTo(ability);
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
      if(!typeof(IAbility).IsAssignableFrom(abilityType))
        throw new ArgumentException($"Ability type must implement `{typeof(IAbility).Name}'.", nameof(abilityType));
      
      var ability = (IAbility) Activator.CreateInstance(abilityType);
      IsAbleTo(ability);
    }

    /// <summary>
    /// Adds an ability object to the current instance.
    /// </summary>
    /// <param name="ability">The ability.</param>
    public virtual void IsAbleTo(IAbility ability)
    {
      if(ability == null)
        throw new ArgumentNullException(nameof(ability));

      abilities.Add(ability);
    }

    /// <summary>
    /// Gets an <see cref="IPerformer"/> implementation from the current actor.
    /// </summary>
    /// <returns>The performer.</returns>
    protected virtual IPerformer GetPerformer()
    {
      return new Performer(abilities, Name);
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
          foreach(var ability in abilities)
          {
            ability.Dispose();
          }
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
      abilities = new HashSet<IAbility>();
    }

    #endregion
  }
}
