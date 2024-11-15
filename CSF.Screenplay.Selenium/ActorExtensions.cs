using System;
using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// Extension methods for actors.
    /// </summary>
    public static class ActorExtensions
    {
        /// <summary>
        /// Gets a lazy-loaded <see cref="SeleniumElement"/> instance for the given actor and target.
        /// </summary>
        /// <param name="actor">An actor, who must have the <see cref="BrowseTheWeb"/> ability.</param>
        /// <param name="target">A target which describes an HTML element</param>
        /// <returns>A lazy <see cref="SeleniumElement"/>, the value of which is the HTML element described by the target.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="actor"/> is <see langword="null" /></exception>
        /// <exception cref="ArgumentException">If the actor does not implement <see cref="IHasAbilities"/></exception>
        /// <exception cref="InvalidOperationException">If the actor does not have the <see cref="BrowseTheWeb"/> ability</exception>
        /// <exception cref="TargetNotFoundException">If the target does not yield an HTML element</exception>
        public static Lazy<SeleniumElement> GetLazyElement(this ICanPerform actor, ITarget target)
            => new Lazy<SeleniumElement>(() => target.GetElement(actor.GetAbility<BrowseTheWeb>().WebDriver));

        /// <summary>
        /// Gets a lazy-loaded <see cref="SeleniumElement"/> instance for the given actor and target.
        /// </summary>
        /// <param name="actor">An actor, who must have the <see cref="BrowseTheWeb"/> ability.</param>
        /// <param name="target">A target which describes an HTML element</param>
        /// <returns>A lazy <see cref="SeleniumElement"/>, the value of which is the HTML element described by the target.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="actor"/> is <see langword="null" /></exception>
        /// <exception cref="InvalidCastException">If the actor does not implement <see cref="ICanPerform"/></exception>
        /// <exception cref="ArgumentException">If the actor does not implement <see cref="IHasAbilities"/></exception>
        /// <exception cref="InvalidOperationException">If the actor does not have the <see cref="BrowseTheWeb"/> ability</exception>
        /// <exception cref="TargetNotFoundException">If the target does not yield an HTML element</exception>
        public static Lazy<SeleniumElement> GetLazyElement(this IHasName actor, ITarget target)
        {
            if (actor is null)
                throw new ArgumentNullException(nameof(actor));

            var performer = (ICanPerform) actor;
            return new Lazy<SeleniumElement>(() => target.GetElement(performer.GetAbility<BrowseTheWeb>().WebDriver));
        }
    }
}