using System;
using System.Collections.Generic;

namespace CSF.Screenplay.Abilities
{
  /// <summary>
  /// Base type for implementations of <see cref="IAbility"/>.
  /// </summary>
  /// <remarks>
  /// <para>
  /// Implementors have three mandatory methods to override and implement, as well as one optional (the
  /// disposal method).
  /// </para>
  /// <para>
  /// Implementors of <see cref="IAbility"/> are not required to derive from this type, it is offered only as a
  /// convenience.
  /// </para>
  /// </remarks>
  public abstract class Ability : IAbility, IDisposable
  {
    /// <summary>
    /// Indicates whether or not the current ability instance is able to create actions of the indicated type.
    /// </summary>
    /// <returns><c>true</c>, if the indicates action type may be created by this ability, <c>false</c> otherwise.</returns>
    /// <typeparam name="TAction">The desired action type.</typeparam>
    public virtual bool CanProvideAction<TAction>()
    {
      return CanProvideAction(typeof(TAction));
    }

    /// <summary>
    /// Gets and returns an action instance of the desired type.
    /// </summary>
    /// <returns>An action instance.</returns>
    /// <typeparam name="TAction">The desired action type.</typeparam>
    public virtual TAction GetAction<TAction>()
    {
      return (TAction) GetAction(typeof(TAction));
    }

    /// <summary>
    /// Indicates whether or not the current ability instance is able to create actions of the indicated type.
    /// </summary>
    /// <returns><c>true</c>, if the indicates action type may be created by this ability, <c>false</c> otherwise.</returns>
    /// <param name="actionType">The desired action type.</param>
    public abstract bool CanProvideAction(Type actionType);

    /// <summary>
    /// Gets and returns an action instance of the desired type.
    /// </summary>
    /// <returns>An action instance.</returns>
    /// <param name="actionType">The desired action type.</param>
    public abstract object GetAction(Type actionType);

    /// <summary>
    /// Gets a collection indicating all of the <c>System.Type</c> of actions which may be created by the current
    /// ability instance.
    /// </summary>
    /// <returns>The action types.</returns>
    public abstract IEnumerable<Type> GetActionTypes();

    #region IDisposable Support

    bool disposed;

    /// <summary>
    /// Gets a value indicating whether this <see cref="Ability"/> is disposed.
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
        disposed = true;
      }
    }

    /// <summary>
    /// Releases unmanaged resources and performs other cleanup operations before the
    /// <see cref="Ability"/> is reclaimed by garbage collection.
    /// </summary>
    ~Ability()
    {
      Dispose(false);
    }

    void IDisposable.Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    #endregion
  }
}
