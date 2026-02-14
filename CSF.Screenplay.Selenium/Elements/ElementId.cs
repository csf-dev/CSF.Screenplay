using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Elements
{
    /// <summary>
    /// Concrete implementation of <see cref="Locator"/> which locates an element by its <c>id</c> attribute.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class wraps Selenium's <see cref="By.Id(string)"/> functionality. The purpose is provide a type which derives
    /// from <see cref="ITarget"/>, compatible with the Selenium extension's performables. This class also provides opportunity
    /// to add a human-readable <see cref="Locator.Name"/> to the element specification. This optional, but recommended, technique
    /// improves the readability of Screenplay reports.
    /// </para>
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