using CSF.Screenplay.Abilities;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay
{
    public static partial class PerforamableBuilder
    {
        /// <summary>
        /// Gets a performable which starts the stopwatch.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use of this performable requires the actor has the <see cref="UseAStopwatch"/> ability,
        /// for accurately measuring time, during the course of a performance.
        /// </para>
        /// </remarks>
        public static StartTheStopwatch StartTheStopwatch() => new StartTheStopwatch();

        /// <summary>
        /// Gets a performable which stops the stopwatch.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use of this performable requires the actor has the <see cref="UseAStopwatch"/> ability,
        /// for accurately measuring time, during the course of a performance.
        /// </para>
        /// </remarks>
        public static StopTheStopwatch StopTheStopwatch() => new StopTheStopwatch();

        /// <summary>
        /// Gets a performable which resets the stopwatch to zero.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use of this performable requires the actor has the <see cref="UseAStopwatch"/> ability,
        /// for accurately measuring time, during the course of a performance.
        /// </para>
        /// </remarks>
        public static ResetTheStopwatch ResetTheStopwatch() => new ResetTheStopwatch();

        /// <summary>
        /// Gets a performable which reads the stopwatch.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use of this performable requires the actor has the <see cref="UseAStopwatch"/> ability,
        /// for accurately measuring time, during the course of a performance.
        /// </para>
        /// </remarks>
        public static ReadTheStopwatch ReadTheStopwatch() => new ReadTheStopwatch();
    }
}