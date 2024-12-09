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
        /// Creates a new <see cref="PredicateQueryBuilder"/> for the specified target.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is used with the creation of predicates for <see cref="SeleniumPerformableBuilder.WaitUntil{T}(System.Func{IWebDriver, T})"/>
        /// to wait until the current target instance meets the specified conditions.
        /// </para>
        /// <para>
        /// This method and <see cref="Has(ITarget)"/> are synonyms and are functionally equivalent.
        /// </para>
        /// </remarks>
        /// <param name="target">The target for which to create the query builder.</param>
        /// <returns>A new instance of <see cref="PredicateQueryBuilder"/>.</returns>
        public static PredicateQueryBuilder Is(this ITarget target) => new PredicateQueryBuilder(target);

        /// <summary>
        /// Creates a new <see cref="PredicateQueryBuilder"/> for the specified target.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is used with the creation of predicates for <see cref="SeleniumPerformableBuilder.WaitUntil{T}(System.Func{IWebDriver, T})"/>
        /// to wait until the current target instance meets the specified conditions.
        /// </para>
        /// <para>
        /// This method and <see cref="Is(ITarget)"/> are synonyms and are functionally equivalent.
        /// </para>
        /// </remarks>
        /// <param name="target">The target for which to create the query builder.</param>
        /// <returns>A new instance of <see cref="PredicateQueryBuilder"/>.</returns>
        public static PredicateQueryBuilder Has(this ITarget target) => new PredicateQueryBuilder(target);
    }
}