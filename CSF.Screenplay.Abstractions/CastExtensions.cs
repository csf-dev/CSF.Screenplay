using System;

namespace CSF.Screenplay
{
    /// <summary>
    /// Extension methods for <see cref="ICast"/>.
    /// </summary>
    public static class CastExtensions
    {
        /// <summary>
        /// Gets a single <see cref="Actor"/> based upon a persona, creating them if they do not already exist in the cast.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method will create the actor within the current cast, using a resolved instance of the persona as a factory,
        /// if they do not already exist.
        /// Alternatively, this method will return the existing actor, if they already exist in the cast, matched using the
        /// <see cref="IPersona"/>'s <see cref="IHasName.Name"/>.
        /// If executing this method results in the creation of a new actor then the <see cref="ICast.ActorCreated"/>
        /// event will be triggered.
        /// </para>
        /// <para>
        /// Actor names are matched using a case-insensitive invariant culture string comparison. Cast implementations
        /// should match an existing actor if the specified persona name differs only in case.
        /// </para>
        /// <para>
        /// This method is the recommended way of getting an actor from a persona, as it takes care of resolving the persona
        /// from dependency injection automatically.
        /// </para>
        /// </remarks>
        /// <returns>An actor based on the specified persona, either an existing instance or a newly-created actor.</returns>
        /// <param name="cast">The cast from which to get an actor</param>
        /// <seealso cref="IPersona"/>
        /// <typeparam name="TPersona">A concrete type of <see cref="IPersona"/> from which to get an actor.</typeparam>
        public static Actor GetActor<TPersona>(this ICast cast) where TPersona : class,IPersona
        {
            if (cast is null)
                throw new ArgumentNullException(nameof(cast));

            var persona = (IPersona) cast.ServiceProvider.GetService(typeof(TPersona));
            if (persona is null)
                throw new InvalidOperationException($"The persona of type {typeof(TPersona).FullName} must be available in dependency injection to use it with this method.");
            return cast.GetActor(persona);
        }
    }
}