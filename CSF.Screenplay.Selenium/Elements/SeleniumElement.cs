using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Elements
{
    /// <summary>
    /// An implementation of <see cref="ITarget"/> which represents a single native Selenium HTML element.
    /// </summary>
    public class SeleniumElement : ITarget, IHasName
    {
        const string unknownNameFormat = "an HTML {0} element";

        /// <inheritdoc/>
        public string Name { get; }

        /// <summary>
        /// Gets the native Selenium web element.
        /// </summary>
        public IWebElement WebElement { get; }

        SeleniumElementCollection ITarget.GetElements(IWebDriver driver) => new SeleniumElementCollection(new[] { this }, Name);

        SeleniumElement ITarget.GetElement(IWebDriver driver) => this;

        /// <inheritdoc/>
        public override string ToString() => Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumElement"/> class.
        /// </summary>
        /// <param name="webElement">The native Selenium web element.</param>
        /// <param name="name">An optional human-readable name which describes the element.</param>
        public SeleniumElement(IWebElement webElement, string name = null)
        {
            WebElement = webElement ?? throw new System.ArgumentNullException(nameof(webElement));
            Name = name ?? string.Format(unknownNameFormat, webElement.TagName);
        }
    }
}