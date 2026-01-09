using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium.Queries
{
    /// <summary>
    /// A query to get the text content of a Selenium element.
    /// </summary>
    public class TextQuery : IQuery<string>
    {
        /// <inheritdoc/>
        public string Name => $"the contained text";

        /// <inheritdoc/>
        public string GetValue(SeleniumElement element) => element.WebElement.Text;
    }
}
