using CSF.Screenplay.Selenium.Builders;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Elements
{
    /// <summary>
    /// Extension methods for <see cref="ITarget"/> instances.
    /// </summary>
    public static class TargetExtensions
    {
        /// <summary>
        /// Creates a new <see cref="QueryPredicatePrototypeBuilder"/> for the specified target, which represents a single HTML element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is used with the creation of predicates for <see cref="PerformableBuilder.WaitUntil(System.Func{IWebDriver, bool})"/>
        /// to wait until the current target instance meets the specified conditions.
        /// </para>
        /// </remarks>
        /// <param name="target">The target for which to create the query builder.</param>
        /// <returns>A new instance of <see cref="QueryPredicatePrototypeBuilder"/>.</returns>
        public static QueryPredicatePrototypeBuilder Has(this ITarget target) => new QueryPredicatePrototypeBuilder(target, false);

        /// <summary>
        /// Creates a new <see cref="QueryPredicatePrototypeBuilder"/> for the specified target, which represents a collection of HTML elements.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is used with the creation of predicates for <see cref="PerformableBuilder.WaitUntil(System.Func{IWebDriver, bool})"/>
        /// to wait until every element expsed by the current target instance meets the specified conditions.
        /// </para>
        /// </remarks>
        /// <param name="target">The target for which to create the query builder.</param>
        /// <returns>A new instance of <see cref="QueryPredicatePrototypeBuilder"/>.</returns>
        public static QueryPredicatePrototypeBuilder AllHave(this ITarget target) => new QueryPredicatePrototypeBuilder(target, true);
    }
}