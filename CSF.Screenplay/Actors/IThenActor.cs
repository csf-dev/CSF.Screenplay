using System;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Represents an actor which is able to perform 'then' steps (post-conditions to performing actions, usually
  /// assertions).
  /// </summary>
  public interface IThenActor
  {
    /// <summary>
    /// Performs the given action or task.
    /// </summary>
    /// <param name="performable">A performable item.</param>
    void Should(IPerformable performable);

    /// <summary>
    /// Performs an action or task which has a public parameterless constructor.
    /// </summary>
    /// <typeparam name="TPerformable">The type of the performable item to execute.</typeparam>
    void Should<TPerformable>() where TPerformable : IPerformable,new();

    /// <summary>
    /// Performs the given action, task or question and gets a result.
    /// </summary>
    /// <returns>The result of performing the task.</returns>
    /// <param name="performable">A task.</param>
    /// <typeparam name="TResult">The result type, returned from the task.</typeparam>
    TResult Should<TResult>(IPerformable<TResult> performable);

    /// <summary>
    /// Asks the given question and gets the answer.
    /// </summary>
    /// <returns>The answer returned from the question.</returns>
    /// <param name="question">A question.</param>
    /// <typeparam name="TResult">The result type, returned from the question.</typeparam>
    TResult ShouldSee<TResult>(IQuestion<TResult> question);
  }
}
