using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Selenium.Reporting
{
    /// <summary>
    /// A service which subscribes to the <see cref="IHasPerformableEvents.PerformableFailed"/> event and takes a screenshot
    /// when a performable fails.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is useful when diagnosing and debugging failed performances.
    /// </para>
    /// </remarks>
    public interface ITakesScreenshotWhenPerformableFails
    {
        /// <summary>
        /// Handles the <see cref="IHasPerformableEvents.PerformableFailed"/> event.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="ev">The event args</param>
        void OnPerformableFailed(object sender, PerformableFailureEventArgs ev);
    }
}
