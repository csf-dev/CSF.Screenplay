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
    /// Gets a single actor by their name.
    /// </summary>
    /// <returns>The named actor, or a <c>null</c> reference if no such actor is contained in the current instance.</returns>
    /// <param name="name">The actor name.</param>
    public virtual IActor GetActor(string name)
    {
      IActor output;
      if(Actors.TryGetValue(name, out output))
      {
        return output;
      }
      return null;
    }

    /// <summary>
    /// Gets a single actor by their name, creating them if they do not already exist in the cast.
    /// </summary>
    /// <returns>The named actor, which might be a newly-created actor.</returns>
    /// <param name="name">The actor name.</param>
    public virtual IActor GetOrAdd(string name)
    {
      IActor output;
      if(Actors.TryGetValue(name, out output))
      {
        return output;
      }

      return Add(name);
    }

    /// <summary>
    /// Creates a new actor of the given name, adds it to the current cast instance and returns it.
    /// </summary>
    /// <returns>The created actor.</returns>
    /// <param name="name">The actor name.</param>
    public virtual IActor Add(string name)
    {
      var actor = CreateActor(name);
      ConfigureNewActor(actor);
      Add(actor);
      return actor;
    }

    /// <summary>
    /// Adds the given actor to the current cast instance.
    /// </summary>
    /// <param name="actor">An actor.</param>
    public virtual void Add(IActor actor)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));

      var name = actor.Name;
      if(name == null)
        throw new ArgumentException("Actors must have a name", nameof(actor));


      if(actors.ContainsKey(name))
        throw new DuplicateActorException($"There is already an actor named '{name}' contained within the current {typeof(Cast).Name}. Duplicates are not permitted.");
      
      Actors.Add(name, actor);
    }

    /// <summary>
    /// Clears the current cast.
    /// </summary>
    public virtual void Clear()
    {
      actors.Clear();
    }

    /// <summary>
    /// Gets or sets a callback which is applied to all newly-created actors after they are created.
    /// </summary>
    /// <value>The new actor callback.</value>
    public virtual Action<IActor> NewActorCallback { get; set; }

    /// <summary>
    /// Creates and returns a new object which implements <see cref="IActor"/>.
    /// </summary>
    /// <returns>The actor.</returns>
    /// <param name="name">The actor's name.</param>
    protected virtual IActor CreateActor(string name)
    {
      return new Actor(name);
    }

    /// <summary>
    /// Configures a newly-created actor.
    /// </summary>
    /// <param name="actor">Actor.</param>
    protected virtual void ConfigureNewActor(IActor actor)
    {
      if(NewActorCallback != null)
        NewActorCallback(actor);
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="Cast"/> class.
    /// </summary>
    public Cast()
    {
      actors = new Dictionary<string,IActor>();
    }

    #endregion
  }
}
