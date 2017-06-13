using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Tasks;

namespace CSF.Screenplay.Questions
{
  /// <summary>
  /// Base type for implementations of <see cref="T:IQuestion{TAnswer}"/>.  Note that this type may also be used as an
  /// <see cref="T:ITask{TAnswer}"/>.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This base type provides just one abstract method which must be overridden.
  /// </para>
  /// <para>
  /// Implementors of questions are not required to derive from this base type.  It is provided only as a convenience
  /// where suitable.
  /// </para>
  /// <para>
  /// Behind this facade, questions are essentially specialisations of tasks, which always return an answer value.
  /// Additionally, it is good practice to avoid changing the state of the application with questions.
  /// </para>
  /// <para>
  /// Underneath this, questions work much like tasks - they make use of actions to interact with the application.
  /// The only real difference is conceptual.
  /// </para>
  /// </remarks>
  public abstract class Question<TAnswer> : IQuestion<TAnswer>, ITask<TAnswer>, ITaskWithResult
  {
    /// <summary>
    /// Gets the answer to the current question.
    /// </summary>
    /// <returns>The answer.</returns>
    /// <param name="actor">The actor for whom we are asking this question.</param>
    protected abstract TAnswer GetAnswer(IPerformer actor);

    TAnswer IQuestion<TAnswer>.GetAnswer(IPerformer actor)
    {
      return GetAnswer(actor);
    }

    object IQuestion.GetAnswer(IPerformer actor)
    {
      return GetAnswer(actor);
    }

    TAnswer ITask<TAnswer>.PerformAs(IPerformer actor)
    {
      return GetAnswer(actor);
    }

    object ITaskWithResult.PerformAs(IPerformer actor)
    {
      return GetAnswer(actor);
    }
  }
}
