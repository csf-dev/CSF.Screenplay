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
  public class Cast : IDisposable
  {
    #region fields

    readonly IDictionary<string,Actor> actors;

    #endregion

    #region public API

    /// <summary>
    /// Gets a collection of all of the actors contained within the current instance.
    /// </summary>
    /// <returns>A collection of actors.</returns>
    public IEnumerable<Actor> GetAll() => actors.Values.ToArray();

    /// <summary>
    /// Gets a single actor by their name.
    /// </summary>
    /// <returns>The named actor, or a <c>null</c> reference if no such actor is contained in the current instance.</returns>
    /// <param name="name">The actor name.</param>
    public Actor GetActor(string name)
    {
      Actor output;
      if(actors.TryGetValue(name, out output))
      {
        return output;
      }
      return null;
    }

    /// <summary>
    /// Creates a new actor of the given name, adds it to the current cast instance and returns it.
    /// </summary>
    /// <returns>The created actor.</returns>
    /// <param name="name">The actor name.</param>
    public Actor NewActor(string name)
    {
      var output = new Actor(name);
      actors.Add(name, output);
      return output;
    }

    #endregion

    #region IDisposable Support

    bool disposed;

    /// <summary>
    /// Disposes of the current instance, which in turn disposes of all of the contained actors.
    /// </summary>
    /// <param name="disposing">If set to <c>true</c> disposing.</param>
    protected virtual void Dispose(bool disposing)
    {
      if(!disposed)
      {
        if(disposing)
        {
          foreach(IDisposable actor in actors.Values)
          {
            actor.Dispose();
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

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="Cast"/> class.
    /// </summary>
    public Cast()
    {
      actors = new Dictionary<string,Actor>();
    }

    #endregion
  }
}
