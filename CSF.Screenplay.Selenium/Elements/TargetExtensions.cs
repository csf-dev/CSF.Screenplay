using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Elements
{
    /// <summary>
    /// Extension methods for <see cref="ITarget"/> instances.
    /// </summary>
    public static class TargetExtensions
    {
        /// <summary>
        /// Gets a single Selenium element which matches the current target.
        /// </summary>
        /// <param name="target">An object which implements <see cref="ITarget"/>.</param>
        /// <param name="driver">A WebDriver.</param>
        /// <returns>An instance of <see cref="SeleniumElement"/>.</returns>
        /// <exception cref="TargetNotFoundException">If the target does not yield an HTML element.</exception>
        public static SeleniumElement GetElement(this ITarget target, IWebDriver driver) => target.GetElement(driver);

        /// <summary>
        /// Gets a single Selenium element which matches the current target.
        /// </summary>
        /// <param name="target">An object which implements <see cref="ITarget"/>.</param>
        /// <param name="ability">An actor's <see cref="BrowseTheWeb"/> ability.</param>
        /// <returns>An instance of <see cref="SeleniumElement"/>.</returns>
        /// <exception cref="TargetNotFoundException">If the target does not yield an HTML element.</exception>
        public static SeleniumElement GetElement(this ITarget target, BrowseTheWeb ability) => target.GetElement(ability.WebDriver);

        /// <summary>
        /// Gets a single Selenium element which matches the current target.
        /// </summary>
        /// <param name="target">An object which implements <see cref="ITarget"/>.</param>
        /// <param name="actor">An actor, who must have the <see cref="BrowseTheWeb"/> ability.</param>
        /// <returns>An instance of <see cref="SeleniumElement"/>.</returns>
        /// <exception cref="TargetNotFoundException">If the target does not yield an HTML element.</exception>
        /// <exception cref="System.InvalidOperationException">If the actor does not have the <see cref="BrowseTheWeb"/> ability.</exception>
        public static SeleniumElement GetElement(this ITarget target, ICanPerform actor) => target.GetElement(actor.GetAbility<BrowseTheWeb>().WebDriver);
    }
}