using System;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Represents an actor which is able to perform 'when' steps (performing the actions which are to be tested, usually
  /// exercising the application).
  /// </summary>
  public interface IWhenActor
  {
    /// <summary>
    /// Performs the given action or task.
    /// </summary>
    /// <param name="performable">A performable item.</param>
    void AttemptsTo(IPerformable performable);

    /// <summary>
    /// Performs an action or task which has a public parameterless constructor.
    /// </summary>
    /// <typeparam name="TPerformable">The type of the performable item to execute.</typeparam>
    void AttemptsTo<TPerformable>() where TPerformable : IPerformable,new();

    /// <summary>
    /// Performs the given action, task or question and gets a result.
    /// </summary>
    /// <returns>The result of performing the task.</returns>
    /// <param name="performable">A task.</param>
    /// <typeparam name="TResult">The result type, returned from the task.</typeparam>
    TResult AttemptsTo<TResult>(IPerformable<TResult> performable);

    /// <summary>
    /// Asks the given question and gets the answer.  This is a synonym of <see cref="Gets"/>.
    /// </summary>
    /// <returns>The answer returned from the question.</returns>
    /// <param name="question">A question.</param>
    /// <typeparam name="TResult">The result type, returned from the question.</typeparam>
    TResult Sees<TResult>(IQuestion<TResult> question);

    /// <summary>
    /// Asks the given question and gets the answer.
    /// </summary>
    /// <returns>The answer returned from the question.</returns>
    /// <param name="question">A question.</param>
    /// <typeparam name="TResult">The result type, returned from the question.</typeparam>
    TResult Gets<TResult>(IQuestion<TResult> question);

    /// <summary>
    /// Asks the given question and gets the answer.  This is a synonym of <see cref="Gets"/>.
    /// </summary>
    /// <returns>The answer returned from the question.</returns>
    /// <param name="question">A question.</param>
    /// <typeparam name="TResult">The result type, returned from the question.</typeparam>
    TResult Reads<TResult>(IQuestion<TResult> question);
  }
}
