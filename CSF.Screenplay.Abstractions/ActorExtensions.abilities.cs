using System;
using System.Linq;

namespace CSF.Screenplay
{
    /// <summary>Extension methods for actor types</summary>
    public static partial class ActorExtensions
    {
        /// <summary>Gets a value which indicates if the actor has an ability of the specified type.</summary>
        /// <remarks>
        /// <para>
        /// This method will also return <see langword="false" /> if the actor does not implement <see cref="IHasAbilities"/>.
        /// </para>
        /// </remarks>
        /// <param name="actor">An actor</param>
        /// <param name="abilityType">The ability type for which to test</param>
        /// <returns><see langword="true" /> if the <paramref name="actor"/> has an ability of the specified
        /// <paramref name="abilityType"/>; <see langword="false" /> if not.</returns>
        public static bool HasAbility(this ICanPerform actor, Type abilityType)
            => actor is IHasAbilities abilityActor && HasAbility(abilityActor, abilityType);

        /// <summary>Gets a value which indicates if the actor has an ability of the specified type.</summary>
        /// <remarks>
        /// <para>
        /// This method will also return <see langword="false" /> if the actor does not implement <see cref="IHasAbilities"/>.
        /// </para>
        /// </remarks>
        /// <param name="actor">An actor</param>
        /// <typeparam name="T">The ability type for which to test</typeparam>
        /// <returns><see langword="true" /> if the <paramref name="actor"/> has an ability of the specified
        /// <typeparamref name="T"/>; <see langword="false" /> if not.</returns>
        public static bool HasAbility<T>(this ICanPerform actor)
            => actor is IHasAbilities abilityActor && HasAbility<T>(abilityActor);

        /// <summary>Gets a value which indicates if the actor has an ability of the specified type.</summary>
        /// <param name="actor">An actor</param>
        /// <param name="abilityType">The ability type for which to test</param>
        /// <returns><see langword="true" /> if the <paramref name="actor"/> has an ability of the specified
        /// <paramref name="abilityType"/>; <see langword="false" /> if not.</returns>
        public static bool HasAbility(this IHasAbilities actor, Type abilityType)
        {
            if(actor is null) throw new ArgumentNullException(nameof(actor));
            if(abilityType is null) throw new ArgumentNullException(nameof(abilityType));

            return actor.Abilities.Any(abilityType.IsInstanceOfType);
        }

        /// <summary>Gets a value which indicates if the actor has an ability of the specified type.</summary>
        /// <param name="actor">An actor</param>
        /// <typeparam name="T">The ability type for which to test</typeparam>
        /// <returns><see langword="true" /> if the <paramref name="actor"/> has an ability of the specified
        /// <typeparamref name="T"/>; <see langword="false" /> if not.</returns>
        public static bool HasAbility<T>(this IHasAbilities actor) => HasAbility(actor, typeof(T));

        /// <summary>Gets the first ability which the actor has of the specified type</summary>
        /// <param name="actor">The actor from whom to get the ability</param>
        /// <typeparam name="T">The type of ability desired</typeparam>
        /// <returns>The ability instance</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="actor"/> is <see langword="null" /></exception>
        /// <exception cref="ArgumentException">If the actor does not implement <see cref="IHasAbilities"/></exception>
        /// <exception cref="InvalidOperationException">If the actor does not have an ability which is or derives from <typeparamref name="T"/></exception>
        public static T GetAbility<T>(this ICanPerform actor) => (T) GetAbility(actor, typeof(T));

        /// <summary>Gets the first ability which the actor has of the specified type</summary>
        /// <param name="actor">The actor from whom to get the ability</param>
        /// <param name="abilityType">The type of ability desired</param>
        /// <returns>The ability instance</returns>
        /// <exception cref="ArgumentNullException">If any parameter is <see langword="null" /></exception>
        /// <exception cref="ArgumentException">If the actor does not implement <see cref="IHasAbilities"/></exception>
        /// <exception cref="InvalidOperationException">If the actor does not have an ability which is or derives from <paramref name="abilityType"/></exception>
        public static object GetAbility(this ICanPerform actor, Type abilityType)
        {
            if(actor is null) throw new ArgumentNullException(nameof(actor));
            if(abilityType is null) throw new ArgumentNullException(nameof(abilityType));

            if (!(actor is IHasAbilities abilityActor))
                throw new ArgumentException($"The actor must implement {nameof(IHasAbilities)}.", nameof(actor));
            
            return abilityActor.Abilities.FirstOrDefault(abilityType.IsInstanceOfType)
                ?? throw new InvalidOperationException($"{((IHasName) actor).Name} must have an ability of type {abilityType.FullName}");
        }

        /// <summary>Tries to get the first ability which the actor has of the specified type</summary>
        /// <param name="actor">The actor from whom to get the ability</param>
        /// <param name="ability">If this method returns <see langword="true" /> then this exposes the strongly-typed ability; if not then this value is undefined</param>
        /// <typeparam name="T">The type of ability desired</typeparam>
        /// <returns><see langword="true" /> if the actor has an ability of the specified type; <see langword="false" /> if not.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="actor"/> is <see langword="null" /></exception>
        /// <exception cref="ArgumentException">If the actor does not implement <see cref="IHasAbilities"/></exception>
        public static bool TryGetAbility<T>(this ICanPerform actor, out T ability)
        {
            ability = default;
            if (!TryGetAbility(actor, typeof(T), out var untypedAbility)) return false;
            ability = (T) untypedAbility;
            return true;
        }

        /// <summary>Gets the first ability which the actor has of the specified type</summary>
        /// <param name="actor">The actor from whom to get the ability</param>
        /// <param name="abilityType">The type of ability desired</param>
        /// <param name="ability">If this method returns <see langword="true" /> then this exposes the strongly-typed ability; if not then this value is undefined</param>
        /// <returns><see langword="true" /> if the actor has an ability of the specified type; <see langword="false" /> if not.</returns>
        /// <exception cref="ArgumentNullException">If any parameter is <see langword="null" /></exception>
        public static bool TryGetAbility(this ICanPerform actor, Type abilityType, out object ability)
        {
            if(actor is null) throw new ArgumentNullException(nameof(actor));
            if(abilityType is null) throw new ArgumentNullException(nameof(abilityType));

            if(!actor.HasAbility(abilityType))
            {
                ability = default;
                return false;
            }

            ability = actor.GetAbility(abilityType);
            return true;
        }

        /// <summary>Adds an ability to the specified actor</summary>
        /// <param name="actor">The actor from whom to get the ability</param>
        /// <param name="ability">The ability to add to the actor</param>
        /// <exception cref="ArgumentNullException">If any parameter is <see langword="null" /></exception>
        /// <exception cref="ArgumentException">If the actor does not implement <see cref="IHasAbilities"/></exception>
        /// <exception cref="InvalidOperationException">If the actor already has an ability of the same type as the
        /// <paramref name="ability"/> to add, or which derives from the same type</exception>
        public static void IsAbleTo(this ICanPerform actor, object ability)
        {
            if(actor is null) throw new ArgumentNullException(nameof(actor));
            if(ability is null) throw new ArgumentNullException(nameof(ability));

            if (!(actor is IHasAbilities abilityActor))
                throw new ArgumentException($"{((IHasName) actor).Name} must implement {nameof(IHasAbilities)}.", nameof(actor));
            abilityActor.IsAbleTo(ability);
        }

        /// <summary>Adds an ability to the specified actor, where the ability has a public parameterless constructor</summary>
        /// <remarks>
        /// <para>
        /// This method is a convenience for manually instantiating the ability instance and adding it to the actor in that manner.
        /// For abilities which do not have a public parameterless constructor, consider adding them to the actor via dependency injection.
        /// The recommended technique for accomplishing this is by implementing <see cref="IPersona"/> in a class of your own.
        /// Implementations of persona are eligible for dependency injection when the actor is retrieved from the persona type
        /// via the <see cref="ICast"/>: <see cref="CastExtensions.GetActor{TPersona}(ICast)"/>.
        /// </para>
        /// </remarks>
        /// <param name="actor">The actor from whom to get the ability</param>
        /// <typeparam name="TAbility">The type of the ability to add to the actor</typeparam>
        /// <exception cref="ArgumentNullException">If any parameter is <see langword="null" /></exception>
        /// <exception cref="ArgumentException">If the actor does not implement <see cref="IHasAbilities"/></exception>
        /// <exception cref="InvalidOperationException">If the actor already has an ability of the same type as the
        /// <typeparamref name="TAbility"/> to add, or which derives from the same type</exception>
        public static void IsAbleTo<TAbility>(this ICanPerform actor) where TAbility : new()
        {
            if(actor is null) throw new ArgumentNullException(nameof(actor));

            if (!(actor is IHasAbilities abilityActor))
                throw new ArgumentException($"{((IHasName) actor).Name} must implement {nameof(IHasAbilities)}.", nameof(actor));
            abilityActor.IsAbleTo(new TAbility());
        }
    }
}