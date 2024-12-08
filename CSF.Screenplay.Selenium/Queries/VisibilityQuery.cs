using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium.Queries
{
    /// <summary>
    /// A query to get the visibility of a Selenium element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This query will have a value of <c>true</c> if the element is visible on the web page; <c>false</c> if it is not.
    /// </para>
    /// </remarks>
    public class VisibilityQuery : IQuery<bool>
    {
        /// <inheritdoc/>
        public string Name => $"whether the element is visible";

        /// <inheritdoc/>
        public bool GetValue(SeleniumElement element) => element.WebElement.Displayed;
    }
}
