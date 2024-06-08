using CSF.Screenplay.Actors;

namespace CSF.Screenplay
{
    /// <summary>Helper class for beginning Screenplay performances from your own logic, using a fluent interface</summary>
    /// <remarks>
    /// <para>
    /// This class is a convenience to aid in the readability of high-level performance logic, providing a fluent entry-point
    /// into the performance methods.
    /// Each of the methods upon this class corresponds to a <see cref="PerformancePhase"/> within the overall performance.
    /// By using these methods, the actor is down-cast to the appropriate interface that is specific to that phase, which
    /// activates appropriate functionality of the fluent interface.
    /// </para>
    /// <para>
    /// It is recommended to consume this functionality in your own logic via the <c>using static</c> directive, so that you may
    /// use the <see cref="Given(Actor)"/>, <see cref="When(Actor)"/> &amp; <see cref="Then(Actor)"/> methods stand-alone.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>Here is an example of the recommended technique for consuming this class' functionality.</para>
    /// <code>
    /// using static CSF.Screenplay.ScreenplayStarter;
    /// 
    /// // ... then in your high-level performance logic:
    /// await Given(joe).WasAbleTo(takeOutTheTrash);
    /// </code>
    /// </example>
    public static class ScreenplayStarter
    {
        /// <summary>
        /// Returns the actor instance, down-cast to <see cref="ICanPerformGiven"/>, activating the fluent interface for the
        /// <see cref="PerformancePhase.Given"/> phase of the performance.
        /// </summary>
        /// <param name="actor">The actor.</param>
        public static ICanPerformGiven Given(Actor actor) => actor;

        /// <summary>
        /// Returns the actor instance, down-cast to <see cref="ICanPerformWhen"/>, activating the fluent interface for the
        /// <see cref="PerformancePhase.When"/> phase of the performance.
        /// </summary>
        /// <param name="actor">The actor.</param>
        public static ICanPerformWhen When(Actor actor) => actor;

        /// <summary>
        /// Returns the actor instance, down-cast to <see cref="ICanPerformThen"/>, activating the fluent interface for the
        /// <see cref="PerformancePhase.Then"/> phase of the performance.
        /// </summary>
        /// <param name="actor">The actor.</param>
        public static ICanPerformThen Then(Actor actor) => actor;
    }
}