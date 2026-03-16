using System;
using System.Threading;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Elements;
using CSF.Screenplay.Selenium.Queries;
using CSF.Screenplay.Selenium.Questions;
using CSF.Screenplay.Selenium.Tasks;

namespace CSF.Screenplay.Selenium
{
    public static partial class PerformableBuilder
    {
        /// <summary>
        /// Gets a builder which may be used to create a performable action which finds a collection of elements within a specified target.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you only want to find elements within the <c>&lt;body&gt;</c> element of the page, consider using <see cref="FindElementsOnThePage"/>
        /// instead.
        /// </para>
        /// </remarks>
        /// <param name="target">The target within which to find HTML elements</param>
        /// <returns>A builder, which may be used to configure/get a question that finds elements</returns>
        public static FindElementsBuilder FindElementsWithin(Locator target) => new FindElementsBuilder(target);

        /// <summary>
        /// Gets a builder which may be used to create a performable action which finds a collection of elements within a specified target.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you only want to find elements within the <c>&lt;body&gt;</c> element of the page, consider using <see cref="FindElementsOnThePage"/>
        /// instead.
        /// </para>
        /// </remarks>
        /// <param name="target">The target within which to find HTML elements</param>
        /// <returns>A builder, which may be used to configure/get a question that finds elements</returns>
        public static FindElementsBuilder FindElementsWithin(IHasSearchContext target) => new FindElementsBuilder(target);

        /// <summary>
        /// Gets a builder which may be used to create a performable action which finds a collection of elements within the body of the page.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you want to find elements which are descendents of a specified target, consider using <see cref="FindElementsWithin(Locator)"/>
        /// or <see cref="FindElementsWithin(IHasSearchContext)"/> instead.
        /// </para>
        /// </remarks>
        /// <returns>A builder, which may be used to configure/get a question that finds elements</returns>
        public static FindElementsBuilder FindElementsOnThePage() => new FindElementsBuilder(CssSelector.BodyElement);

        /// <summary>
        /// Gets a builder which may be used to create a performable action which finds a single element within a specified target.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you only want to find an element within the <c>&lt;body&gt;</c> element of the page, consider using <see cref="FindAnElementOnThePage"/>
        /// instead.
        /// </para>
        /// </remarks>
        /// <param name="target">The target within which to find HTML elements</param>
        /// <returns>A builder, which may be used to configure/get a question that finds an element</returns>
        public static FindElementBuilder FindAnElementWithin(Locator target) => new FindElementBuilder(target);

        /// <summary>
        /// Gets a builder which may be used to create a performable action which finds a single element within a specified target.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you only want to find an element within the <c>&lt;body&gt;</c> element of the page, consider using <see cref="FindAnElementOnThePage"/>
        /// instead.
        /// </para>
        /// </remarks>
        /// <param name="target">The target within which to find HTML elements</param>
        /// <returns>A builder, which may be used to configure/get a question that finds an element</returns>
        public static FindElementBuilder FindAnElementWithin(IHasSearchContext target) => new FindElementBuilder(target);

        /// <summary>
        /// Gets a builder which may be used to create a performable action which finds a single element within the body of the page.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you want to find an element which is a descendent of a specified target, consider using <see cref="FindAnElementWithin(Locator)"/>
        /// or <see cref="FindAnElementWithin(IHasSearchContext)"/> instead.
        /// </para>
        /// </remarks>
        /// <returns>A builder, which may be used to configure/get a question that finds an element</returns>
        public static FindElementBuilder FindAnElementOnThePage() => new FindElementBuilder(CssSelector.BodyElement);

        /// <summary>
        /// Gets a builder which may be used to create a performable question which filters a collection of elements for those which match a specification.
        /// </summary>
        /// <example>
        /// <para>
        /// Here is a sample usage which combines both the <see cref="QueryPredicatePrototypeBuilder"/> and <see cref="FilterElementsBuilder"/> classes:
        /// </para>
        /// <code>
        /// using static CSF.Screenplay.Selenium.PerformableBuilder;
        /// 
        /// var buyNowButtons = await actor.PerformAsync(Filter(someElements).ForThoseWhich(have => have.Clickability(true).And(have.Text("Buy now")), cancellationToken);
        /// </code>
        /// <para>
        /// The code sample above assumes that <c>actor</c> is an instance of <see cref="ICanPerform"/>, that <c>someElements</c> is a collection
        /// of <see cref="SeleniumElement"/> instances, and that <c>cancellationToken</c> is a <see cref="CancellationToken"/> instance.
        /// It would filter the elements in <c>someElements</c> to only those which are clickable and have the text "Buy now".
        /// </para>
        /// </example>
        /// <param name="elements">The collection of elements to filter.</param>
        /// <returns>A builder with which consuming logic must provide a specification.</returns>
        /// <seealso cref="FilterElementsBuilder"/>
        /// <seealso cref="QueryPredicatePrototypeBuilder"/>
        public static FilterElementsBuilder Filter(SeleniumElementCollection elements)
            => new FilterElementsBuilder(elements);

        /// <summary>
        /// Gets a builder which may be used to create a performable question which reads a piece of information from a single element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This question makes use of an <see cref="IQuery{TResult}"/> to interrogate a single element and return a value.
        /// </para>
        /// </remarks>
        /// <param name="element">The element to interrogate for a value.</param>
        /// <returns>A builder which chooses the query</returns>
        public static QuestionQueryBuilder ReadFromTheElement(ITarget element) => new QuestionQueryBuilder(element);

        /// <summary>
        /// Gets a builder which may be used to create a performable question which reads a collection of the same information from a collection of elements.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This question makes use of an <see cref="IQuery{TResult}"/> to interrogate each element element in the collection and return a series of
        /// corresponding values.
        /// </para>
        /// </remarks>
        /// <param name="element">The elements to interrogate for values.</param>
        /// <returns>A builder which chooses the query</returns>
        public static QuestionMultiQueryBuilder ReadFromTheCollectionOfElements(ITarget element) => new QuestionMultiQueryBuilder(element);

        /// <summary>
        /// Gets a performable task/question which gets a Shadow Root from the specified Shadow Host target.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is used when working with web pages which use
        /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/Web_components/Using_shadow_DOM">The Shadow DOM technique</see>.
        /// This question allows Screenplay to 'pierce' the Shadow DOM and get the Shadow Root element, so that the Performance
        /// may continue and interact with elements which are inside the Shadow DOM.
        /// </para>
        /// <para>
        /// The <see cref="ITarget"/> passed to this performable as a parameter must be the Shadow Host element, or else this question will
        /// throw.
        /// </para>
        /// <para>
        /// This technique is supported only by recent Chromium and Firefox versions, and not by Safari.
        /// Use <see cref="GetTheShadowRootFrom(ITarget)"/> in order to automatically select the best technique for the current web browser.
        /// </para>
        /// </remarks>
        /// <param name="shadowHost">The Shadow Host element, or a locator which identifies it</param>
        /// <returns>A performable which gets the Shadow Root.</returns>
        public static IPerformableWithResult<ShadowRoot> GetTheShadowRootNativelyFrom(ITarget shadowHost)
            => SingleElementPerformableWithResultAdapter.From(new GetShadowRootNatively(), shadowHost);

        /// <summary>
        /// Gets a performable task/question which gets a Shadow Root from the specified Shadow Host target.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is used when working with web pages which use
        /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/Web_components/Using_shadow_DOM">The Shadow DOM technique</see>.
        /// This question allows Screenplay to 'pierce' the Shadow DOM and get the Shadow Root element, so that the Performance
        /// may continue and interact with elements which are inside the Shadow DOM.
        /// </para>
        /// <para>
        /// The <see cref="ITarget"/> passed to this performable as a parameter must be the Shadow Host element, or else this question will
        /// throw.
        /// </para>
        /// <para>
        /// This technique is supported only by older Chromium versions and Safari.
        /// Use <see cref="GetTheShadowRootFrom(ITarget)"/> in order to automatically select the best technique for the current web browser.
        /// </para>
        /// </remarks>
        /// <param name="shadowHost">The Shadow Host element, or a locator which identifies it</param>
        /// <returns>A performable which gets the Shadow Root.</returns>
        public static IPerformableWithResult<ShadowRoot> GetTheShadowRootWithJavaScriptFrom(ITarget shadowHost)
            => SingleElementPerformableWithResultAdapter.From(new GetShadowRootWithJavaScript(), shadowHost);

        /// <summary>
        /// Gets a performable task/question which gets a Shadow Root from the specified Shadow Host target.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is used when working with web pages which use
        /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/Web_components/Using_shadow_DOM">The Shadow DOM technique</see>.
        /// This question allows Screenplay to 'pierce' the Shadow DOM and get the Shadow Root element, so that the Performance
        /// may continue and interact with elements which are inside the Shadow DOM.
        /// </para>
        /// <para>
        /// The <see cref="ITarget"/> passed to this performable as a parameter must be the Shadow Host element, or else this question will
        /// throw.
        /// </para>
        /// <para>
        /// Use this method to automatically select the best technique to use for the current web browser.
        /// This functionality is unavailable for Firefox versions 112 and below, which do not support piercing the Shadow DOM from
        /// Selenium.
        /// </para>
        /// </remarks>
        /// <param name="shadowHost">The Shadow Host element, or a locator which identifies it</param>
        /// <returns>A performable which gets the Shadow Root.</returns>
        public static IPerformableWithResult<ShadowRoot> GetTheShadowRootFrom(ITarget shadowHost)
            => new GetShadowRoot(shadowHost);
    }
}