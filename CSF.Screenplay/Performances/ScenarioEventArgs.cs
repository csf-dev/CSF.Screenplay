using System;
using System.Collections.Generic;

namespace CSF.Screenplay.Performances
{
    /// <summary>
    /// A model for event arguments which relate to a scope of a performance.
    /// </summary>
    /// <seealso cref="Performance"/>
    /// <seealso cref="PerformanceScopeEventArgs"/>
    public class ScenarioEventArgs : PerformanceScopeEventArgs, IHasScenarioHierarchy
    {
        /// <inheritdoc/>
        public IReadOnlyList<IdentifierAndName> ScenarioHierarchy { get; }

        /// <summary>Initialises a new instance of <see cref="ScenarioEventArgs"/></summary>
        /// <param name="performanceIdentity">The performance identity</param>
        /// <param name="scenarioHierarchy">The scenario hierarchy</param>
        /// <exception cref="ArgumentNullException">If the scenario hierarchy is <see langword="null" /></exception>
        public ScenarioEventArgs(Guid performanceIdentity, IReadOnlyList<IdentifierAndName> scenarioHierarchy) : base(performanceIdentity)
        {
            ScenarioHierarchy = scenarioHierarchy ?? throw new System.ArgumentNullException(nameof(scenarioHierarchy));
        }
    }
}