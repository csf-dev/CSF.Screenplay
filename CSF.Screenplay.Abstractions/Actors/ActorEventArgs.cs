using System;

namespace CSF.Screenplay.Actors
{
    /// <summary>
    /// A model for event arguments which relate to an <see cref="CSF.Screenplay.Actor"/>.
    /// </summary>
    /// <seealso cref="Actor"/>
    public class ActorEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the name of the actor to which these event arguments relate
        /// </summary>
        public string ActorName { get; }

        /// <summary>
        /// Gets the identity of the <see cref="IPerformance"/> to which the actor belongs.
        /// </summary>
        public Guid PerformanceIdentity { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="ActorEventArgs"/>
        /// </summary>
        /// <param name="actorName">The actor's name</param>
        /// <param name="performanceIdentity">The actor's performance identity</param>
        public ActorEventArgs(string actorName, Guid performanceIdentity)
        {
            ActorName = actorName ?? throw new ArgumentNullException(nameof(actorName));
            PerformanceIdentity = performanceIdentity;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ActorEventArgs"/>
        /// </summary>
        /// <param name="actor">The actor</param>
        public ActorEventArgs(Actor actor) : this(((IHasName) actor).Name, ((IHasPerformanceIdentity) actor).PerformanceIdentity) {}
    }
}
