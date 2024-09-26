using CSF.Screenplay.Abilities;

namespace CSF.Screenplay.Performables
{
    /// <summary>
    /// A builder for actions relating to the <see cref="UseAStopwatch"/> ability.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When using this class it is recommended to include <c>using static CSF.Screenplay.Performables.StopwatchBuilder;</c>
    /// in the source file which uses it.  This will allow you use the method names in this class in a more human-readable
    /// fashion.
    /// </para>
    /// <para>
    /// The actions and the question exposed by this builder allow an actor to accurately
    /// track and measure time elapsed during a Performance.
    /// </para>
    /// </remarks>
    public static class StopwatchBuilder
    {
        /// <summary>
        /// Gets a performable which starts the stopwatch.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use of this performable requires the actor has the <see cref="UseAStopwatch"/> ability.
        /// </para>
        /// </remarks>
        public static StartTheStopwatch StartTheStopwatch() => new StartTheStopwatch();

        /// <summary>
        /// Gets a performable which stops the stopwatch.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use of this performable requires the actor has the <see cref="UseAStopwatch"/> ability.
        /// </para>
        /// </remarks>
        public static StopTheStopwatch StopTheStopwatch() => new StopTheStopwatch();

        /// <summary>
        /// Gets a performable which resets the stopwatch to zero.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use of this performable requires the actor has the <see cref="UseAStopwatch"/> ability.
        /// </para>
        /// </remarks>
        public static ResetTheStopwatch ResetTheStopwatch() => new ResetTheStopwatch();

        /// <summary>
        /// Gets a performable which reads the stopwatch.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use of this performable requires the actor has the <see cref="UseAStopwatch"/> ability.
        /// </para>
        /// </remarks>
        public static ReadTheStopwatch ReadTheStopwatch() => new ReadTheStopwatch();
    }
}
