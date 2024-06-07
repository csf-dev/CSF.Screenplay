using System;

namespace CSF.Screenplay.Performables
{
  /// <summary>
  /// Represents an action which may be performed by an actor. This action returns a response value.
  /// </summary>
  public interface IAction<TResult> : IPerformable<TResult>
  {
  }
}
