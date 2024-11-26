using System;
using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium.Queries
{
    /// <summary>
    /// A query to get the value of a specified CSS property from a Selenium element.
    /// </summary>
    public class CssPropertyQuery : IQuery<string>
    {
        readonly string propertyName;

        /// <inheritdoc/>
        public string GetValue(SeleniumElement element) => element.WebElement.GetCssValue(propertyName);

        /// <summary>
        /// Initializes a new instance of the <see cref="CssPropertyQuery"/> class with the specified property name.
        /// </summary>
        /// <param name="propertyName">The name of the CSS property to query.</param>
        public CssPropertyQuery(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentException($"'{nameof(propertyName)}' cannot be null or whitespace.", nameof(propertyName));

            this.propertyName = propertyName;
        }
    }
}