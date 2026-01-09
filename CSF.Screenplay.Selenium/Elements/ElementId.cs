using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Elements
{
    /// <summary>
    /// Concrete implementation of <see cref="Locator"/> which locates an element by its <c>id</c> attribute.
    /// </summary>
    /// <remarks>
    /// <para>
    /// In normal circumstances (with valid HTML) this locator should match a maximum of only one element.
    /// </para>
    /// </remarks>
    public class ElementId : Locator
    {
        const string defaultNameFormat = "element with id '{0}'";

        /// <inheritdoc/>
        public override By GetLocator() => By.Id(Specification);

        /// <inheritdoc/>
        protected override string GetDefaultName() => string.Format(defaultNameFormat, Specification);

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementId"/> class.
        /// </summary>
        /// <param name="id">The id by which to locate an element.</param>
        /// <param name="name">An optional human-readable name of the locator which describes the element it matches.</param>
        public ElementId(string id, string name = null) : base(id, name) {}
    }

}