using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Elements
{
    /// <summary>
    /// An implementation of <see cref="ITarget"/> which represents a single native Selenium HTML element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This type of <see cref="ITarget"/> contains a reference to a native Selenium <see cref="IWebElement"/>.
    /// This class provides that element under the ITarget interface, as well as allowing the developer to specify
    /// a <see cref="Name"/>.  This optional, but recommended, technique facilitates human-readable reporting.
    /// </para>
    /// </remarks>
    public class SeleniumElement : ITarget, IHasWebElement, IHasSearchContext
    {
        const string unknownNameFormat = "an HTML {0} element";

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public IWebElement WebElement { get; }

        ISearchContext IHasSearchContext.SearchContext => WebElement;

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

    /// <summary>
    /// An object which exposes a Selenium web element.
    /// </summary>
    public interface IHasWebElement : IHasName
    {
        /// <summary>
        /// Gets the native Selenium web element.
        /// </summary>
        IWebElement WebElement { get; }
    }

    /// <summary>
    /// An object which exposes a Selenium search context.
    /// </summary>
    public interface IHasSearchContext : IHasName
    {
        /// <summary>
        /// Gets the native Selenium search context.
        /// </summary>
        ISearchContext SearchContext { get; }
    }
}