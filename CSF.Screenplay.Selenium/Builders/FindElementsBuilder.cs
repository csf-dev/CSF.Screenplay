using CSF.Screenplay.Selenium.Elements;
using CSF.Screenplay.Selenium.Questions;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// A builder class for finding elements within a specified target.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This builder offers two methods to configure the finding of elements:
    /// </para>
    /// <list type="bullet">
    /// <item><description><see cref="WhichMatch(Locator)"/> - Specify a <see cref="Locator"/> which should be used to filter the elements which are found.</description></item>
    /// <item><description><see cref="AndNameThem(string)"/> - Specify a human-readable name for the collection of elements which are found.</description></item>
    /// </list>
    /// </remarks>
    public class FindElementsBuilder
    {
        readonly ITarget target;
        Locator locator;
        string name;

        /// <summary>
        /// Specifies a <see cref="Locator"/> which should be used to filter the elements which are found.
        /// </summary>
        /// <param name="locator">The locator to filter the elements.</param>
        /// <returns>The current instance of <see cref="FindElementsBuilder"/>.</returns>
        public FindElementsBuilder WhichMatch(Locator locator)
        {
            this.locator = locator ?? throw new System.ArgumentNullException(nameof(locator));
            return this;
        }

        /// <summary>
        /// Specifies a human-readable name for the collection of elements which are found.
        /// </summary>
        /// <param name="name">The name for the collection of elements.</param>
        /// <returns>The current instance of <see cref="FindElementsBuilder"/>.</returns>
        public FindElementsBuilder AndNameThem(string name)
        {
            this.name = name ?? throw new System.ArgumentNullException(nameof(name));
            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindElementsBuilder"/> class with the specified target.
        /// </summary>
        /// <param name="target">The target within which elements will be found.</param>
        public FindElementsBuilder(ITarget target)
        {
            this.target = target ?? throw new System.ArgumentNullException(nameof(target));
        }

        /// <summary>
        /// Converts a <see cref="FindElementsBuilder"/> to a <see cref="SingleElementPerformableWithResultAdapter{SeleniumElementCollection}"/>.
        /// </summary>
        /// <param name="builder">The <see cref="FindElementsBuilder"/> instance to convert.</param>
        /// <returns>A <see cref="SingleElementPerformableWithResultAdapter{SeleniumElementCollection}"/> instance.</returns>
        public static implicit operator SingleElementPerformableWithResultAdapter<SeleniumElementCollection>(FindElementsBuilder builder)
        {
            return SingleElementPerformableWithResultAdapter.From(new FindElements(builder.name, builder.locator), builder.target);
        }
    }

    /// <summary>
    /// A builder class for finding a single element within a specified target.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This builder offers two methods to configure the finding of elements:
    /// </para>
    /// <list type="bullet">
    /// <item><description><see cref="WhichMatch(Locator)"/> - Specify a <see cref="Locator"/> which should be used to filter for the element.</description></item>
    /// <item><description><see cref="AndNameThem(string)"/> - Specify a human-readable name for the element which is found.</description></item>
    /// </list>
    /// </remarks>
    public class FindElementBuilder
    {
        readonly ITarget target;
        Locator locator;
        string name;

        /// <summary>
        /// Specifies a <see cref="Locator"/> which should be used to filter the elements which are found.
        /// </summary>
        /// <param name="locator">The locator to filter the elements.</param>
        /// <returns>The current instance of <see cref="FindElementsBuilder"/>.</returns>
        public FindElementBuilder WhichMatch(Locator locator)
        {
            this.locator = locator ?? throw new System.ArgumentNullException(nameof(locator));
            return this;
        }

        /// <summary>
        /// Specifies a human-readable name for the collection of elements which are found.
        /// </summary>
        /// <param name="name">The name for the collection of elements.</param>
        /// <returns>The current instance of <see cref="FindElementsBuilder"/>.</returns>
        public FindElementBuilder AndNameThem(string name)
        {
            this.name = name ?? throw new System.ArgumentNullException(nameof(name));
            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindElementsBuilder"/> class with the specified target.
        /// </summary>
        /// <param name="target">The target within which elements will be found.</param>
        public FindElementBuilder(ITarget target)
        {
            this.target = target ?? throw new System.ArgumentNullException(nameof(target));
        }

        /// <summary>
        /// Converts a <see cref="FindElementsBuilder"/> to a <see cref="SingleElementPerformableWithResultAdapter{SeleniumElement}"/>.
        /// </summary>
        /// <param name="builder">The <see cref="FindElementBuilder"/> instance to convert.</param>
        /// <returns>A <see cref="SingleElementPerformableWithResultAdapter{SeleniumElement}"/> instance.</returns>
        public static implicit operator SingleElementPerformableWithResultAdapter<SeleniumElement>(FindElementBuilder builder)
        {
            return SingleElementPerformableWithResultAdapter.From(new FindElement(builder.name, builder.locator), builder.target);
        }
    }
}