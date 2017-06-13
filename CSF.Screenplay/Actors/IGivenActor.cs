using System;
using CSF.Screenplay.Questions;
using CSF.Screenplay.Tasks;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Represents an actor which is able to perform 'given' steps (preconditions to performing actions).
  /// </summary>
  public interface IGivenActor
  {
    /// <summary>
    /// Performs the given task.
    /// </summary>
    /// <param name="task">A task.</param>
    void WasAbleTo(ITask task);

    /// <summary>
    /// Performs the given task and gets a result.
    /// </summary>
    /// <returns>The result of performing the task.</returns>
    /// <param name="task">A task.</param>
    /// <typeparam name="TResult">The result type, returned from the task.</typeparam>
    TResult WasAbleTo<TResult>(ITask<TResult> task);

    /// <summary>
    /// Asks the given question and gets the answer.
    /// </summary>
    /// <returns>The answer returned from the question.</returns>
    /// <param name="question">A question.</param>
    object Saw(IQuestion question);

    /// <summary>
    /// Asks the given question and gets the answer.
    /// </summary>
    /// <returns>The answer returned from the question.</returns>
    /// <param name="question">A question.</param>
    /// <typeparam name="TResult">The result type, returned from the question.</typeparam>
    TResult Saw<TResult>(IQuestion<TResult> question);
  }
}
