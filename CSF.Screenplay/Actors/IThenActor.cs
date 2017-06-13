using System;
using CSF.Screenplay.Questions;
using CSF.Screenplay.Tasks;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Represents an actor which is able to perform 'then' steps (post-conditions to performing actions, usually
  /// assertions).
  /// </summary>
  public interface IThenActor
  {
    /// <summary>
    /// Performs the given task.
    /// </summary>
    /// <param name="task">A task.</param>
    void Should(ITask task);

    /// <summary>
    /// Performs the given task and gets a result.
    /// </summary>
    /// <returns>The result of performing the task.</returns>
    /// <param name="task">A task.</param>
    /// <typeparam name="TResult">The result type, returned from the task.</typeparam>
    TResult Should<TResult>(ITask<TResult> task);

    /// <summary>
    /// Asks the given question and gets the answer.
    /// </summary>
    /// <returns>The answer returned from the question.</returns>
    /// <param name="question">A question.</param>
    object ShouldSee(IQuestion question);

    /// <summary>
    /// Asks the given question and gets the answer.
    /// </summary>
    /// <returns>The answer returned from the question.</returns>
    /// <param name="question">A question.</param>
    /// <typeparam name="TResult">The result type, returned from the question.</typeparam>
    TResult ShouldSee<TResult>(IQuestion<TResult> question);
  }
}
