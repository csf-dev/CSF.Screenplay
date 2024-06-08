using System.Collections.Generic;

namespace CSF.Screenplay.Performances
{
    /// <summary>An object which creates instances of <see cref="Performance"/>; a factory service.</summary>
    public interface ICreatesPerformance
    {
        /// <summary>Creates a new performance with the specified scenario hierarchy.</summary>
        /// <param name="scenarioHierarchy">A collection of identifiers and names providing the scenario
        /// hierarchy; see <see cref="IHasScenarioHierarchy"/> for more information.</param>
        /// <returns>A new performance instance</returns>
        Performance CreatePerformance(IList<IdentifierAndName> scenarioHierarchy = default);
    }
}