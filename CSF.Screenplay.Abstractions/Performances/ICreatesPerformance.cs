using System.Collections.Generic;

namespace CSF.Screenplay.Performances
{
    /// <summary>An object which creates instances of <see cref="IPerformance"/>; a factory service.</summary>
    public interface ICreatesPerformance
    {
        /// <summary>Creates a new performance instance.</summary>
        /// <param name="namingHierarchy">A collection of identifiers and names providing the hierarchical name of this
        /// performance; see <see cref="IPerformance.NamingHierarchy"/> for more information.</param>
        /// <returns>A new performance instance</returns>
        IPerformance CreatePerformance(IList<IdentifierAndName> namingHierarchy = null);
    }
}