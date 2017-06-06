using System;
using System.Collections.Generic;

namespace CSF.Screenplay
{
  public class Actor : IActor, IGivenActor, IWhenActor, IThenActor, ICanReceiveAbilities
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

    void IThenActor.Should(IExpectation expectation)
    {
      Verify(expectation);
    }

    protected virtual void Perform(ITask task)
    {
      var provider = GetActionProvider();
      task.Execute(provider);
    }

    protected virtual void Verify(IExpectation expectation)
    {
      var provider = GetActionProvider();
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

      abilities.Add(ability);
    }

    protected virtual ICanPerformActions GetActionProvider()
    {
      return new ActionProvider(abilities);
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
