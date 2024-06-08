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
        /// <param name="actor">The actor</param>
        /// <param name="ability">The ability</param>
        public GainAbilityEventArgs(ICanPerform actor, object ability) : base(actor)
        {
            Ability = ability ?? throw new ArgumentNullException(nameof(ability));
        }
    }
}
