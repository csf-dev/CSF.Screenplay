using System.Collections.Generic;

namespace CSF.Screenplay.Performances
{
    /// <summary>An object which creates instances of <see cref="Performance"/>; a factory service.</summary>
    public interface ICreatesPerformance
    {
        /// <summary>Creates a new performance instance.</summary>
        /// <returns>A new performance instance</returns>
        Performance CreatePerformance();
    }
}