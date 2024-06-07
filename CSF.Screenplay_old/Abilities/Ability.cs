using System;
using System.Collections.Generic;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Reporting;

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
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected virtual string GetReport(INamed actor)
    {
      return $"{actor.Name} is able to {GetType().Name}";
    }

    string IProvidesReport.GetReport(INamed actor)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));
      
      return GetReport(actor);
    }

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
