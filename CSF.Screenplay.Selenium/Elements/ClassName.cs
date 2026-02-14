using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Elements
{
    /// <summary>
    /// Concrete implementation of <see cref="Locator"/> which locates elements by their HTML <c>class</c> attribute value.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class wraps Selenium's <see cref="By.ClassName(string)"/> functionality. The purpose is provide a type which derives
    /// from <see cref="ITarget"/>, compatible with the Selenium extension's performables. This class also provides opportunity
    /// to add a human-readable <see cref="Locator.Name"/> to the element specification. This optional, but recommended, technique
    /// improves the readability of Screenplay reports.
    /// </para>
    /// </remarks>
    public class ClassName : Locator
    {
        const string defaultNameFormat = "element(s) with the class '{0}'";

        /// <inheritdoc/>
        public override By GetLocator() => By.ClassName(Specification);

        /// <inheritdoc/>
        protected override string GetDefaultName() => string.Format(defaultNameFormat, Specification);

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassName"/> class.
        /// </summary>
        /// <param name="className">The class attribute value by which to locate elements.</param>
        /// <param name="name">An optional human-readable name of the locator which describes the elements it matches.</param>
        public ClassName(string className, string name = null) : base(className, name) {}
    }

}