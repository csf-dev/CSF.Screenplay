using System;
using System.Collections.Generic;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Questions;
using CSF.Screenplay.Tasks;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay
{
  public class Actor : IGivenActor, IWhenActor, IThenActor, ICanReceiveAbilities
  {
    #region fields

    readonly ISet<IAbility> abilities;

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

    void IThenActor.Should(IExpectation expectation)
    {
      Verify(expectation);
    }

    protected virtual void Perform(ITask task)
    {
      var provider = GetPerformer();
      task.Execute(provider);
    }

    protected virtual TResult Perform<TResult>(ITask<TResult> task)
    {
      var provider = GetPerformer();
      return task.Execute(provider);
    }

    protected virtual void Verify(IExpectation expectation)
    {
      var provider = GetPerformer();
      expectation.Verify(provider);
    }

    #endregion

    #region ICanReceiveAbilities implementation

    public virtual void IsAbleTo<TAbility>() where TAbility : IAbility
    {
      var ability = Activator.CreateInstance<TAbility>();
      IsAbleTo(ability);
    }

    public virtual void IsAbleTo(Type abilityType)
    {
      if(!typeof(IAbility).IsAssignableFrom(abilityType))
        throw new ArgumentException($"Ability type must implement `{typeof(IAbility).Name}'.", nameof(abilityType));
      
      var ability = (IAbility) Activator.CreateInstance(abilityType);
      IsAbleTo(ability);
    }

    public virtual void IsAbleTo(IAbility ability)
    {
      if(ability == null)
        throw new ArgumentNullException(nameof(ability));

      ability.RegisterActions();
      abilities.Add(ability);
    }

    protected virtual IPerformer GetPerformer()
    {
      return new Performer(abilities);
    }

    #endregion

    #region constructor

    public Actor()
    {
      abilities = new HashSet<IAbility>();
    }

    #endregion
  }
}
