using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Elements
{
    /// <summary>
    /// Concrete implementation of <see cref="Locator"/> which locates elements by a CSS selector value.
    /// </summary>
    public class CssSelector : Locator
    {
        const string defaultNameFormat = "element(s) matching the selector '{0}'";

        /// <summary>
        /// Gets a CSS-based locator which matches all/any descendent elements.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This locator is equivalent to the CSS selector <c>*</c>, which matches any &amp; all descendent elements.
        /// </para>
        /// </remarks>
        public static readonly CssSelector AnyElement = new CssSelector("*", "any element");

        /// <summary>
        /// Gets a CSS-based locator which matches the HTML <c>body</c> element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This locator is equivalent to the CSS selector <c>body</c>, which matches the root of the rendered page.
        /// </para>
        /// </remarks>
        public static readonly CssSelector BodyElement = new CssSelector("body", "the page body");

        /// <inheritdoc/>
        public override By GetLocator() => By.CssSelector(Specification);

        /// <inheritdoc/>
        protected override string GetDefaultName() => string.Format(defaultNameFormat, Specification);

        /// <summary>
        /// Initializes a new instance of the <see cref="CssSelector"/> class.
        /// </summary>
        /// <param name="cssSelector">The CSS selector by which to locate elements.</param>
        /// <param name="name">An optional human-readable name of the locator which describes the elements it matches.</param>
        public CssSelector(string cssSelector, string name = null) : base(cssSelector, name) {}
    }

}