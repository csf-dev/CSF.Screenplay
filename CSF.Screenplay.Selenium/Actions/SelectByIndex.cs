using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// A <see cref="ISingleElementPerformable"/> which represents an actor selecting an option from a <c>&lt;select&gt;</c> element,
    /// where the option is specified by its zero-based index within the available options.
    /// </summary>
    public class SelectByIndex : ISingleElementPerformable
    {
        readonly int index;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} selects option {Index} (zero-based) from {Element}", actor.Name, index, element.Value);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            element.Value.AsSelectElement().SelectByIndex(index);
            return default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectByIndex"/> class with the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the option to select.</param>
        public SelectByIndex(int index)
        {
            if(index < 0) throw new ArgumentOutOfRangeException(nameof(index), "The index must be zero or greater.");
            this.index = index;
        }
    }
}