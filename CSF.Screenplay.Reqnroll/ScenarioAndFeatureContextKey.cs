using System;
#if SPECFLOW
using TechTalk.SpecFlow;
#else
using Reqnroll;
#endif

namespace CSF.Screenplay
{
    /// <summary>
    /// Simple key class for a combination of Scenario &amp; Feature contexts.
    /// </summary>
    internal sealed class ScenarioAndFeatureContextKey : IEquatable<ScenarioAndFeatureContextKey>
    {
        readonly int cachedHashCode;

        /// <summary>Gets the scenario</summary>
        public ScenarioContext Scenario { get; }

        /// <summary>Gets the feature</summary>
        public FeatureContext Feature { get; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => Equals(obj as ScenarioAndFeatureContextKey);

        /// <inheritdoc/>
        public bool Equals(ScenarioAndFeatureContextKey other)
        {
            if(ReferenceEquals(this, other)) return true;
            if(ReferenceEquals(null, other)) return false;

            return ReferenceEquals(Scenario, other.Scenario) && ReferenceEquals(Feature, other.Feature);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => cachedHashCode;

        /// <summary>
        /// Initializes a new instance of <see cref="ScenarioAndFeatureContextKey"/>.
        /// </summary>
        /// <param name="scenario">The scenario</param>
        /// <param name="feature">The feature</param>
        /// <exception cref="ArgumentNullException">If either parameter is <see langword="null" />.</exception>
        public ScenarioAndFeatureContextKey(ScenarioContext scenario, FeatureContext feature)
        {
            if(scenario is null) throw new ArgumentNullException(nameof(scenario));
            if(feature is null) throw new ArgumentNullException(nameof(feature));

            Scenario = scenario;
            Feature = feature;
            cachedHashCode = scenario.GetHashCode() ^ feature.GetHashCode();
        }
    }
}
