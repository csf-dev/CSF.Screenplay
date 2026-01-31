using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium.Queries
{
    /// <summary>
    /// A query to get the text content of a Selenium element.
    /// </summary>
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
