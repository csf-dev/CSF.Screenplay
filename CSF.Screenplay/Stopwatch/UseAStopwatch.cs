using System;
using CSF.Screenplay.Abilities;

namespace CSF.Screenplay.Stopwatch
{
  /// <summary>
  /// A Screenplay ability which indicates that the actor is able to make use of a stopwatch
  /// in order to time other actions/tasks.
  /// </summary>
  public class UseAStopwatch : Ability
  {
    readonly System.Diagnostics.Stopwatch watch;

    /// <summary>
    /// Gets access to the underlying stopwatch instance.
    /// </summary>
    /// <value>The watch.</value>
    public System.Diagnostics.Stopwatch Watch => watch;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(Actors.INamed actor) => $"{actor.Name} is able to use a stopwatch";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Stopwatch.UseAStopwatch"/> class.
    /// </summary>
    public UseAStopwatch()
    {
      watch = new System.Diagnostics.Stopwatch();
    }
  }
}
