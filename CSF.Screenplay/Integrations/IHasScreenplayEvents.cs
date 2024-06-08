using System;

namespace CSF.Screenplay.Integrations
{
    /// <summary>An object which provides events which occur at the beginning and ending of the Screenplay process.</summary>
    /// <remarks>
    /// <para>A Screenplay process typically corresponds to a complete test run in a testing framework integration that consumes Screenplay.</para>
    /// </remarks>
    public interface IHasScreenplayEvents
    {
        /// <summary>Occurs at the beginning of a Screenplay process.</summary>
        event EventHandler ScreenplayBegun;

        /// <summary>Occurs when the Screenplay process has completed.</summary>
        event EventHandler ScreenplayCompleted;
    }
}