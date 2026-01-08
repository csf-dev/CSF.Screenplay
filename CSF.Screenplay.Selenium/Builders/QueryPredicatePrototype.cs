
using System;
using System.Linq;
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Elements;
using CSF.Screenplay.Selenium.Queries;
using CSF.Screenplay.Selenium.Questions;
using CSF.Specifications;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Builders
{

    /// <summary>
    /// A prototype object which may be converted into either a <see cref="WaitUntilPredicate{Boolean}"/> or an <see cref="ISpecificationFunction{SeleniumElement}"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Wait-until predicates and element specifications are conceptually quite similar.  Both represent predicates for Selenium elements in one form or another.
    /// Becuase of this similarity, it is useful to use just one builder, which is capable of producing either form.  This class represents the intermediate
    /// form which may be converted into either a wait-until predicate or an element specification.
    /// </para>
    /// </remarks>
    /// <typeparam name="TQueryable">The type of value which is returned by <see cref="Query"/>.</typeparam>
    public sealed class QueryPredicatePrototype<TQueryable> : IBuildsElementPredicates
    {
        /// <summary>
        /// Gets the target, which represents one or more Selenium elements upon which this predicate will operate.
        /// </summary>
        public ITarget Target { get; }

        /// <summary>
        /// Gets a specification function which will be evaluated against the value(s) produced by the <see cref="Query"/>.
        /// </summary>
        public ISpecificationFunction<TQueryable> Specification { get; }

        /// <summary>
        /// Gets the query which will be used to obtain value(s) from the target element(s).
        /// </summary>
        public IQuery<TQueryable> Query { get; }

        /// <summary>
        /// Gets a function which will create a human-readable summary for this predicate, as would appear in Screenplay reports.
        /// </summary>
        public Func<ITarget, string> SummaryCreator { get; }

        /// <summary>
        /// Gets a value indicating whether this predicate is intended to operate upon multiple elements.
        /// </summary>
        public bool MultiElement { get; }

        /// <summary>
        /// Gets a human-readable summary for this predicate, as would appear in Screenplay reports.
        /// </summary>
        /// <returns>A string summary</returns>
        public string GetSummary() => SummaryCreator(Target);

        /// <summary>
        /// Gets a <see cref="Func{IWebDriver, Boolean}"/> which may be used to evaluate this predicate against a web driver.
        /// </summary>
        /// <returns>A predicate function</returns>
        /// <exception cref="InvalidOperationException">If this instance has not been initialised in a manner compatible with creating Web Driver predicates</exception>
        public Func<IWebDriver, bool> GetWebDriverPredicate()
        {
            if (Target == null)
                throw new InvalidOperationException($"The {nameof(Target)} property must not be null when calling this method.");
            if (SummaryCreator == null)
                throw new InvalidOperationException($"The {nameof(SummaryCreator)} property must not be null when calling this method.");

            if (MultiElement)
                return driver => Target.GetElements(driver).All(element => Specification.Matches(Query.GetValue(element)));
            return driver => Specification.Matches(Query.GetValue(Target.GetElement(driver)));
        }

        /// <inheritdoc/>
        public WaitUntilPredicate<bool> GetWaitUntilPredicate()
            => WaitUntilPredicate.From(GetWebDriverPredicate(), GetSummary());

        /// <inheritdoc/>
        public ISpecificationFunction<SeleniumElement> GetElementSpecification()
            => new FilterSpecification<TQueryable>(Query, Specification);

        /// <summary>
        /// Initialises a new instance of the <see cref="QueryPredicatePrototype{TQueryable}"/> class in a manner suitable for using either <see cref="GetElementSpecification"/>
        /// or <see cref="GetWaitUntilPredicate"/>.
        /// </summary>
        /// <param name="specification">The specification function for the value returned by the <paramref name="query"/></param>
        /// <param name="query">A query which retrieves a value from a Selenium element</param>
        /// <param name="target">The target element or elements</param>
        /// <param name="summaryCreator">A function which creates a summary string for this predicate</param>
        /// <param name="multiElement">If true, then the target is expected to be a collection of elements</param>
        /// <exception cref="ArgumentNullException">If any nullable parameter is <see langword="null"/></exception>
        public QueryPredicatePrototype(ISpecificationFunction<TQueryable> specification,
                                       IQuery<TQueryable> query,
                                       ITarget target,
                                       Func<ITarget, string> summaryCreator,
                                       bool multiElement = false)
        {
            Specification = specification ?? throw new ArgumentNullException(nameof(specification));
            Query = query ?? throw new ArgumentNullException(nameof(query));
            Target = target ?? throw new ArgumentNullException(nameof(target));
            SummaryCreator = summaryCreator ?? throw new ArgumentNullException(nameof(summaryCreator));
            MultiElement = multiElement;
        }
    }
}