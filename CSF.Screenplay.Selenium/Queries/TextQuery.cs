using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium.Queries
{
    /// <summary>
    /// A query to get the text content of a Selenium element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When reading text from the web browser, depending upon the whitespace-trimming parameter, this
    /// query will or will not trim leading/trailing whitespace.
    /// This functionality is present because some browsers (Safari) include whitespace at the beginning/end
    /// of text read from the browser which isn't visible to the end user.  This is typically the 
    /// space which is inherent in the markup, but which browsers ignore when actually displaying content.
    /// Other WebDriver implementations do not include this leading/trailing whitespace.
    /// </para>
    /// <para>
    /// When whitespace is trimmed, this provides a more consistent cross-browser experience when reading
    /// the text of an element. The consequence is that sometimes, when reading leading/trailing whitespace
    /// is intended, we must pass a different parameter value.
    /// </para>
    /// </remarks>
    /// <seealso cref="Builders.QuestionQueryBuilder.TheText"/>
    /// <seealso cref="Builders.QuestionQueryBuilder.TheTextWithoutTrimmingWhitespace"/>
    /// <seealso cref="Builders.QueryPredicatePrototypeBuilder.Text(string)"/>
    /// <seealso cref="Builders.QueryPredicatePrototypeBuilder.Text(System.Func{string, bool})"/>
    /// <seealso cref="Builders.QueryPredicatePrototypeBuilder.TextWithoutTrimmingWhitespace(string)"/>
    /// <seealso cref="Builders.QueryPredicatePrototypeBuilder.TextWithoutTrimmingWhitespace(System.Func{string, bool})"/>
    public class TextQuery : IQuery<string>
    {
        readonly bool trimWhitespace;

        /// <inheritdoc/>
        public string Name => $"the contained text";

        /// <inheritdoc/>
        public string GetValue(SeleniumElement element)
        {
            var text = element.WebElement.Text;
            return trimWhitespace ? text?.Trim() : text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextQuery"/> class.
        /// </summary>
        /// <param name="trimWhitespace">If <see langword="true"/>, leading &amp; trailing whitespace will be trimmed from
        /// the returned text content; if <see langword="true"/> it will not.</param>
        public TextQuery(bool trimWhitespace = true)
        {
            this.trimWhitespace = trimWhitespace;
        }
    }
}
