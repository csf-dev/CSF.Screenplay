using System;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Html5;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// Represents an action to clear the browser's local storage for the current site.
    /// </summary>
    public class ClearLocalStorage : IPerformable, ICanReport
    {
        readonly bool throwIfUnsupported;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} clears their browser local storage for the current site", actor.Name);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var browseTheWeb = actor.GetAbility<BrowseTheWeb>();
            var driver = browseTheWeb.WebDriver;

            if(!(driver is IHasWebStorage hasStorageDriver) || !hasStorageDriver.HasWebStorage)
            {
                return throwIfUnsupported
                    ? throw new InvalidOperationException("The WebDriver must support HTML5 Web Storage")
                    : new ValueTask();
            }

            hasStorageDriver.WebStorage.LocalStorage.Clear();
            return default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClearLocalStorage"/> class.
        /// </summary>
        /// <param name="throwIfUnsupported">If set to <c>true</c>, an exception will be thrown if the WebDriver does not support HTML5 Web Storage.</param>
        public ClearLocalStorage(bool throwIfUnsupported = true)
        {
            this.throwIfUnsupported = throwIfUnsupported;
        }
    }
}