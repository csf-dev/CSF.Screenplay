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
    /// A Screenplay action which pauses the Performance until either a specified condition is met or a timeout expires,
    /// whichever occurs sooner.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this action via either the builder method <see cref="PerformableBuilder.WaitUntil(Builders.IBuildsElementPredicates)"/>
    /// or <see cref="PerformableBuilder.WaitUntil(Func{IWebDriver, bool})"/>.  The former is preferred over the latter,
    /// as it permits the use of a fluent builder syntax to specify the behaviour of the wait.
    /// See the documentation of those builder methods to learn more about their use.
    /// </para>
    /// <para>
    /// This action forces the progress of the <see cref="IPerformance"/> to be delayed
    /// whilst the WebDriver waits for a condition to be true.  Internally, this action makes use of Selenium's
    /// <c>WebDriverWait</c> functionality to implement the waiting logic. Documentation for the WebDriver Wait
    /// functionality from the web may be useful if you would like a deeper understanding of this action.
    /// This action accepts a number of parameters.
    /// </para>
    /// <para>
    /// The <b>Predicate</b> is the condition for which we are waiting.  In other words, we are waiting until the Predicate
    /// is <see langword="true"/>.  This is expressed using a <see cref="WaitUntilPredicate{T}"/>, of <see cref="bool"/>.
    /// See <see cref="Elements.TargetExtensions"/> for extension methods which act as builders for a predicate:
    /// <see cref="Elements.TargetExtensions.Has(Elements.ITarget)"/> builds a predicate for a single element whereas
    /// <see cref="Elements.TargetExtensions.AllHave(Elements.ITarget)"/> builds a predicate for a collection of
    /// elements.
    /// </para>
    /// <para>
    /// The <b>Timeout</b> is the maximum time which this Action will wait. If the Predicate (above) is not true.
    /// If the timeout elapses before the Predicate is true then this action will throw
    /// <see cref="WebDriverTimeoutException"/>. In fact, it is the underlying Selenium WebDriver which will throw
    /// this exception; this action does not change that.
    /// It is impossible to specify an indefinite timeout.  The following rules of precedence are used to ensure that
    /// the timeout is always set:
    /// </para>
    /// <list type="number">
    /// <item><description>If a timeout was specified when constructing this instance, that value is used.</description></item>
    /// <item><description>If the <see cref="Actor"/> performing this question has the ability <see cref="UseADefaultWaitTime"/>,
    /// then the time specified by <see cref="UseADefaultWaitTime.WaitTime"/> is used.</description></item>
    /// <item><description>If neither of the above apply, a default timeout of 5 seconds is used.</description></item>
    /// </list>
    /// <para>
    /// The <b>Polling Interval</b> controls how often the WebDriver queries the web browser to determine if the
    /// Predicate (above) has been fulfilled.  Shorter polling intervals can lead to more responsive waits, the Performance
    /// is not left waiting longer than it needs.  The 'cost' of a shorter polling interval is increased traffic between
    /// the Screenplay and the WebDriver. This is particularly relevant when using a Remote WebDriver, as this translates
    /// to actual network/Internet traffic and latency.
    /// If unspecified, Selenium's default polling interval is used.  At the time of writing that is 500 milliseconds.
    /// </para>
    /// <para>
    /// Lastly, the <b>Ignored Exception Types</b> is a collection of <see cref="Type"/>. Each entry should be a type which
    /// derives from <see cref="Exception"/> which should be silently ignored whilst testing the Predicate (above).
    /// This can be useful to ignore errors which are raised because the Predicate isn't true yet, without needing
    /// expensive (and performance-impacting) defensive programming.  If this collection is specified then these exception
    /// types will be caught and silently ignored whilst evaluating the Predicate, treating an exception as "not true".
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// In this example, Screenplay instructs the WebDriver to wait for up to 2 seconds, or until no element
    /// with the class <c>loading_spinner</c> is visible on the web page.
    /// </para>
    /// <code>
    /// using CSF.Screenplay.Selenium.Elements;
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// readonly ITarget spinner = new ClassName("loading_spinner", "the loading spinner");
    /// 
    /// // Within the logic of a custom task, deriving from IPerformable
    /// public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     // ... other performance logic
    ///     await actor.PerformAsync(WaitUntil(spinner.Is().NotVisible().ForAtMost(TimeSpan.FromSeconds(2)), cancellationToken);
    ///     // ... other performance logic
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="PerformableBuilder.WaitUntil(Builders.IBuildsElementPredicates)"/>
    /// <seealso cref="PerformableBuilder.WaitUntil(Func{IWebDriver, bool})"/>
    /// <seealso cref="UseADefaultWaitTime"/>
    public class Wait : IPerformable, ICanReport
    {
        /// <summary>
        /// This default timeout of 5 seconds used when no timeout is specified and the actor has no <see cref="UseADefaultWaitTime"/> ability.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This, along with a few other performables in the Selenium extension for Screenplay involve waiting, with a timeout
        /// to prevent waiting indefinitely.
        /// If no timeout has been specified then this 5-second timeout is used as a fall-back default.
        /// This may be overridden by granting the <see cref="Actor"/> the ability <see cref="UseADefaultWaitTime"/>, with a different
        /// timeout specified.
        /// </para>
        /// </remarks>
        public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(5);

        readonly TimeSpan? timeout;
        readonly TimeSpan? pollingInterval;
        readonly ICollection<Type> ignoredExceptionTypes;
        readonly WaitUntilPredicate<bool> predicate;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} waits for at most {Timeout} or until {PredicateName}", actor, GetTimeout(actor), predicate);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var webDriver = actor.GetAbility<BrowseTheWeb>().WebDriver;

            var wait = new WebDriverWait(webDriver, GetTimeout(actor));
            if(pollingInterval.HasValue)
                wait.PollingInterval = pollingInterval.Value;
            if(ignoredExceptionTypes != null)
                wait.IgnoreExceptionTypes(ignoredExceptionTypes.ToArray());
            
            wait.Until(predicate.Predicate, cancellationToken);
            return default;
        }

        TimeSpan GetTimeout(ICanPerform actor)
        {
            if(timeout.HasValue)
                return timeout.Value;

            if(actor.TryGetAbility<UseADefaultWaitTime>(out var ability))
                return ability.WaitTime;

            return DefaultTimeout;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Wait"/> class.
        /// </summary>
        /// <param name="predicate">The predicate function to evaluate.</param>
        /// <param name="timeout">An optional maximum amount of time to wait for the condition; the default is determined by the
        /// presence of the <see cref="UseADefaultWaitTime"/> ability. See the documentation of this class for more details.</param>
        /// <param name="pollingInterval">An optional interval at which to poll the <paramref name="predicate"/>; the Selenium default is 500ms.</param>
        /// <param name="ignoredExceptionTypes">An optional collection of types of exceptions to ignore while waiting; the default is an empty collection.</param>
        public Wait(WaitUntilPredicate<bool> predicate, TimeSpan? timeout, TimeSpan? pollingInterval = null, ICollection<Type> ignoredExceptionTypes = null)
        {
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            this.timeout = timeout;
            this.pollingInterval = pollingInterval;
            this.ignoredExceptionTypes = ignoredExceptionTypes;
        }
    }
}