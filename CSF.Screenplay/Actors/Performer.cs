using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actions;

namespace CSF.Screenplay.Actors
{
  public class Performer : IPerformer, ICanPerformActions
  {
    #region fields

    readonly IEnumerable<IAbility> abilities;

    #endregion

    #region public API

    public virtual void AttemptsTo<TAction>(IActionExecutor<TAction> executor)
      where TAction : IAction
    {
      if(executor == null)
        throw new ArgumentNullException(nameof(executor));
      executor.Execute(this);
    }

    public TResult AttemptsTo<TAction,TResult>(IActionExecutorWithResult<TAction> executor)
      where TAction : IAction<TResult>
    {
      if(executor == null)
        throw new ArgumentNullException(nameof(executor));
      return (TResult) executor.Execute(this);
    }

    #endregion

    #region ICanPerformActions implementation

    TAction ICanPerformActions.GetAction<TAction>()
    {
      return GetAction<TAction>();
    }

    bool ICanPerformActions.SupportsActionType<TAction>()
    {
      return SupportsActionType<TAction>();
    }

    protected virtual IAbility GetAbility<TAction>()
    {
      return abilities.FirstOrDefault(x => x.CanProvideAction<TAction>());
    }

    protected virtual bool SupportsActionType<TAction>()
    {
      return GetAbility<TAction>() != null;
    }

    protected virtual TAction GetAction<TAction>() where TAction : class
    {
      var ability = GetAbility<TAction>();
      if(ability == null)
        return null;

      return ability.GetAction<TAction>();
    }

    protected virtual IEnumerable<Type> GetAllActionTypes()
    {
      return abilities
        .SelectMany(x => x.GetActionTypes())
        .Distinct()
        .ToArray();
    }

    #endregion

    #region constructor

    internal Performer(IEnumerable<IAbility> abilities)
    {
      if(abilities == null)
        throw new ArgumentNullException(nameof(abilities));

      this.abilities = abilities;
    }

    #endregion
  }
}
