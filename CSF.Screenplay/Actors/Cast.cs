using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// A cast is a container for many actors.  It may be useful in scenarios in which many actors are involved.
  /// </summary>
  /// <remarks>
  /// <para>
  /// There is no need to make use of a <see cref="Cast"/> instance if there is no need.  If you need only use a
  /// single actor then it is far easier to stick with just one.  For testing scenarios in which multiple actors are
  /// involved though, making all of those actor instances available across multiple classes can be difficult.
  /// </para>
  /// <para>
  /// This is where the <see cref="Cast"/> object becomes useful.
  /// </para>
  /// </remarks>
  public class Cast : ICast
  {
    #region fields

    readonly IDictionary<string,IActor> actors;
    readonly object syncRoot;
    readonly Guid scenarioIdentity;

    #endregion

    #region properties

    /// <summary>
    /// Gets a collection of the currently-available actors.
    /// </summary>
    /// <value>The actors.</value>
    protected IDictionary<string,IActor> Actors => actors;

    #endregion

    #region public API

    /// <summary>
    /// Gets a collection of all of the actors contained within the current instance.
    /// </summary>
    /// <returns>A collection of actors.</returns>
    public virtual IEnumerable<IActor> GetAll() => Actors.Values.ToArray();

    /// <summary>
    /// Gets a value which indicates whether or not the current instance has an actor of the given name.
    /// </summary>
    /// <returns><c>true</c>, if an actor of the specified name exists in the current cast, <c>false</c> otherwise.</returns>
    /// <param name="name">The name for which to search.</param>
    public virtual bool HasActor(string name) => Actors.ContainsKey(name);

    /// <summary>
    /// Gets a single actor by their name.
    /// </summary>
    /// <returns>The named actor, or a <c>null</c> reference if no such actor is contained in the current instance.</returns>
    /// <param name="name">The actor name.</param>
    public virtual IActor GetExisting(string name)
    {
      lock(syncRoot)
      {
        return GetActorLocked(name);
      }
    }

    /// <summary>
    /// Gets a single actor by their name, creating them if they do not already exist in the cast.
    /// If this operation leads to the creation of a new actor then it will fire both
    /// <see cref="ActorCreated"/> and then <see cref="ActorAdded"/>.
    /// </summary>
    /// <returns>The named actor, which might be a newly-created actor.</returns>
    /// <param name="name">The actor name.</param>
    public virtual IActor Get(string name) => Get(name, null);

    /// <summary>
    /// Gets a single actor by their name, creating them if they do not already exist in the cast.
    /// If this operation leads to the creation of a new actor then it will fire both
    /// <see cref="ActorCreated"/> and then <see cref="ActorAdded"/>.
    /// </summary>
    /// <returns>The named actor, which might be a newly-created actor.</returns>
    /// <param name="name">The actor name.</param>
    /// <param name="createCustomisation">If the actor does not yet exist, then this action will be executed to customise the newly-created actor.</param>
    public virtual IActor Get(string name, Action<IActor> createCustomisation)
    {
      IActor actor;

      lock(syncRoot)
      {
        actor = GetActorLocked(name);
        if(actor != null)
          return actor;

        actor = CreateAndAddLocked(name);
      }

      if(createCustomisation != null)
        createCustomisation(actor);
      
      return actor;
    }

    /// <summary>
    /// Creates a new actor of the given name and adds it to the current cast instance.
    /// This operation will fire both <see cref="ActorCreated"/> and then <see cref="ActorAdded"/>.
    /// </summary>
    /// <param name="name">The actor name.</param>
    public virtual void Add(string name)
    {
      lock(syncRoot)
      {
        CreateAndAddLocked(name);
      }
    }

    /// <summary>
    /// Adds the given actor to the current cast instance.
    /// </summary>
    /// <param name="actor">An actor.</param>
    public virtual void Add(IActor actor)
    {
      lock(syncRoot)
      {
        AddLocked(actor);
      }
    }

    /// <summary>
    /// Clears the current cast.
    /// </summary>
    public virtual void Dismiss()
    {
      lock(syncRoot)
      {
        Actors.Clear();
      }
    }

    /// <summary>
    /// Creates and returns a new object which implements <see cref="IActor"/>.
    /// </summary>
    /// <returns>The actor.</returns>
    /// <param name="name">The actor's name.</param>
    protected virtual IActor CreateActor(string name)
    {
      var actor = new Actor(name, scenarioIdentity);
      OnActorCreated(actor);
      return actor;
    }

    IActor GetActorLocked(string name)
    {
      IActor output;
      if(Actors.TryGetValue(name, out output))
      {
        return output;
      }
      return null;
    }

    IActor CreateAndAddLocked(string name)
    {
      var actor = CreateActor(name);
      AddLocked(actor);
      return actor;
    }

    void AddLocked(IActor actor)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));

      var name = actor.Name;
      if(name == null)
        throw new ArgumentException(Resources.ExceptionFormats.ActorMustHaveAName, nameof(actor));

      if(Actors.ContainsKey(name))
      {
        var message = String.Format(Resources.ExceptionFormats.DuplicateActorsNotAllowedInCast,
                                    name,
                                    typeof(Cast).Name);
        throw new DuplicateActorException(message);
      }

      Actors.Add(name, actor);
      OnActorAdded(actor);
    }

    #endregion

    #region events and invokers

    /// <summary>
    /// An event which is triggered any time a new actor is created by the current cast.
    /// Fires before <see cref="ActorAdded"/>.
    /// </summary>
    public event EventHandler<ActorEventArgs> ActorCreated;

    /// <summary>
    /// Event invoker for the <see cref="ActorCreated"/> event.
    /// </summary>
    /// <param name="actor">Actor.</param>
    protected virtual void OnActorCreated(IActor actor)
    {
      var args = new ActorEventArgs(actor);
      ActorCreated?.Invoke(this, args);
    }

    /// <summary>
    /// An event which is triggered any time a new actor is added to the current cast.
    /// Where an actor is created then added, this event fires after <see cref="ActorCreated"/>.
    /// </summary>
    public event EventHandler<ActorEventArgs> ActorAdded;

    /// <summary>
    /// Event invoker for the <see cref="ActorAdded"/> event.
    /// </summary>
    /// <param name="actor">Actor.</param>
    protected virtual void OnActorAdded(IActor actor)
    {
      var args = new ActorEventArgs(actor);
      ActorAdded?.Invoke(this, args);
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="Cast"/> class.
    /// </summary>
    /// <param name="scenarioIdentity">The identity of the current scenario.</param>
    public Cast(Guid scenarioIdentity)
    {
      if(scenarioIdentity == Guid.Empty)
        throw new ArgumentException(Resources.ExceptionFormats.ActorMustHaveAScenarioId, nameof(scenarioIdentity));

      this.scenarioIdentity = scenarioIdentity;
      syncRoot = new Object();
      actors = new Dictionary<string,IActor>();
    }

    #endregion
  }
}
