using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Elements;
using CSF.Screenplay.Selenium.Questions;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// A builder class for finding a collection of elements within a specified target.
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
    public class FindElementsBuilder : IGetsPerformableWithResult<SeleniumElementCollection>
    {
        readonly ITarget target;
        readonly IHasSearchContext searchContext;
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

        IPerformableWithResult<SeleniumElementCollection> IGetsPerformableWithResult<SeleniumElementCollection>.GetPerformable()
        {
            return target != null
                ? new FindElements(target, name, locator)
                : new FindElements(searchContext, name, locator);
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
        /// Initializes a new instance of the <see cref="FindElementsBuilder"/> class with the specified target.
        /// </summary>
        /// <param name="searchContext">The target within which elements will be found.</param>
        public FindElementsBuilder(IHasSearchContext searchContext)
        {
            this.searchContext = searchContext ?? throw new System.ArgumentNullException(nameof(searchContext));
        }
    }
}