using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay
{
    public partial class Actor : IHasAbilities
    {
        readonly HashSet<object> abilities = new HashSet<object>();
        readonly object abilitySyncRoot = new object();

        /// <summary>Gets a collection of the actor's abilities</summary>
        protected virtual HashSet<object> Abilities => abilities;

        IReadOnlyCollection<object> IHasAbilities.Abilities => Abilities;

        /// <summary>Adds a new ability to the actor</summary>
        /// <param name="ability">The ability to add</param>
        /// <exception cref="ArgumentNullException">If the ability is <see langword="null" /></exception>
        /// <exception cref="InvalidOperationException">If the actor already has an ability of this type or a derived type</exception>
        protected virtual void IsAbleTo(object ability)
        {
            if (ability is null) throw new ArgumentNullException(nameof(ability));

            var abilityType = ability.GetType();

            lock (abilitySyncRoot)
            {
                if (abilities.Any(abilityType.IsInstanceOfType))
                    throw new InvalidOperationException($"{name} must not have any abilities which derive from {abilityType.FullName}.");
                abilities.Add(ability);
            }

            InvokeGainedAbility(ability);
        }

        void IHasAbilities.IsAbleTo(object ability) => IsAbleTo(ability);
    }
}