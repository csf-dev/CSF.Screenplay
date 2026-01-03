using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// A performable action that waits until a specified condition is met, or a timeout expires (whichever is sooner).
    /// </summary>
    /// <remarks>
    /// <para>
    /// This action makes use of Selenium's <see cref="WebDriverWait"/> class to implement the waiting logic.
    /// The maximum time (the timeout) for which to wait is determined by the following rules of precedence:
    /// </para>
    /// <list type="number">
    /// <item><description>If a timeout was specified when constructing this instance, that value is used.</description></item>
    /// <item><description>If the <see cref="Actor"/> performing this question has the ability <see cref="UseADefaultWaitTime"/>, then the
    /// time specified by <see cref="UseADefaultWaitTime.WaitTime"/> is used.</description></item>
    /// <item><description>If neither of the above apply, a default timeout of 5 seconds is used.</description></item>
    /// </list>
    /// </remarks>
    /// <typeparam name="T">The type of the result returned by the wait condition.</typeparam>
    public class Wait<T> : IPerformableWithResult<T>, ICanReport
    {
        static readonly TimeSpan defaultTimeout = TimeSpan.FromSeconds(5);

        readonly TimeSpan? timeout;
        readonly TimeSpan? pollingInterval;
        readonly ICollection<Type> ignoredExceptionTypes;
        readonly WaitUntilPredicate<T> predicate;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} waits for at most {Timeout} or until {PredicateName}", actor, GetTimeout(actor), predicate);

        /// <inheritdoc/>
        public ValueTask<T> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var webDriver = actor.GetAbility<BrowseTheWeb>().WebDriver;

            var wait = new WebDriverWait(webDriver, GetTimeout(actor));
            if(pollingInterval.HasValue)
                wait.PollingInterval = pollingInterval.Value;
            if(ignoredExceptionTypes != null)
                wait.IgnoreExceptionTypes(ignoredExceptionTypes.ToArray());
            
            return new ValueTask<T>(wait.Until(predicate.Predicate, cancellationToken));
        }

        TimeSpan GetTimeout(ICanPerform actor)
        {
            if(timeout.HasValue)
                return timeout.Value;

            if(actor.TryGetAbility<UseADefaultWaitTime>(out var ability))
                return ability.WaitTime;

            return defaultTimeout;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Wait{T}"/> class.
        /// </summary>
        /// <param name="predicate">The predicate function to evaluate.</param>
        /// <param name="timeout">An optional maximum amount of time to wait for the condition; the default is determined by the
        /// presence of the <see cref="UseADefaultWaitTime"/> ability. See the documentation of this class for more details.</param>
        /// <param name="pollingInterval">An optional interval at which to poll the <paramref name="predicate"/>; the Selenium default is 500ms.</param>
        /// <param name="ignoredExceptionTypes">An optional collection of types of exceptions to ignore while waiting; the default is an empty collection.</param>
        public Wait(WaitUntilPredicate<T> predicate, TimeSpan? timeout, TimeSpan? pollingInterval = null, ICollection<Type> ignoredExceptionTypes = null)
        {
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            this.timeout = timeout;
            this.pollingInterval = pollingInterval;
            this.ignoredExceptionTypes = ignoredExceptionTypes;
        }
    }
}