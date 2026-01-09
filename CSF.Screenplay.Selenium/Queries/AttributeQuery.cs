using System;
using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium.Queries
{
    /// <summary>
    /// A query to get the value of a specified HTML attribute from a Selenium element.
    /// </summary>
    public class AttributeQuery : IQuery<string>
    {
        internal const string ClassAttribute = "class";

        readonly string attributeName;

        /// <inheritdoc/>
        public string Name => $"the value of the '{attributeName}' attribute";

        /// <inheritdoc/>
        public string GetValue(SeleniumElement element) => element.WebElement.GetAttribute(attributeName);

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeQuery"/> class with the specified attribute name.
        /// </summary>
        /// <param name="attributeName">The name of the HTML attribute to query.</param>
        public AttributeQuery(string attributeName)
        {
            if (string.IsNullOrWhiteSpace(attributeName))
                throw new ArgumentException($"'{nameof(attributeName)}' cannot be null or whitespace.", nameof(attributeName));

            this.attributeName = attributeName;
        }
    }
}