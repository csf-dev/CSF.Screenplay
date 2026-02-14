using System;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// An ability which specifies a default amount of time for performables which involve waiting.
    /// that allows an <see cref="Actor"/> to use a default time for which
    /// to wait for <see cref="BrowseTheWeb"/> WebDriver operations to complete.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A small number of performables within the Selenium extension for Screenplay involve waiting,
    /// along with the opportunity to specify a timeout.  If no timeout is specified and the <see cref="Actor"/>
    /// does not have this ability then the default timeout is 5 seconds, as specified by
    /// <see cref="Actions.Wait.DefaultTimeout"/>.
    /// </para>
    /// <para>
    /// If the Actor has this ability then, if they perform a waiting-type action and no timeout is explicitly specified,
    /// they will use the default timeout which is specified within this ability instead of the 5-second hard-coded default.
    /// </para>
    /// </remarks>
    /// <seealso cref="Actions.Wait"/>
    /// <seealso cref="Tasks.ClickAndWaitForDocumentReady"/>
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