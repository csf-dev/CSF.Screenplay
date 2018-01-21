using System;
namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// The stage is a concept which enables the use of "passive voice" in tests, such as
  /// "Given Joe has money // When he buys eggs".
  /// </summary>
  public interface IStage
  {
    /// <summary>
    /// Gets the actor which is currently "in the spotlight" and thus the subject in the context of the test logic.
    /// </summary>
    /// <returns>The actor in the spotlight.</returns>
    IActor GetTheActorInTheSpotlight();

    /// <summary>
    /// Shines the spotlight upon an actor, such that they will now be returned by <see cref="GetTheActorInTheSpotlight"/>.
    /// This essentially marks them the subject of future test logic.
    /// </summary>
    /// <param name="actor">Actor.</param>
    void ShineTheSpotlightOn(IActor actor);

    /// <summary>
    /// Removes the spotlight from the current actor and sets such that no actor has the spotlight.
    /// </summary>
    void RemoveTheSpotlight();
  }
}
