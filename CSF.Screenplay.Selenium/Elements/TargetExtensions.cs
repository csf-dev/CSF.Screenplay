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
        /// Creates a new <see cref="PredicateQueryBuilder"/> for the specified target, which represents a single HTML element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is used with the creation of predicates for <see cref="SeleniumPerformableBuilder.WaitUntil(System.Func{IWebDriver, bool})"/>
        /// to wait until the current target instance meets the specified conditions.
        /// </para>
        /// <para>
        /// This method and <see cref="Has(ITarget)"/> are synonyms and are functionally equivalent.
        /// </para>
        /// </remarks>
        /// <param name="target">The target for which to create the query builder.</param>
        /// <returns>A new instance of <see cref="PredicateQueryBuilder"/>.</returns>
        public static PredicateQueryBuilder Is(this ITarget target) => new PredicateQueryBuilder(target, false);

        /// <summary>
        /// Creates a new <see cref="PredicateQueryBuilder"/> for the specified target, which represents a single HTML element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is used with the creation of predicates for <see cref="SeleniumPerformableBuilder.WaitUntil(System.Func{IWebDriver, bool})"/>
        /// to wait until the current target instance meets the specified conditions.
        /// </para>
        /// <para>
        /// This method and <see cref="Is(ITarget)"/> are synonyms and are functionally equivalent.
        /// </para>
        /// </remarks>
        /// <param name="target">The target for which to create the query builder.</param>
        /// <returns>A new instance of <see cref="PredicateQueryBuilder"/>.</returns>
        public static PredicateQueryBuilder Has(this ITarget target) => new PredicateQueryBuilder(target, false);

        /// <summary>
        /// Creates a new <see cref="PredicateQueryBuilder"/> for the specified target, which represents a collection of HTML elements.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is used with the creation of predicates for <see cref="SeleniumPerformableBuilder.WaitUntil(System.Func{IWebDriver, bool})"/>
        /// to wait until every element expsed by the current target instance meets the specified conditions.
        /// </para>
        /// <para>
        /// This method and <see cref="AllHave(ITarget)"/> are synonyms and are functionally equivalent.
        /// </para>
        /// </remarks>
        /// <param name="target">The target for which to create the query builder.</param>
        /// <returns>A new instance of <see cref="PredicateQueryBuilder"/>.</returns>
        public static PredicateQueryBuilder AreAll(this ITarget target) => new PredicateQueryBuilder(target, true);

        /// <summary>
        /// Creates a new <see cref="PredicateQueryBuilder"/> for the specified target, which represents a collection of HTML elements.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is used with the creation of predicates for <see cref="SeleniumPerformableBuilder.WaitUntil(System.Func{IWebDriver, bool})"/>
        /// to wait until every element expsed by the current target instance meets the specified conditions.
        /// </para>
        /// <para>
        /// This method and <see cref="AreAll(ITarget)"/> are synonyms and are functionally equivalent.
        /// </para>
        /// </remarks>
        /// <param name="target">The target for which to create the query builder.</param>
        /// <returns>A new instance of <see cref="PredicateQueryBuilder"/>.</returns>
        public static PredicateQueryBuilder AllHave(this ITarget target) => new PredicateQueryBuilder(target, true);
    }
}