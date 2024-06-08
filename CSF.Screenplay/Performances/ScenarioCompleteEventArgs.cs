using System;
using System.Collections.Generic;

namespace CSF.Screenplay.Performances
{
    /// <summary>
    /// A model for event arguments which relate to a scope of a performance.
    /// </summary>
    /// <seealso cref="Performance"/>
    /// <seealso cref="PerformanceScopeEventArgs"/>
    /// <seealso cref="ScenarioEventArgs"/>
    public class ScenarioCompleteEventArgs : ScenarioEventArgs, IHasScenarioHierarchy
    {
        /// <summary>Gets a value indicating whether the completion of this scenario was a success or not</summary>
        /// <remarks>
        /// <para>
        /// When this value is <see langword="true" /> then the scenario integration which is consuming Screenplay should be
        /// considered a success; a "test pass" in testing framework terms.
        /// When this value is <see langword="false" /> then the scenario integration should be considered a failure.
        /// When this value is <see langword="null" /> then this indicates neither success nor failure; it typically means
        /// that the scenario was skipped or terminated prematurely in some manner, but not in a way which should count
        /// as a failure.
        /// </para>
        /// </remarks>
        public bool? Success { get; }

        /// <summary>Initialises a new instance of <see cref="ScenarioEventArgs"/></summary>
        /// <param name="performanceIdentity">The performance identity</param>
        /// <param name="scenarioHierarchy">The scenario hierarchy</param>
        /// <param name="success">A value indicating whether or not the scenario completed with a succeess result</param>
        /// <exception cref="ArgumentNullException">If the scenario hierarchy is <see langword="null" /></exception>
        public ScenarioCompleteEventArgs(Guid performanceIdentity,
                                         IReadOnlyList<IdentifierAndName> scenarioHierarchy,
                                         bool? success) : base(performanceIdentity, scenarioHierarchy)
        {
            Success = success;
        }
    }
}