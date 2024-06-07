using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Abilities
{
  /// <summary>
  /// Implementation of <see cref="IAbilityStore"/> which uses a generic <c>ISet</c> to hold the abilities.
  /// </summary>
  public class AbilityStore : IAbilityStore
  {
    readonly ISet<IAbility> abilities;

    /// <summary>
    /// Determines whether or not the current instance contains an ability or not.
    /// </summary>
    /// <returns><c>true</c>, if the current instance has the ability, <c>false</c> otherwise.</returns>
    /// <typeparam name="TAbility">The desired ability type.</typeparam>
    public virtual bool HasAbility<TAbility>() where TAbility : IAbility
    {
      return abilities.Any(x => AbilityImplementsType(x, typeof(TAbility)));
    }

    /// <summary>
    /// Gets an ability of the noted type.
    /// </summary>
    /// <returns>The ability.</returns>
    /// <typeparam name="TAbility">The desired ability type.</typeparam>
    public virtual TAbility GetAbility<TAbility>() where TAbility : IAbility
    {
      return (TAbility) abilities.FirstOrDefault(x => AbilityImplementsType(x, typeof(TAbility)));
    }

    /// <summary>
    /// Adds an ability to the current instance.
    /// </summary>
    /// <param name="ability">The ability.</param>
    public virtual void Add(IAbility ability)
    {
      if(ability == null)
        throw new ArgumentNullException(nameof(ability));

      abilities.Add(ability);
    }

    /// <summary>
    /// Instantiates an ability of the given type, adds it to the current store instance and returns the created ability.
    /// </summary>
    /// <param name="abilityType">The ability type.</param>
    public virtual IAbility Add(Type abilityType)
    {
      if(!typeof(IAbility).IsAssignableFrom(abilityType))
      {
        var message = String.Format(Resources.ExceptionFormats.AbilityMustImplementType, typeof(IAbility).Name);
        throw new ArgumentException(message, nameof(abilityType));
      }

      var ability = (IAbility) Activator.CreateInstance(abilityType);
      Add(ability);
      return ability;
    }

    bool AbilityImplementsType(IAbility ability, Type desiredType)
    {
      var abilityType = GetAbilityType(ability);
      if(abilityType == null)
        return false;

      return desiredType.IsAssignableFrom(abilityType);
    }

    Type GetAbilityType(IAbility ability)
    {
      if(ReferenceEquals(ability, null))
        return null;

      return ability.GetType();
    }

    #region IDisposable implementation

    bool disposed;

    /// <summary>
    /// Gets a value indicating whether this <see cref="AbilityStore"/> is disposed.
    /// </summary>
    /// <value><c>true</c> if disposed; otherwise, <c>false</c>.</value>
    protected bool Disposed => disposed;

    /// <summary>
    /// Performs disposal of the current instance.
    /// </summary>
    /// <param name="disposing">If set to <c>true</c> then we are explicitly disposing.</param>
    protected virtual void Dispose(bool disposing)
    {
      if(!disposed)
      {
        if(disposing)
        {
          foreach(var ability in abilities)
          {
            ability.Dispose();
          }
        }

        disposed = true;
      }
    }

    void IDisposable.Dispose()
    {
      Dispose(true);
    }

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="AbilityStore"/> class.
    /// </summary>
    public AbilityStore()
    {
      abilities = new HashSet<IAbility>();
    }
  }
}
