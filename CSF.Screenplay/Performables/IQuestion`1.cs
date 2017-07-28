using System;

namespace CSF.Screenplay.Performables
{
  /// <summary>
  /// A question, which has a specific (generic) answer type.
  /// </summary>
  /// <remarks>
  /// <para>
  /// Behind this facade, questions are essentially specialisations of performables, which always return an answer value.
  /// Additionally, it is good practice to avoid changing the state of the application with questions.
  /// </para>
  /// <para>
  /// Underneath this, questions work much like tasks - they make use of actions to interact with the application.
  /// The only real difference is conceptual.
  /// </para>
  /// </remarks>
  public interface IQuestion<TAnswer> : IPerformable<TAnswer>
  {
  }
}
