using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium.Queries
{
    /// <summary>
    /// Represents a query to determine if a Selenium element is clickable.
    /// </summary>
    public class ClickableQuery : IQuery<bool>
    {
        /// <inheritdoc/>
        public string Name => "whether the element is clickable";

        /// <inheritdoc/>
        public bool GetValue(SeleniumElement element) => element.WebElement.Displayed && element.WebElement.Enabled;
    }
}