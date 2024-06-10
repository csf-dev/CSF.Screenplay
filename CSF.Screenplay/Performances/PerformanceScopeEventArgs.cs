using System;

namespace CSF.Screenplay.Performances
{
    /// <summary>
    /// A model for event arguments which relate to a scope of a performance.
    /// </summary>
    /// <seealso cref="Performance"/>
    public class PerformanceScopeEventArgs : EventArgs, IHasPerformanceIdentity
    {
        /// <inheritdoc/>
        public Guid PerformanceIdentity { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="PerformanceScopeEventArgs"/>
        /// </summary>
        /// <param name="performanceIdentity">The performance identity</param>
        public PerformanceScopeEventArgs(Guid performanceIdentity)
        {
            PerformanceIdentity = performanceIdentity;
        }
    }
}