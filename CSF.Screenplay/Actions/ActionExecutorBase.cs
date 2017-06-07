using System;
using System.Collections.Generic;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Actions
{
  public abstract class ActionExecutorBase<TAction> where TAction : class
  {
    readonly ICollection<System.Action<TAction>> configurations;

    protected virtual void AddConfiguration(System.Action<TAction> configuration)
    {
      configurations.Add(configuration);
    }

    protected virtual void Configure(TAction action)
    {
      if(action == null)
        throw new ArgumentNullException(nameof(action));

      foreach(var config in configurations)
      {
        if(config == null)
          continue;
        
        config(action);
      }
    }

    protected virtual TAction GetAction(ICanPerformActions performer)
    {
      if(performer == null)
        throw new ArgumentNullException(nameof(performer));

      if(!performer.SupportsActionType<TAction>())
      {
        throw new ArgumentException($"The performer must support the action type `{typeof(TAction).Name}'.",
                                    nameof(performer));
      }

      var action = performer.GetAction<TAction>();
      Configure(action);
      return action;
    }

    public ActionExecutorBase()
    {
      configurations = new List<System.Action<TAction>>();
    }
  }
}
