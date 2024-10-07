using System;
using CSF.Screenplay.Performances;

namespace CSF.Screenplay.Actors
{
    /// <summary>
    /// A model for event arguments which relate to an <see cref="Screenplay.Actor"/>.
    /// </summary>
    /// <seealso cref="Screenplay.Actor"/>
    public class ActorEventArgs : PerformanceScopeEventArgs
    {
        /// <summary>
        /// Gets the name of the actor to which these event arguments relate
        /// </summary>
        public Actor Actor { get; }

        // /// <summary>
        // /// Initializes a new instance of <see cref="ActorEventArgs"/>
        // /// </summary>
        // /// <param name="actorName">The actor's name</param>
        // /// <param name="performanceIdentity">The actor's performance identity</param>
        // public ActorEventArgs(string actorName, Guid performanceIdentity) : base(performanceIdentity)
        // {
        //     ActorName = actorName ?? throw new ArgumentNullException(nameof(actorName));
        // }

        /// <summary>
        /// Initializes a new instance of <see cref="ActorEventArgs"/>
        /// </summary>
        /// <param name="actor">The actor</param>
        public ActorEventArgs(Actor actor) : base(((IHasPerformanceIdentity) actor).PerformanceIdentity)
        {
            Actor = actor ?? throw new ArgumentNullException(nameof(actor));
        }
    }
}
