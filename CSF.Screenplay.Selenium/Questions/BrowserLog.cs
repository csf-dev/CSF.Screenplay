using System;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// Model which represents a single web browser log entry.
    /// </summary>
    public sealed class BrowserLog
    {
        /// <summary>
        /// Gets a string description of the 'severity' level at which this item was recorded.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The levels and their precise meanings could potentially be implementation/WebDriver-specific.
        /// Generally speaking they are quite predictable, and correspond to strings which are equivalent to the members of
        /// <see cref="OpenQA.Selenium.LogLevel"/>.
        /// </para>
        /// </remarks>
        public string Level { get; set; }

        /// <summary>
        /// The log message, as recorded in the console log.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Note that log messages are retrieved from web browsers as pure strings, wheras they may be recorded as mixed strings, values and objects.
        /// When using techniques such as <see cref="PerformableBuilder.GetTheBrowserLogs"/>, it's important to ensure that the logs retrieved are readable and
        /// useful when converted to pure strings. This is a limitation which exists due to the cross-domain (web browser/Javascript and .NET) communication.
        /// There is no good <em>one size fits all</em> solution to interpreting arbitrary objects within browser logs, which convert them to useful strings.
        /// </para>
        /// </remarks>
        public string Message { get; set; }

        /// <summary>
        /// Gets the timestamp at which the log message was recorded.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}