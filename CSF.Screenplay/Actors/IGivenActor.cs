using System;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Represents an actor which is able to perform 'given' steps (preconditions to performing actions).
  /// </summary>
  public interface IGivenActor
  {
    /// <summary>
    /// Performs the given action or task.
    /// </summary>
    /// <param name="performable">A performable item.</param>
    void WasAbleTo(IPerformable performable);

    /// <summary>
    /// Performs the given action, task or question and gets a result.
    /// </summary>
    /// <returns>The result of performing the task.</returns>
    /// <param name="performable">A task.</param>
    /// <typeparam name="TResult">The result type, returned from the task.</typeparam>
    TResult WasAbleTo<TResult>(IPerformable<TResult> performable);

    /// <summary>
    /// Asks the given question and gets the answer.
    /// </summary>
    /// <returns>The answer returned from the question.</returns>
    /// <param name="question">A question.</param>
    /// <typeparam name="TResult">The result type, returned from the question.</typeparam>
    TResult Saw<TResult>(IQuestion<TResult> question);
  }
}
