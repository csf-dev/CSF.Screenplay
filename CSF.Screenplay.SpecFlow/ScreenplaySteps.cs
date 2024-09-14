using CSF.Screenplay.Actors;
using TechTalk.SpecFlow;

namespace CSF.Screenplay
{
    /// <summary>
    /// Convenience subclass of <c>TechTalk.SpecFlow.Steps</c> making it possible to call <c>Given, When &amp; Then</c>
    /// methods without conflicting with the built-in methods of the same names.
    /// </summary>
    public class ScreenplaySteps : Steps
    {
        /// <summary>
        /// Returns the actor instance, as an <see cref="ICanPerformGiven"/>, in order to perform precondition actions.
        /// </summary>
        /// <param name="actor">The actor.</param>
        public ICanPerformGiven Given(Actor actor) => PerformanceStarter.Given(actor);

        /// <summary>
        /// Returns the actor instance, as an <see cref="ICanPerformWhen"/>, in order to perform actions which exercise the
        /// system under test.
        /// </summary>
        /// <param name="actor">The actor.</param>
        public ICanPerformWhen When(Actor actor) => PerformanceStarter.When(actor);

        /// <summary>
        /// Returns the actor instance, as an <see cref="ICanPerformThen"/>, in order to get information which are required to
        /// make assertions that the scenario has completed successfully.
        /// </summary>
        /// <param name="actor">The actor.</param>
        public ICanPerformThen Then(Actor actor) => PerformanceStarter.Then(actor);
    }
}
