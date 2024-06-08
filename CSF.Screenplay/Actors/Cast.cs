using System;
using System.Collections.Concurrent;

namespace CSF.Screenplay.Actors
{
    /// <summary>The default implementation of <see cref="ICast"/> which serves as a registry/factory for <see cref="Actor"/> instances.</summary>
    public class Cast : ICast
    {
        readonly ConcurrentDictionary<string,Actor> actors = new ConcurrentDictionary<string,Actor>(StringComparer.InvariantCultureIgnoreCase);

        /// <inheritdoc/>
        public virtual IServiceProvider ServiceProvider { get; }

        /// <inheritdoc/>
        public virtual Guid ScenarioIdentity { get; }

        /// <inheritdoc/>
        public event EventHandler<ActorEventArgs> ActorCreated;

        /// <summary>Invokes the <see cref="ActorCreated"/> event.</summary>
        /// <param name="actor">The actor which has been created</param>
        protected virtual void InvokeActorCreated(Actor actor)
        {
            var args = new ActorEventArgs(actor);
            ActorCreated?.Invoke(this, args);
        }

        /// <inheritdoc/>
        public virtual Actor GetActor(string name) => GetActor(new NameOnlyPersona(name));

        /// <inheritdoc/>
        public virtual Actor GetActor(IPersona persona)
        {
            if (persona is null) throw new ArgumentNullException(nameof(persona));
            return actors.GetOrAdd(persona.Name, _ => {
                var actor = persona.GetActor(ScenarioIdentity, ServiceProvider);
                InvokeActorCreated(actor);
                return actor;
            });
        }

        /// <summary>Initialises a new instance of <see cref="Cast"/>.</summary>
        /// <param name="serviceProvider">A service provider</param>
        /// <param name="scenarioIdentity">The identity of the current scenario/performance</param>
        /// <exception cref="ArgumentNullException">If <paramref name="serviceProvider"/> is <see langword="null" />.</exception>
        public Cast(IServiceProvider serviceProvider, Guid scenarioIdentity)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            ScenarioIdentity = scenarioIdentity;
        }

        /// <summary>Implementation of <see cref="IPersona"/> which performs no operation beyond giving the actor a name</summary>
        class NameOnlyPersona : IPersona
        {
            /// <inheritdoc/>
            public string Name { get; }

            /// <inheritdoc/>
            public Actor GetActor(Guid scenarioIdentity, IServiceProvider serviceProvider)
                => new Actor(Name, scenarioIdentity, serviceProvider);

            internal NameOnlyPersona(string name)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
            }
        }
    }
}