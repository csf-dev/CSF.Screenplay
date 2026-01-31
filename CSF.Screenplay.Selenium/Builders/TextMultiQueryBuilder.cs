using System.Collections.Generic;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Elements;
using CSF.Screenplay.Selenium.Queries;
using CSF.Screenplay.Selenium.Questions;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// Builder type for a performable that gets a collection of text values.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The purpose of this builder is to enable or disable the trimming of whitespace characters at the beginning or end of the returned text.
    /// </para>
    /// </remarks>
    public class TextMultiQueryBuilder : IGetsPerformableWithResult<IReadOnlyList<string>>
    {
        readonly ITarget elements;
        bool trimWhitespace = true;

        /// <summary>
        /// Configures the performable to disable the trimming of whitespace characters at the beginning or end of the returned text.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Trimming of whitespace at the beginning/end of text is enabled by default.  This is because some browsers (Safari)
        /// include whitespace at the beginning/end of text read from the browser, which isn't visible to the end user.  This is typically the 
        /// space which is inherent in the markup, but which browsers ignore when actually displaying content.
        /// </para>
        /// <para>
        /// Trimming it by default ensures that Screenplay reproduces functionality reliably cross-browser.
        /// If this causes an issue and you would like the leading/trailing whitespace included in the result then use this method.
        /// Note that you may see different results in browsers which include leading/trailing whitespace anyway.
        /// </para>
        /// </remarks>
        /// <returns>The current instance of the builder, so calls may be chained.</returns>
        public TextMultiQueryBuilder WithoutTrimmingWhitespace()
        {
            trimWhitespace = false;
            return this;
        }

        IPerformableWithResult<IReadOnlyList<string>> IGetsPerformableWithResult<IReadOnlyList<string>>.GetPerformable()
            => ElementCollectionPerformableWithResultAdapter.From(ElementCollectionQuery.From(new TextQuery(trimWhitespace)), elements);

        /// <summary>
        /// Initializes a new instance of the <see cref="TextMultiQueryBuilder"/> class.
        /// </summary>
        /// <param name="elements">The target elements to query.</param>
        public TextMultiQueryBuilder(ITarget elements)
        {
            this.elements = elements ?? throw new System.ArgumentNullException(nameof(elements));
        }
    }

    /// <summary>
    /// Builder type for a performable that gets a single text value.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The purpose of this builder is to enable or disable the trimming of whitespace characters at the beginning or end of the returned text.
    /// </para>
    /// </remarks>
    public class TextQueryBuilder : IGetsPerformableWithResult<string>
    {
        readonly ITarget element;
        bool trimWhitespace = true;

        /// <summary>
        /// Configures the performable to disable the trimming of whitespace characters at the beginning or end of the returned text.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Trimming of whitespace at the beginning/end of text is enabled by default.  This is because some browsers (Safari)
        /// include whitespace at the beginning/end of text read from the browser, which isn't visible to the end user.  This is typically the 
        /// space which is inherent in the markup, but which browsers ignore when actually displaying content.
        /// </para>
        /// <para>
        /// Trimming it by default ensures that Screenplay reproduces functionality reliably cross-browser.
        /// If this causes an issue and you would like the leading/trailing whitespace included in the result then use this method.
        /// Note that you may see different results in browsers which include leading/trailing whitespace anyway.
        /// </para>
        /// </remarks>
        /// <returns>The current instance of the builder, so calls may be chained.</returns>
        public TextQueryBuilder WithoutTrimmingWhitespace()
        {
            trimWhitespace = false;
            return this;
        }

        IPerformableWithResult<string> IGetsPerformableWithResult<string>.GetPerformable()
            => SingleElementPerformableWithResultAdapter.From(SingleElementQuery.From(new TextQuery(trimWhitespace)), element);

        /// <summary>
        /// Initializes a new instance of the <see cref="TextQueryBuilder"/> class.
        /// </summary>
        /// <param name="element">The target element to query.</param>
        public TextQueryBuilder(ITarget element)
        {
            this.element = element ?? throw new System.ArgumentNullException(nameof(element));
        }
    }
}