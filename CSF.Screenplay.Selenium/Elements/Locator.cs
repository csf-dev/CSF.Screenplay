using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Elements
{
    /// <summary>
    /// Base type for implementations of <see cref="ITarget"/> which serve as a specification for Selenium elements
    /// but which do not reference those elements directly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// All derived types of this class are Screenplay wrappers (adapters) for the Selenium <see cref="By"/> class.
    /// The 'By' class is a specification of sorts, describing how a WebDriver may find an element on a page.
    /// Implementations of this type provide that specification, but additionally a <see cref="Name"/>, providing
    /// human-readable content in Screenplay reports.
    /// </para>
    /// </remarks>
    public abstract class Locator : ITarget
    {
        readonly string name;

        /// <summary>
        /// Gets the locator specification string.
        /// </summary>
        protected string Specification { get; }

        /// <inheritdoc/>
        public string Name => name ?? GetDefaultName();

        /// <inheritdoc/>
        public SeleniumElement GetElement(IWebDriver driver)
        {
            if (driver is null)
                throw new ArgumentNullException(nameof(driver));

            var element = GetSingleElement(driver, GetLocator());
            return new SeleniumElement(element, Name);
        }

        /// <inheritdoc/>
        public SeleniumElementCollection GetElements(IWebDriver driver)
        {
            if (driver is null)
                throw new ArgumentNullException(nameof(driver));

            var elements = driver.FindElements(GetLocator());
            return new SeleniumElementCollection(elements, Name);
        }

        IWebElement GetSingleElement(IWebDriver driver, By locator)
        {
            try
            {
                return driver.FindElement(locator);
            }
            catch(NoSuchElementException ex)
            {
                throw new TargetNotFoundException("An element matching the specified locator was not found.", ex, this);
            }
        }

        /// <summary>
        /// Gets the Selenium locator instance for finding HTML elements.
        /// </summary>
        /// <returns>A Selenium <c>By</c> object used to locate elements.</returns>
        public abstract By GetLocator();

        /// <summary>
        /// Gets a value for <see cref="Name"/> if none has been specified.
        /// </summary>
        /// <returns>A default name</returns>
        protected abstract string GetDefaultName();

        /// <summary>
        /// Initializes a new instance of the <see cref="Locator"/> class.
        /// </summary>
        /// <param name="specification">The locator specification string.</param>
        /// <param name="name">An optional human-readable name of the locator which describes the elements it matches.</param>
        protected Locator(string specification, string name = null)
        {
            if (string.IsNullOrWhiteSpace(specification))
                throw new ArgumentException($"'{nameof(specification)}' cannot be null or whitespace.", nameof(specification));

            Specification = specification;
            this.name = name;
        }

        /// <summary>
        /// Implicitly converts a <see cref="Locator"/> to a Selenium <see cref="By"/> object.
        /// </summary>
        /// <param name="locator">The locator to convert.</param>
        /// <returns>A Selenium <see cref="By"/> object.</returns>
        public static implicit operator By(Locator locator) => locator.GetLocator();
    }
}