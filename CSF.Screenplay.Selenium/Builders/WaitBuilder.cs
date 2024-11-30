using System;
using System.Collections.Generic;
using CSF.Screenplay.Selenium.Actions;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// Provides a builder for configuring wait actions in Selenium.
    /// </summary>
    public class WaitBuilder<T>
    {
        readonly Func<IWebDriver,T> predicate;
        TimeSpan timeout = TimeSpan.FromMilliseconds(500);
        TimeSpan? pollingInterval;
        ICollection<Type> ignoredExceptionTypes = new List<Type>();
        string name;

        /// <summary>
        /// Configures the wait action to use a specified maximum timeout.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the timeout is reached before the predicate function returns a successful result then the wait will end and an exception
        /// will be raised. The default timeout if this method is not used is 500ms.
        /// </para>
        /// </remarks>
        /// <param name="timeout">The maximum amount of time to wait.</param>
        /// <returns>The same wait builder, so that calls may be chained.</returns>
        public WaitBuilder<T> ForAtMost(TimeSpan timeout)
        {
            this.timeout = timeout;
            return this;
        }

        /// <summary>
        /// Configures the wait action which will be created to use a specified polling interval.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The polling interval is the amount of time that the WebDriver will wait between each evaluation of the predicate function.
        /// The default interval is 500ms.  A shorter interval may result in more frequent evaluations of the predicate, causing more
        /// network traffic. Conversely, a shorter interval may also result in a more responsive wait.
        /// </para>
        /// <para>
        /// This method is optional.  If it is not called then the default polling interval will be used.
        /// </para>
        /// </remarks>
        /// <param name="pollingInterval">The polling interval to use.</param>
        /// <returns>The same wait builder, so that calls may be chained.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the polling interval has already been set.</exception>
        public WaitBuilder<T> WithPollingInterval(TimeSpan pollingInterval)
        {
            if(this.pollingInterval.HasValue)
                throw new InvalidOperationException("The polling interval has already been set; it cannot be set again.");
            
            this.pollingInterval = pollingInterval;
            return this;
        }

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
        public WaitBuilder<T> IgnoringTheseExceptionTypes(params Type[] ignoredExceptionTypes)
        {
            if(this.ignoredExceptionTypes != null)
                throw new InvalidOperationException("The ignored exception types have already been set; they cannot be set again.");
            
            this.ignoredExceptionTypes = ignoredExceptionTypes;
            return this;
        }

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
        public WaitBuilder<T> Named(string name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));
            if(this.name != null)
                throw new InvalidOperationException("The name has already been set; it cannot be set again.");
            
            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WaitBuilder{T}"/> class with a specified timeout.
        /// </summary>
        /// <param name="predicate">The predicate which ends the wait when it returns a successful result.</param>
        public WaitBuilder(Func<IWebDriver,T> predicate)
        {
            var predType = typeof(T);
            if(predType.IsValueType && predType != typeof(bool))
                throw new ArgumentException("The result type of the predicate must be a boolean or a reference type.", nameof(predicate));
            
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        /// <summary>
        /// Implicitly converts a <see cref="WaitBuilder{T}"/> to a <see cref="Wait{T}"/>.
        /// </summary>
        /// <param name="builder">The wait builder to convert.</param>
        /// <returns>A new <see cref="Wait{T}"/> instance.</returns>
        public static implicit operator Wait<T>(WaitBuilder<T> builder)
        {
            var predicate = new WaitUntilPredicate<T>(builder.predicate, builder.name);
            return new Wait<T>(builder.timeout, predicate, builder.pollingInterval, builder.ignoredExceptionTypes);
        }
    }
}