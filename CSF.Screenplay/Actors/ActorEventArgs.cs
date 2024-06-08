using System;

namespace CSF.Screenplay.Actors
{
    /// <summary>
    /// A model for event arguments which relate to an actor.
    /// </summary>
    /// <seealso cref="ICanPerform"/>
    public class ActorEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the actor to which these event arguments relate
        /// </summary>
        public ICanPerform Actor { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="ActorEventArgs"/>
        /// </summary>
        /// <param name="actor">The actor</param>
        public ActorEventArgs(ICanPerform actor)
        {
            Actor = actor ?? throw new ArgumentNullException(nameof(actor));;
        }
    }
}
