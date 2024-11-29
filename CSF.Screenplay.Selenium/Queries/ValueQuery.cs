using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium.Queries
{
    /// <summary>
    /// A query to get the value of the <c>value</c> attribute of a Selenium element.
    /// </summary>
    public class ValueQuery : IQuery<string>
    {
        internal const string ValueAttribute = "value";

        /// <inheritdoc/>
        public string Name => $"the element's value";

        /// <inheritdoc/>
        public string GetValue(SeleniumElement element) => element.WebElement.GetAttribute(ValueAttribute);
    }
}
