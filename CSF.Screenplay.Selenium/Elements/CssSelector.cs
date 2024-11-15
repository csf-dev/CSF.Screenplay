using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Elements
{
    /// <summary>
    /// Concrete implementation of <see cref="Locator"/> which locates elements by a CSS selector value.
    /// </summary>
    public class CssSelector : Locator
    {
        const string defaultNameFormat = "element(s) matching the selector '{0}'";

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