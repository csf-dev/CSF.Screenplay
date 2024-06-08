using System;

namespace CSF.Screenplay
{
    /// <summary>An object which provides a value which uniquely identifies the currently-executing scenario</summary>
    public interface IHasScenarioIdentity
    {
        /// <summary>Gets the unique scenario identifier</summary>
        Guid ScenarioIdentity { get; }
    }
}