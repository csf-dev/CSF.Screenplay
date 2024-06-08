using System;

namespace CSF.Screenplay.Actors
{
    /// <summary>
    /// A model for event arguments which relate to a scenario.
    /// </summary>
    /// <seealso cref="ICanPerform"/>
    public class ScenarioEventArgs : EventArgs, IHasScenarioIdentity
    {
        /// <inheritdoc/>
        public Guid ScenarioIdentity { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="ScenarioEventArgs"/>
        /// </summary>
        /// <param name="scenarioIdentity">The scenario identity</param>
        public ScenarioEventArgs(Guid scenarioIdentity)
        {
            ScenarioIdentity = scenarioIdentity;
        }
    }
}
