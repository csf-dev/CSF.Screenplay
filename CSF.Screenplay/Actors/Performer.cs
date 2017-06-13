using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actions;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Default implementation of <see cref="IPerformer"/>.
  /// </summary>
  public class Performer : IPerformer
  {
    #region fields

    readonly IEnumerable<IAbility> abilities;
    readonly string name;

    #endregion

    #region public API

    /// <summary>
    /// Gets the name of the actor.
    /// </summary>
    /// <value>The name.</value>
    public string Name => name;

    /// <summary>
    /// Performs an action, using the given parameters.
    /// </summary>
    /// <param name="action">The action instance to execute.</param>
    /// <param name="parameters">The parameters for the action.</param>
    /// <typeparam name="TParams">The action parameters type.</typeparam>
    public void Perform<TParams>(IAction<TParams> action, TParams parameters)
    {
      if(ReferenceEquals(action, null))
        throw new ArgumentNullException(nameof(action));

      action.Execute(this, parameters);
    }

    /// <summary>
    /// Performs an action, using the given parameters, getting a result value.
    /// </summary>
    /// <returns>The result of performing the action</returns>
    /// <param name="action">The action instance to execute.</param>
    /// <param name="parameters">The parameters for the action.</param>
    /// <typeparam name="TParams">The action parameters type.</typeparam>
    /// <typeparam name="TResult">The action result type.</typeparam>
    public TResult Perform<TParams,TResult>(IAction<TParams,TResult> action, TParams parameters)
    {
      if(ReferenceEquals(action, null))
        throw new ArgumentNullException(nameof(action));

      return action.Execute(this, parameters);
    }

    #endregion

    #region IPerformer implementation

    TAction IPerformer.GetAction<TAction>()
    {
      return GetAction<TAction>();
    }

    bool IPerformer.SupportsActionType<TAction>()
    {
      return SupportsActionType<TAction>();
    }

    object IPerformer.Perform<TParams>(IActionWithResult<TParams> action, TParams parameters)
    {
      if(ReferenceEquals(action, null))
        throw new ArgumentNullException(nameof(action));

      return action.Execute(this, parameters);
    }

    /// <summary>
    /// Gets the <see cref="IAbility"/> which corresponds to creating instance of the indicated action type.
    /// </summary>
    /// <returns>The ability, or a <c>null</c> reference if no possessed ability is suitable.</returns>
    /// <typeparam name="TAction">The desired action type.</typeparam>
    protected virtual IAbility GetAbility<TAction>()
    {
      return abilities.FirstOrDefault(x => x.CanProvideAction<TAction>());
    }

    /// <summary>
    /// Gets a value which determines whether or not the current instance can perform actions of the indicated
    /// action type.
    /// </summary>
    /// <returns><c>true</c>, if action type is supported, <c>false</c> otherwise.</returns>
    /// <typeparam name="TAction">The desired action type.</typeparam>
    protected virtual bool SupportsActionType<TAction>()
    {
      return GetAbility<TAction>() != null;
    }

    /// <summary>
    /// Creates and returns an instance of the indicated action type, using the actors abilities.
    /// </summary>
    /// <returns>The action instance.</returns>
    /// <typeparam name="TAction">The desired action type.</typeparam>
    protected virtual TAction GetAction<TAction>() where TAction : class
    {
      var ability = GetAbility<TAction>();
      if(ability == null)
        return null;

      return ability.GetAction<TAction>();
    }

    /// <summary>
    /// Gets a collection of all of the <c>System.Type</c> of actions supported by the current actor's abilities.
    /// </summary>
    /// <returns>The all action types.</returns>
    protected virtual IEnumerable<Type> GetAllActionTypes()
    {
      return abilities
        .SelectMany(x => x.GetActionTypes())
        .Distinct()
        .ToArray();
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="Performer"/> class.
    /// </summary>
    /// <param name="abilities">Abilities.</param>
    /// <param name="name">Name.</param>
    internal Performer(IEnumerable<IAbility> abilities, string name)
    {
      if(name == null)
        throw new ArgumentNullException(nameof(name));
      if(abilities == null)
        throw new ArgumentNullException(nameof(abilities));

      this.name = name;
      this.abilities = abilities;
    }

    #endregion
  }
}
