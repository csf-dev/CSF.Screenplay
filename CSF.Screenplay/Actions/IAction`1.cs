using System;
namespace CSF.Screenplay.Actions
{
  public interface IAction<TResult>
  {
    TResult Execute();
  }
}
