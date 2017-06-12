using System;
namespace CSF.Screenplay.Actions
{
  public interface IAction<TParams>
  {
    void Execute(TParams parameters);
  }
}
