using System;

namespace CSF.Screenplay.Actors
{
    /// <summary>
    /// A model for event arguments which relate to an actor gaining a new ability.
    /// </summary>
    public class GainAbilityEventArgs : ActorEventArgs
    {
        /// <summary>
        /// Gets the ability which the actor has gained
        /// </summary>
        public object Ability { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="GainAbilityEventArgs"/>.
        /// </summary>
        /// <param name="actorName">The actor's name</param>
        /// <param name="performanceIdentity">The actor's performance identity</param>
        /// <param name="ability">The ability</param>
        public GainAbilityEventArgs(string actorName, Guid performanceIdentity, object ability) : base(actorName, performanceIdentity)
        {
            Ability = ability ?? throw new ArgumentNullException(nameof(ability));
        }
    }
}
