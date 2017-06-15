using System;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Represents a service which receives reporting information relating to what the actor(s) in the scenario are
  /// doing.
  /// </summary>
  public interface IReporter
  {
    /// <summary>
    /// Reports that an actor has gained an ability.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="ability">The ability.</param>
    void GainAbility(INamed actor, IAbility ability);

    /// <summary>
    /// Reports that a performable item has begun.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    void Begin(INamed actor, IPerformable performable);

    /// <summary>
    /// Reports that a performable item has completed successfully.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    void Success(INamed actor, IPerformable performable);

    /// <summary>
    /// Reports that a performable item has produced a result.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="result">The result produced.</param>
    void Result(INamed actor, IPerformableWithResult performable, object result);

    /// <summary>
    /// Reports that a performable item has failed and possible terminated early.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    void Failure(INamed actor, IPerformable performable);

    /// <summary>
    /// Reports that a performable item has failed and possible terminated early.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="exception">An exception encountered whilst attempting to perform the item.</param>
    void Failure(INamed actor, IPerformable performable, Exception exception);
  }
}
