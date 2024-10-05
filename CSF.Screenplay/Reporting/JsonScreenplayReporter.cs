using CSF.Screenplay.Performances;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// An object which subscribes/listens to the events of <see cref="IHasPerformanceEvents"/> and which
    /// produces a JSON-formatted report from them.
    /// </summary>
    public class JsonScreenplayReporter
    {
        readonly string filePath;

        /// <summary>
        /// Subscribes to the events emitted by the specified Screenplay event notifier.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As events are received, the JSON object model will be accumulated and written incrementally to file.
        /// </para>
        /// </remarks>
        /// <param name="events">A Screenplay event notifier</param>
        public void SubscribeTo(IHasPerformanceEvents events)
        {
            // throw new NotImplementedException();
        }

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
        public void UnsubscribeFrom(IHasPerformanceEvents events)
        {
            // throw new NotImplementedException();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="JsonScreenplayReporter"/> for a specified file path.
        /// </summary>
        /// <param name="filePath">The file path at which to write the report.</param>
        /// <exception cref="System.ArgumentNullException">If <paramref name="filePath"/> is <see langword="null" />.</exception>
        public JsonScreenplayReporter(string filePath)
        {
            this.filePath = filePath ?? throw new System.ArgumentNullException(nameof(filePath));
        }
    }
}