using System.Collections.Generic;

namespace CSF.Screenplay.Performances
{
    /// <summary>An object which creates instances of <see cref="Performance"/>; a factory service.</summary>
    public interface ICreatesPerformance
    {
        /// <summary>Creates a new performance with the specified naming hierarchy.</summary>
        /// <param name="namingHierarchy">A collection of identifiers and names providing the naming
        /// hierarchy; see <see cref="Performance.NamingHierarchy"/> for more information.</param>
        /// <returns>A new performance instance</returns>
        Performance CreatePerformance(IList<IdentifierAndName> namingHierarchy = default);
    }
}