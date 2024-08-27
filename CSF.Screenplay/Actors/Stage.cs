using System;
using CSF.Screenplay.Performances;

namespace CSF.Screenplay.Actors
{
    /// <summary>The default implementation of <see cref="IStage"/> which provides a context for which actor is currently active.</summary>
    public sealed class Stage : IStage, IHasPerformanceIdentity
    {
        readonly object spotlightSyncRoot = new object();

        Actor spotlitActor;

        /// <summary>
        /// Gets the cast to which the current stage is linked.
        /// </summary>
        public ICast Cast { get; }

        /// <inheritdoc/>
        public Guid PerformanceIdentity => Cast.PerformanceIdentity;

        /// <inheritdoc/>
        public event EventHandler<ActorEventArgs> ActorSpotlit;

        /// <summary>Invokes the <see cref="ActorSpotlit"/> event.</summary>
        /// <param name="actor">The actor which is now in the spotlight</param>
        void InvokeActorSpotlit(Actor actor)
        {
            var args = new ActorEventArgs(actor);
            ActorSpotlit?.Invoke(this, args);
        }

        /// <inheritdoc/>
        public event EventHandler<PerformanceScopeEventArgs> SpotlightTurnedOff;

        /// <summary>Invokes the <see cref="SpotlightTurnedOff"/> event.</summary>
        void InvokeSpotlitTurnedOff()
        {
            var args = new PerformanceScopeEventArgs(PerformanceIdentity);
            SpotlightTurnedOff?.Invoke(this, args);
        }

        /// <inheritdoc/>
        public Actor GetSpotlitActor() => spotlitActor;

        /// <inheritdoc/>
        public void Spotlight(Actor actor)
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
        public Actor Spotlight(IPersona persona)
        {
            var actor = Cast.GetActor(persona);
            Spotlight(actor);
            return actor;
        }

        /// <inheritdoc/>
        public Actor TurnSpotlightOff()
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
            Cast = cast ?? throw new ArgumentNullException(nameof(cast));
        }
    }
}