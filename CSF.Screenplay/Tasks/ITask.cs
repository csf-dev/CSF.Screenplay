using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Tasks
{
  /// <summary>
  /// Represents a task, which typically performs one or more actions (or uses sub-tasks) in order to alter the
  /// state of the application.
  /// </summary>
  /// <remarks>
  /// <para>
  /// Tasks make use of the <see cref="IPerformer"/> (actor) passed to them, in order to get actions.  Actions
  /// are then used to interact with the application and - typically - make changes to its state.
  /// Additionally, tasks may make use of other tasks, using composition of low-level tasks in order to create
  /// higher-level ones.
  /// </para>
  /// </remarks>
  public interface ITask
  {
    /// <summary>
    /// Performs this task, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
    void PerformAs(IPerformer actor);
  }
}