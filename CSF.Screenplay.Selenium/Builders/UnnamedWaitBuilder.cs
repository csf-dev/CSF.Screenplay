using System;
using CSF.Screenplay.Selenium.Actions;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// Provides a builder for configuring wait actions in Selenium.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class provides methods for configuring various aspects of a wait action, such as timeout, polling interval,
    /// and ignored exception types. It also provides a way to set a human-readable name for the wait condition.
    /// </para>
    /// </remarks>
    /// <typeparam name="T">The return type of the predicate function.</typeparam>
    public class UnnamedWaitBuilder<T> : NamedWaitBuilder<T>
    {
        readonly Func<IWebDriver,T> predicate;
        string name;

        /// <inheritdoc/>
        protected override WaitUntilPredicate<T> GetWaitUntilPredicate()
            => new WaitUntilPredicate<T>(predicate, name ?? "a predicate is satisfied");

        /// <summary>
        /// Configures the wait action to use a specified maximum timeout.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the timeout is reached before the predicate function returns a <see langword="true"/> result then
        /// the wait will end, and an exception will be raised.
        /// </para>
        /// <para>
        /// This method is optional.  If it is not called then the default behaviour of the <see cref="Wait{T}"/> question
        /// will be used.  See the documentation for that class for more details.
        /// </para>
        /// </remarks>
        /// <param name="timeout">The maximum amount of time to wait.</param>
        /// <returns>The same wait builder, so that calls may be chained.</returns>
        public new UnnamedWaitBuilder<T> ForAtMost(TimeSpan timeout)
            => (UnnamedWaitBuilder<T>) base.ForAtMost(timeout);

        /// <summary>
        /// Configures the wait action which will be created to use a specified polling interval.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The polling interval is the amount of time that the WebDriver will wait between each evaluation of the predicate function.
        /// </para>
        /// <para>
        /// This method is optional.  If it is not called then Screenplay will not specify a polling interval.
        /// Selenium will then use its own default interval, which is 500ms.
        /// </para>
        /// <para>
        /// Choosing a polling interval is a balance between responsiveness and resource usage.
        /// A shorter polling interval may lead to quicker detection of the condition being met (a more responsive predicate),
        /// but at the cost of increased network usage as more round-trips are made to the WebDriver.
        /// Conversely, a longer polling interval reduces network traffic but may result in a less responsive predicate.
        /// If you are not sure what to choose, it's recommended not to use this method; use the default
        /// provided by Selenium.
        /// </para>
        /// </remarks>
        /// <param name="pollingInterval">The polling interval to use.</param>
        /// <returns>The same wait builder, so that calls may be chained.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the polling interval has already been set.</exception>
        public new UnnamedWaitBuilder<T> WithPollingInterval(TimeSpan pollingInterval)
            => (UnnamedWaitBuilder<T>) base.WithPollingInterval(pollingInterval);

        /// <summary>
        /// Configures the wait action which will be created to ignore exceptions of the specified types.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When evaluating the predicate/condition function, the WebDriver may throw exceptions, such as if an element doesn't exist.
        /// Use this method to specify a collection of exception types which should be ignored.  If an exception thrown of one of these
        /// types then the WebDriver wait will treat it the same as a <see langword="false" /> outcome and continue to poll the predicate.
        /// </para>
        /// <para>
        /// This method is optional.  If it is not called then no exception types will be ignored.
        /// </para>
        /// </remarks>
        /// <param name="ignoredExceptionTypes">A collection of exception types to be ignored when polling the predicate function.</param>
        /// <returns></returns>
        /// <returns>The same wait builder, so that calls may be chained.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the ignored exception types have already been set.</exception>
        public new UnnamedWaitBuilder<T> IgnoringTheseExceptionTypes(params Type[] ignoredExceptionTypes)
            => (UnnamedWaitBuilder<T>) base.IgnoringTheseExceptionTypes(ignoredExceptionTypes);

        /// <summary>
        /// Configures the wait action to use a short, descriptive, human-readable name summarising what the performable is waiting for.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use of this method is optional but strongly recommended. Giving the wait performable a name will make it easier to understand.
        /// </para>
        /// </remarks>
        /// <param name="name">The name to use.</param>
        /// <returns>The same wait builder, so that calls may be chained.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the name is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the name has already been set.</exception>
        public UnnamedWaitBuilder<T> Named(string name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));
            if(this.name != null)
                throw new InvalidOperationException("The name has already been set; it cannot be set again.");

            this.name = name;
            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnnamedWaitBuilder{T}"/> class with a specified timeout.
        /// </summary>
        /// <param name="predicate">The predicate which ends the wait when it returns a successful result.</param>
        public UnnamedWaitBuilder(Func<IWebDriver,T> predicate) : base()
        {
            var predType = typeof(T);
            if(predType.IsValueType && predType != typeof(bool))
                throw new ArgumentException("The result type of the predicate must be a boolean or a reference type.", nameof(predicate));
            
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }
    }
}
