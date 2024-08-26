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
        /// Gets the actor to which these event arguments relate
        /// </summary>
        public Actor Actor { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="ActorEventArgs"/>
        /// </summary>
        /// <param name="actor">The actor</param>
        public ActorEventArgs(Actor actor)
        {
            Actor = actor ?? throw new ArgumentNullException(nameof(actor));;
        }
    }
}
