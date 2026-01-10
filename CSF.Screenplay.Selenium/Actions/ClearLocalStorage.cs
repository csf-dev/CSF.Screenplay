using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Performables;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// Represents an action to clear the browser's local storage for the current site.
    /// </summary>
    public class ClearLocalStorage : IPerformable, ICanReport
    {
        readonly bool throwIfUnsupported;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} clears their browser local storage for the current site", actor.Name);

        /// <inheritdoc/>
        public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var script = Resources.Scripts.ClearLocalStorage;
            try
            {
                await actor.PerformAsync(ExecuteSomeJavaScript(script).WithTheName("clear local storage"));
            }
            catch(PerformableException e)
            {
                if(e.InnerException is NotSupportedException && !throwIfUnsupported) return;
                throw;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClearLocalStorage"/> class.
        /// </summary>
        /// <param name="throwIfUnsupported">If set to <c>true</c>, an exception will be thrown if the WebDriver does not support the execution of JavaScript.</param>
        public ClearLocalStorage(bool throwIfUnsupported = true)
        {
            this.throwIfUnsupported = throwIfUnsupported;
        }
    }
}