using System;
namespace CSF.Screenplay.Actors
{
  public interface ICanPerformActions
  {
    bool SupportsActionType<TAction>();

    TAction GetAction<TAction>() where TAction : class;
  }
}
