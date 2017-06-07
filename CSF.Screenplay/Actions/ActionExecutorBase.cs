using System;
namespace CSF.Screenplay.Actions
{
  public abstract class ActionExecutorBase
  {
    protected virtual void Configure<TAction>(Action<TAction> configuration, TAction action) where TAction : class
    {
      if(configuration == null)
        throw new ArgumentNullException(nameof(configuration));
      if(action == null)
        throw new ArgumentNullException(nameof(action));
      
      configuration(action);
    }

    //protected virtual TAction GetAction<TAction>()
  }
}
