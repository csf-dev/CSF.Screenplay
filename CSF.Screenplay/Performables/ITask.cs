using System;

namespace CSF.Screenplay.Performables
{
  /// <summary>
  /// Represents a task, which typically performs one or more actions (or uses sub-tasks) in order to alter the
  /// state of the application.
  /// </summary>
  /// <remarks>
  /// <para>
  /// Tasks make use of the <see cref="Actors.IPerformer"/> (actor) passed to them, in order to get actions.  Actions
  /// are then used to interact with the application and - typically - make changes to its state.
  /// Additionally, tasks may make use of other tasks, using composition of low-level tasks in order to create
  /// higher-level ones.
  /// </para>
  /// </remarks>
  public interface ITask : IPerformable
  {
  }
}