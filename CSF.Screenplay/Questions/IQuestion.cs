using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Questions
{
  /// <summary>
  /// Represents a question, which queries the state of the application from the performer's perspective.
  /// </summary>
  /// <remarks>
  /// <para>
  /// Behind this facade, questions are essentially specialisations of tasks, which always return an answer value.
  /// Additionally, it is good practice to avoid changing the state of the application with questions.
  /// </para>
  /// <para>
  /// Underneath this, questions work much like tasks - they make use of actions to interact with the application.
  /// The only real difference is conceptual.
  /// </para>
  /// </remarks>
  public interface IQuestion
  {
    /// <summary>
    /// Gets the answer to the current question.
    /// </summary>
    /// <returns>The answer.</returns>
    /// <param name="actor">The actor for whom we are asking this question.</param>
    object GetAnswer(IPerformer actor);
  }
}