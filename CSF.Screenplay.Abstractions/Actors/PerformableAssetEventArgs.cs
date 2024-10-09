using System;

namespace CSF.Screenplay.Actors
{
    /// <summary>
    /// Event arguments which represent the revealing of a file asset which relates to a performable.
    /// </summary>
    /// <seealso cref="ICanPerform.RecordAsset(object, string, string)"/>
    public class PerformableAssetEventArgs : PerformableEventArgs
    {
        /// <summary>
        /// Gets a full/absolute path to the asset file.
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// Gets an optional human-readable summary of what this asset represents.  This should be one sentence at most, suitable
        /// for display in a UI tool-tip.
        /// </summary>
        public string FileSummary { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="PerformableAssetEventArgs"/>.
        /// </summary>
        /// <param name="actor">The actor</param>
        /// <param name="performable">The performable item</param>
        /// <param name="filePath">The full absolute path to the asset file</param>
        /// <param name="fileSummary">An optional human-readable summary of the asset file</param>
        /// <param name="phase">The phase of performance</param>
        public PerformableAssetEventArgs(Actor actor,
                                         object performable,
                                         string filePath,
                                         string fileSummary = null,
                                         PerformancePhase phase = PerformancePhase.Unspecified) : base(actor, performable, phase)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            FileSummary = fileSummary;
        }
    }
}