using System;
using System.Collections.Generic;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Actions;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// Provides a builder for configuring wait actions in Selenium.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class provides methods for configuring various aspects of a wait action, such as timeout, polling interval,
    /// and ignored exception types.
    /// Using this class directly requires that a <see cref="WaitUntilPredicate"/> instance is provided to the constructor.
    /// The specified predicate provides both the logic to evaluate and a human-readable name for the wait condition.
    /// </para>
    /// </remarks>
    public class NamedWaitBuilder : IGetsPerformable
    {
        readonly WaitUntilPredicate<bool> predicate;

        /// <summary>
        /// Gets or sets the maximum timeout for the wait action.
        /// </summary>
        protected TimeSpan? Timeout { get; set; }

        /// <summary>
        /// Gets or sets the interval at which Selenium will poll to determine if the predicate to end the wait has been met.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If this property is <see langword="null"/>, then Selenium provides a default polling interval of 500ms.
        /// </para>
        /// </remarks>
        protected TimeSpan? PollingInterval { get; set; }

        /// <summary>
        /// Gets or sets the collection of exception types which will be ignored when evaluating the wait predicate.
        /// </summary>
        protected ICollection<Type> IgnoredExceptionTypes { get; set; } = new List<Type>();

        /// <summary>
        /// Gets the wait-until predicate, which wraps both the predicate logic and a human-readable name.
        /// </summary>
        /// <returns>The wait-until predicate instance.</returns>
        protected virtual WaitUntilPredicate<bool> GetWaitUntilPredicate() => predicate;

        /// <summary>
        /// Configures the wait action to use a specified maximum timeout.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the timeout is reached before the predicate function returns a <see langword="true"/> result then
        /// the wait will end, and an exception will be raised.
        /// </para>
        /// <para>
        /// This method is optional.  If it is not called then the default behaviour of the <see cref="Wait"/> question
        /// will be used.  See the documentation for that class for more details.
        /// </para>
        /// </remarks>
        /// <param name="timeout">The maximum amount of time to wait.</param>
        /// <returns>The same wait builder, so that calls may be chained.</returns>
        public NamedWaitBuilder ForAtMost(TimeSpan timeout)
        {
            Timeout = timeout;
            return this;
        }

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
        public NamedWaitBuilder WithPollingInterval(TimeSpan pollingInterval)
        {
            if(PollingInterval.HasValue)
                throw new InvalidOperationException("The polling interval has already been set; it cannot be set again.");
            
            PollingInterval = pollingInterval;
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
        public NamedWaitBuilder IgnoringTheseExceptionTypes(params Type[] ignoredExceptionTypes)
        {
            if(IgnoredExceptionTypes != null)
                throw new InvalidOperationException("The ignored exception types have already been set; they cannot be set again.");
            
            IgnoredExceptionTypes = ignoredExceptionTypes;
            return this;
        }

        IPerformable IGetsPerformable.GetPerformable()
            => new Wait(GetWaitUntilPredicate(), Timeout, PollingInterval, IgnoredExceptionTypes);

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedWaitBuilder"/> class.
        /// </summary>
        /// <param name="predicate">The predicate which ends the wait when it returns a successful result.</param>
        public NamedWaitBuilder(WaitUntilPredicate<bool> predicate)
        {
            this.predicate = predicate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedWaitBuilder"/> class.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This constructor is protected, as it is only to be used by derived classes which provide an alternative
        /// implementation of <see cref="GetWaitUntilPredicate"/>.
        /// </para>
        /// </remarks>
        protected NamedWaitBuilder() {}

        /// <summary>
        /// Implicitly converts a <see cref="NamedWaitBuilder"/> (or derived type) to a <see cref="Wait"/>.
        /// </summary>
        /// <param name="builder">The wait builder to convert.</param>
        /// <returns>A new <see cref="Wait"/> instance.</returns>
        public static implicit operator Wait(NamedWaitBuilder builder)
        {
            return new Wait(builder.GetWaitUntilPredicate(), builder.Timeout, builder.PollingInterval, builder.IgnoredExceptionTypes);
        }
    }
}