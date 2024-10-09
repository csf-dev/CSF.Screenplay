using System;
using CSF.Screenplay.Performances;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// Defines a reporter that subscribes to and processes events from a Screenplay event notifier.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Implementations of this interface are responsible for accumulating information about the Screenplay
    /// as events are received, and generating reports based on that information.
    /// </para>
    /// </remarks>
    public interface IReporter : IDisposable
    {
        /// <summary>
        /// Subscribes to the events emitted by the specified Screenplay event notifier.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As events are received, this reporter instance may accumulate information about the Screnplay that it is to report upon.
        /// </para>
        /// </remarks>
        /// <param name="events">A Screenplay event notifier</param>
        void SubscribeTo(IHasPerformanceEvents events);

        /// <summary>
        /// Unsubscribes from the specified Screenplay event notifier.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use this method only after the event notifier has emitted the <see cref="IHasPerformanceEvents.ScreenplayEnded"/> event.
        /// If this reporter unsubscribes from Screenplay events before the Screenplay has ended then the results are undefined.
        /// This could lead to a corrupt report file.
        /// </para>
        /// </remarks>
        /// <param name="events">A Screenplay event notifier</param>
        void UnsubscribeFrom(IHasPerformanceEvents events);
    }
}