using System;
using CSF.Screenplay.Performances;

namespace CSF.Screenplay.Actors
{
    /// <summary>The default implementation of <see cref="IStage"/> which provides a context for which actor is currently active.</summary>
    public class Stage : IStage, IHasPerformanceIdentity
    {
        readonly ICast cast;
        readonly object spotlightSyncRoot = new object();

        Actor spotlitActor;

        /// <inheritdoc/>
        public virtual Guid PerformanceIdentity => cast.PerformanceIdentity;

        /// <inheritdoc/>
        public event EventHandler<ActorEventArgs> ActorSpotlit;

        /// <summary>Invokes the <see cref="ActorSpotlit"/> event.</summary>
        /// <param name="actor">The actor which is now in the spotlight</param>
        protected virtual void InvokeActorSpotlit(Actor actor)
        {
            var args = new ActorEventArgs(actor);
            ActorSpotlit?.Invoke(this, args);
        }

        /// <inheritdoc/>
        public event EventHandler<PerformanceScopeEventArgs> SpotlitTurnedOff;

        /// <summary>Invokes the <see cref="SpotlitTurnedOff"/> event.</summary>
        protected virtual void InvokeSpotlitTurnedOff()
        {
            var args = new PerformanceScopeEventArgs(PerformanceIdentity);
            SpotlitTurnedOff?.Invoke(this, args);
        }

        /// <inheritdoc/>
        public virtual Actor GetSpotlitActor() => spotlitActor;

        /// <inheritdoc/>
        public virtual void Spotlight(Actor actor)
        {
            if (actor is null) throw new ArgumentNullException(nameof(actor));

            lock(spotlightSyncRoot)
            {
                if (ReferenceEquals(actor, spotlitActor)) return;
                spotlitActor = actor;
            }

            InvokeActorSpotlit(actor);
        }

        /// <inheritdoc/>
        public virtual Actor Spotlight(IPersona persona)
        {
            var actor = cast.GetActor(persona);
            Spotlight(actor);
            return actor;
        }

        /// <inheritdoc/>
        public virtual Actor TurnSpotlightOff()
        {
            Actor previouslySpotlit;

            lock(spotlightSyncRoot)
            {
                if (spotlitActor is null) return null;
                previouslySpotlit = spotlitActor; 
                spotlitActor = null;
            }

            InvokeSpotlitTurnedOff();
            return previouslySpotlit;
        }

        /// <summary>Initialises a new instance of <see cref="Stage"/></summary>
        /// <param name="cast">The cast</param>
        /// <exception cref="ArgumentNullException">If the cast is <see langword="null" /></exception>
        public Stage(ICast cast)
        {
            this.cast = cast ?? throw new ArgumentNullException(nameof(cast));
        }
    }
}