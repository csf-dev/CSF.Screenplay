using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// A <see cref="ISingleElementPerformable"/> which represents an actor deselecting an option from a <c>&lt;select&gt;</c> element,
    /// where the option is specified by its zero-based index within the available options.
    /// </summary>
    public class DeselectByIndex : ISingleElementPerformable
    {
        readonly int index;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} deselects option {Index} (zero-based) from {Element}", actor.Name, index, element.Value);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            element.Value.AsSelectElement().DeselectByIndex(index);
            return default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeselectByIndex"/> class with the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the option to deselect.</param>
        public DeselectByIndex(int index)
        {
            if(index < 0) throw new ArgumentOutOfRangeException(nameof(index), "The index must be zero or greater.");
            this.index = index;
        }
    }
}