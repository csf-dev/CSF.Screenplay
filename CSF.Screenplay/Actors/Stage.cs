using System;
namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// The stage is a concept which enables the use of "passive voice" in tests, such as
  /// "Given Joe has money // When he buys eggs".
  /// </summary>
  public class Stage : IStage
  {
    IActor currentActor;
    readonly ICast cast;

    /// <summary>
    /// Gets the actor which is currently "in the spotlight" and thus the subject in the context of the test logic.
    /// </summary>
    /// <returns>The actor in the spotlight.</returns>
    public IActor GetTheActorInTheSpotlight()
    {
      if(currentActor == null)
        throw new InvalidOperationException(Resources.ExceptionFormats.MustBeAnActorInTheSpotlight);
      return currentActor;
    }

    /// <summary>
    /// Shines the spotlight upon an actor, such that they will now be returned by <see cref="GetTheActorInTheSpotlight"/>.
    /// This essentially marks them the subject of future test logic.
    /// </summary>
    /// <returns>The actor who has just been placed into the spotlight.</returns>
    /// <typeparam name="TPersona">The persona type.</typeparam>
    public IActor ShineTheSpotlightOn<TPersona>() where TPersona : class,IPersona,new()
    {
      var actor = cast.Get<TPersona>();
      ShineTheSpotlightOn(actor);
      return actor;
    }

    /// <summary>
    /// Shines the spotlight upon an actor, such that they will now be returned by <see cref="GetTheActorInTheSpotlight"/>.
    /// This essentially marks them the subject of future test logic.
    /// </summary>
    /// <returns>The actor who has just been placed into the spotlight.</returns>
    /// <param name="persona">The persona.</param>
    public IActor ShineTheSpotlightOn(IPersona persona)
    {
      var actor = cast.Get(persona);
      ShineTheSpotlightOn(actor);
      return actor;
    }

    /// <summary>
    /// Shines the spotlight upon an actor, such that they will now be returned by <see cref="GetTheActorInTheSpotlight"/>.
    /// This essentially marks them the subject of future test logic.
    /// </summary>
    /// <returns>The actor who has just been placed into the spotlight.</returns>
    /// <param name="actorName">The actor name.</param>
    public IActor ShineTheSpotlightOn(string actorName)
    {
      var actor = cast.Get(actorName);
      ShineTheSpotlightOn(actor);
      return actor;
    }

    /// <summary>
    /// Removes the spotlight from the current actor and sets such that no actor has the spotlight.
    /// </summary>
    public void RemoveTheSpotlight() => ShineTheSpotlightOn((IActor) null);

    /// <summary>
    /// Shines the spotlight upon an actor, such that they will now be returned by <see cref="M:CSF.Screenplay.Actors.IStage.GetTheActorInTheSpotlight" />.
    /// This essentially marks them the subject of future test logic.
    /// </summary>
    /// <param name="actor">Actor.</param>
    public void ShineTheSpotlightOn(IActor actor) => currentActor = actor;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Actors.Stage"/> class.
    /// </summary>
    /// <param name="cast">Cast.</param>
    public Stage(ICast cast)
    {
      if(cast == null)
        throw new ArgumentNullException(nameof(cast));

      this.cast = cast;
    }
  }
}
