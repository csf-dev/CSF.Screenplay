using System;
namespace CSF.Screenplay.Actions
{
  public interface IActionWithResult<TParams>
  {
    object Execute(TParams parameters);
  }
}
