using System;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Questions
{
  /// <summary>
  /// An <see cref="IQuestion"/>, which has a specific (generic) answer type.
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
  public interface IQuestion<TAnswer> : IQuestion
  {
    /// <summary>
    /// Gets the answer to the current question.
    /// </summary>
    /// <returns>The answer.</returns>
    /// <param name="actor">The actor for whom we are asking this question.</param>
    new TAnswer GetAnswer(IPerformer actor);
  }
}
