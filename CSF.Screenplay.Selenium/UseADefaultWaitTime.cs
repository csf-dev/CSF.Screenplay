using System;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// Screenplay ability that allows an <see cref="Actor"/> to use a default time for which
    /// to wait for <see cref="BrowseTheWeb"/> WebDriver operations to complete.
    /// </summary>
    public class UseADefaultWaitTime : ICanReport
    {
        /// <summary>
        /// Gets the default time for which to wait.
        /// </summary>
        public TimeSpan WaitTime { get; }

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} is able to use a default wait time of {Timeout}",
                                actor.Name,
                                WaitTime);

        /// <summary>
        /// Initializes a new instance of the <see cref="UseADefaultWaitTime"/> class with the specified wait time.
        /// </summary>
        /// <param name="waitTime">The default time for which to wait.</param>
        public UseADefaultWaitTime(TimeSpan waitTime)
        {
            WaitTime = waitTime;
        }
    }
}