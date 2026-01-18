#if SPECFLOW
using CSF.Screenplay.Actors;
using TechTalk.SpecFlow;

namespace CSF.Screenplay
{
    /// <summary>
    /// A subclass of <c>TechTalk.SpecFlow.Steps</c> provided for convenience of SpecFlow 3.x users to avoid naming conflicts.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Note that this class does not exist in the Reqnroll Test Framework Integration.  It is irrelevant there.
    /// </para>
    /// <para>
    /// In SpecFlow 3.x, the <c>Steps</c> class has three methods named <c>Given</c>, <c>When</c> &amp; <c>Then</c> which can cause
    /// a naming conflict with the same-named methods of <see cref="PerformanceStarter"/>.  If you using the performance starter in
    /// the recommended way, then the methods of these two types can become ambiguous and force the developer to write additional
    /// boilerplate which spoils the ease-of-comprehension for Screenplay-based test logic.
    /// This subclass of <c>Steps</c> provides a workaround to that situation. Instead of deriving from the SpecFlow Steps class and including
    /// <c>using static CSF.Screenplay.PerformanceStarter;</c> at the top of your source file, have your bindings derive from this
    /// class instead. This provides Given, When, Then methods which have the same functionality as those in <see cref="PerformanceStarter"/>
    /// but in a manner which will not cause a name-resolution conflict.
    /// </para>
    /// <para>
    /// Note that in SpecFlow 4.x and up, and Reqnroll, this problem is irrelevant; there is no gain in using this subclass over the <c>using static</c> technique.
    /// The Given, When &amp; Then methods upon the SpecFlow <c>Steps</c> class were removed in v4.x, and never existed in any version of Reqnroll.
    /// This means that the naming conflict won't be present and that there is no need for your bindings to derive from this class instead
    /// of the official Steps class.
    /// </para>
    /// </remarks>
    public abstract class ScreenplaySteps : Steps
    {
        /// <summary>
        /// Returns the actor instance, as an <see cref="ICanPerformGiven"/>, in order to perform precondition actions.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is equivalent to <see cref="PerformanceStarter.Given(Actor)"/> but is provided as a convenience method
        /// in a subclass of <c>TechTalk.SpecFlow.Steps</c> to avoid method-name resolution conflicts.  See the remarks on
        /// <see cref="ScreenplaySteps"/> for more info.
        /// </para>
        /// </remarks>
        /// <param name="actor">The actor.</param>
        public static ICanPerformGiven Given(Actor actor) => PerformanceStarter.Given(actor);

        /// <summary>
        /// Returns the actor instance, as an <see cref="ICanPerformWhen"/>, in order to perform actions which exercise the
        /// system under test.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is equivalent to <see cref="PerformanceStarter.When(Actor)"/> but is provided as a convenience method
        /// in a subclass of <c>TechTalk.SpecFlow.Steps</c> to avoid method-name resolution conflicts.  See the remarks on
        /// <see cref="ScreenplaySteps"/> for more info.
        /// </para>
        /// </remarks>
        /// <param name="actor">The actor.</param>
        public static ICanPerformWhen When(Actor actor) => PerformanceStarter.When(actor);

        /// <summary>
        /// Returns the actor instance, as an <see cref="ICanPerformThen"/>, in order to get information which are required to
        /// make assertions that the scenario has completed successfully.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is equivalent to <see cref="PerformanceStarter.Then(Actor)"/> but is provided as a convenience method
        /// in a subclass of <c>TechTalk.SpecFlow.Steps</c> to avoid method-name resolution conflicts.  See the remarks on
        /// <see cref="ScreenplaySteps"/> for more info.
        /// </para>
        /// </remarks>
        /// <param name="actor">The actor.</param>
        public static ICanPerformThen Then(Actor actor) => PerformanceStarter.Then(actor);
    }
}
#endif
