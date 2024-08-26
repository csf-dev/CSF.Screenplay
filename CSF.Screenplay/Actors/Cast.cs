using System;
using System.Collections.Concurrent;
using CSF.Screenplay.Performances;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay.Actors
{
    /// <summary>The default implementation of <see cref="ICast"/> which serves as a registry/factory for <see cref="Actor"/> instances.</summary>
    public sealed class Cast : ICast
    {
        readonly IRelaysPerformanceEvents performanceEventBus;
        readonly ConcurrentDictionary<string,Actor> actors = new ConcurrentDictionary<string,Actor>(StringComparer.InvariantCultureIgnoreCase);

        /// <inheritdoc/>
        public IServiceProvider ServiceProvider { get; }

        /// <inheritdoc/>
        public Guid PerformanceIdentity { get; }

        /// <inheritdoc/>
        public event EventHandler<ActorEventArgs> ActorCreated;

        /// <summary>Invokes the <see cref="ActorCreated"/> event.</summary>
        /// <param name="actor">The actor which has been created</param>
        void InvokeActorCreated(Actor actor)
        {
            var args = new ActorEventArgs(actor);
            ActorCreated?.Invoke(this, args);
        }

        /// <inheritdoc/>
        public Actor GetActor(string name) => GetActor(new NameOnlyPersona(name));

        /// <inheritdoc/>
        public Actor GetActor(IPersona persona)
        {
            if (persona is null) throw new ArgumentNullException(nameof(persona));
            return actors.GetOrAdd(persona.Name, _ => {
                var actor = persona.GetActor(PerformanceIdentity);
                performanceEventBus.SubscribeTo(actor);
                InvokeActorCreated(actor);
                return actor;
            });
        }

        /// <summary>Initialises a new instance of <see cref="Cast"/>.</summary>
        /// <param name="serviceProvider">A service provider</param>
        /// <param name="performanceIdentity">The identity of the current performance</param>
        /// <exception cref="ArgumentNullException">If <paramref name="serviceProvider"/> is <see langword="null" />.</exception>
        public Cast(IServiceProvider serviceProvider, Guid performanceIdentity)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            PerformanceIdentity = performanceIdentity;
            performanceEventBus = serviceProvider.GetRequiredService<IRelaysPerformanceEvents>();
        }

        /// <summary>Implementation of <see cref="IPersona"/> which performs no operation beyond giving the actor a name</summary>
        class NameOnlyPersona : IPersona
        {
            /// <inheritdoc/>
            public string Name { get; }

            /// <inheritdoc/>
            public Actor GetActor(Guid performanceIdentity)
                => new Actor(Name, performanceIdentity);

            internal NameOnlyPersona(string name)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
            }
        }
    }
}