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

        /// <summary>
        /// Initializes a new instance of <see cref="ActorEventArgs"/>
        /// </summary>
        /// <param name="actor">The actor</param>
        public ActorEventArgs(Actor actor) : base(((IHasPerformanceIdentity) actor ?? throw new ArgumentNullException(nameof(actor))).PerformanceIdentity)
        {
            Actor = actor;
        }
    }
}
