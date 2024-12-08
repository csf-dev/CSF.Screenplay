using System;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// A question which represents an actor taking a screenshot of the current web page.
    /// </summary>
    public class TakeScreenshot : IPerformableWithResult<Screenshot>, ICanReport
    {
        readonly bool throwIfUnsupported;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} takes a screenshot of the web page", actor.Name);

        /// <inheritdoc/>
        public ValueTask<Screenshot> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var webDriver = actor.GetAbility<BrowseTheWeb>().WebDriver;
            if(!(webDriver is ITakesScreenshot takesScreenshot))
            {
                return throwIfUnsupported
                    ? throw new InvalidOperationException($"The WebDriver must support taking screenshots.")
                    : new ValueTask<Screenshot>((Screenshot) null);
            }

            return new ValueTask<Screenshot>(takesScreenshot.GetScreenshot());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TakeScreenshot"/> class.
        /// </summary>
        /// <param name="throwIfUnsupported">If set to <c>true</c>, throws an exception if the WebDriver does not support taking screenshots.</param>
        public TakeScreenshot(bool throwIfUnsupported = true)
        {
            this.throwIfUnsupported = throwIfUnsupported;
        }
    }
}