using System;

namespace CSF.Screenplay
{
    /// <summary>An object which provides a value which uniquely identifies the currently-executing performance</summary>
    public interface IHasPerformanceIdentity
    {
        /// <summary>Gets the unique performance identifier</summary>
        Guid PerformanceIdentity { get; }
    }
}