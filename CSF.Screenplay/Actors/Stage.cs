using System;
using CSF.Screenplay.Performances;

namespace CSF.Screenplay.Actors
{
    /// <summary>The default implementation of <see cref="IStage"/> which provides a context for which actor is currently active.</summary>
    public sealed class Stage : IStage, IHasPerformanceIdentity
    {
        readonly object spotlightSyncRoot = new object();
        readonly IRelaysPerformanceEvents performanceEventBus;
        Actor spotlitActor;

        /// <summary>
        /// Gets the cast to which the current stage is linked.
        /// </summary>
        public ICast Cast { get; }

        /// <inheritdoc/>
        public Guid PerformanceIdentity => Cast.PerformanceIdentity;

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

            performanceEventBus.InvokeActorSpotlit(actor);
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

            performanceEventBus.InvokeSpotlightTurnedOff(PerformanceIdentity);
            return previouslySpotlit;
        }

        /// <summary>Initialises a new instance of <see cref="Stage"/></summary>
        /// <param name="cast">The cast</param>
        /// <param name="performanceEventBus">An event bus for collecting stage-related events</param>
        /// <exception cref="ArgumentNullException">If any parameter value is <see langword="null" /></exception>
        public Stage(ICast cast, IRelaysPerformanceEvents performanceEventBus)
        {
            Cast = cast ?? throw new ArgumentNullException(nameof(cast));
            this.performanceEventBus = performanceEventBus ?? throw new ArgumentNullException(nameof(performanceEventBus));
        }
    }
}