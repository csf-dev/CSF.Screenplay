using System;
using CSF.Screenplay.Questions;
using CSF.Screenplay.Tasks;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Represents an actor which is able to perform 'when' steps (performing the actions which are to be tested, usually
  /// exercising the application).
  /// </summary>
  public interface IWhenActor
  {
    /// <summary>
    /// Performs the given task.
    /// </summary>
    /// <param name="task">A task.</param>
    void AttemptsTo(ITask task);

    /// <summary>
    /// Performs the given task and gets a result.
    /// </summary>
    /// <returns>The result of performing the task.</returns>
    /// <param name="task">A task.</param>
    /// <typeparam name="TResult">The result type, returned from the task.</typeparam>
    TResult AttemptsTo<TResult>(ITask<TResult> task);

    /// <summary>
    /// Asks the given question and gets the answer.
    /// </summary>
    /// <returns>The answer returned from the question.</returns>
    /// <param name="question">A question.</param>
    object Sees(IQuestion question);

    /// <summary>
    /// Asks the given question and gets the answer.
    /// </summary>
    /// <returns>The answer returned from the question.</returns>
    /// <param name="question">A question.</param>
    /// <typeparam name="TResult">The result type, returned from the question.</typeparam>
    TResult Sees<TResult>(IQuestion<TResult> question);
  }
}
