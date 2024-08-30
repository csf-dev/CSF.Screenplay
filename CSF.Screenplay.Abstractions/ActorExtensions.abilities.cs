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
                ?? throw new InvalidOperationException($"{DefaultStrings.FormatValue(actor)} must have an ability of type {abilityType.FullName}");
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
                throw new ArgumentException($"{DefaultStrings.FormatValue(actor)} must implement {nameof(IHasAbilities)}.", nameof(actor));
            abilityActor.IsAbleTo(ability);
        }
    }
}