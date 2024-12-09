using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// A performable action that waits until a specified condition is met, or a timeout expires (whichever is sooner).
    /// </summary>
    /// <typeparam name="T">The type of the result returned by the wait condition.</typeparam>
    public class Wait<T> : IPerformableWithResult<T>, ICanReport
    {
        readonly TimeSpan timeout;
        readonly TimeSpan? pollingInterval;
        readonly ICollection<Type> ignoredExceptionTypes;
        readonly WaitUntilPredicate<T> predicate;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} waits for at most {Timeout} or until {PredicateName}", actor, timeout, predicate);

        /// <inheritdoc/>
        public ValueTask<T> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var webDriver = actor.GetAbility<BrowseTheWeb>().WebDriver;

            var wait = new WebDriverWait(webDriver, timeout);
            if(pollingInterval.HasValue)
                wait.PollingInterval = pollingInterval.Value;
            if(ignoredExceptionTypes != null)
                wait.IgnoreExceptionTypes(ignoredExceptionTypes.ToArray());
            
            return new ValueTask<T>(wait.Until(predicate.Predicate, cancellationToken));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Wait{T}"/> class.
        /// </summary>
        /// <param name="timeout">The maximum amount of time to wait for the condition.</param>
        /// <param name="predicate">The predicate function to evaluate.</param>
        /// <param name="pollingInterval">An optional interval at which to poll the <paramref name="predicate"/>; the default is 500ms.</param>
        /// <param name="ignoredExceptionTypes">An optional collection of types of exceptions to ignore while waiting; the default is an empty collection.</param>
        public Wait(TimeSpan timeout, WaitUntilPredicate<T> predicate, TimeSpan? pollingInterval = null, ICollection<Type> ignoredExceptionTypes = null)
        {
            this.timeout = timeout;
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            this.pollingInterval = pollingInterval;
            this.ignoredExceptionTypes = ignoredExceptionTypes;
        }
    }
}