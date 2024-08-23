using System;

namespace CSF.Screenplay
{
    /// <summary>An object which provides a value which uniquely identifies the currently-executing <see cref="Performance"/>.</summary>
    public interface IHasPerformanceIdentity
    {
        /// <summary>Gets the unique <see cref="Performance"/> identifier</summary>
        /// <remarks>
        /// <para>
        /// This value is used to uniquely identify a performance within a <see cref="Screenplay"/>.
        /// </para>
        /// </remarks>
        Guid PerformanceIdentity { get; }
    }
}