using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Actors
{
  public class Cast : IDisposable
  {
    #region fields

    readonly IDictionary<string,Actor> actors;

    #endregion

    #region public API

    public IEnumerable<Actor> GetAll() => actors.Values.ToArray();

    public Actor GetActor(string name)
    {
      Actor output;
      if(actors.TryGetValue(name, out output))
      {
        return output;
      }
      return null;
    }

    public Actor NewActor(string name)
    {
      var output = new Actor(name);
      actors.Add(name, output);
      return output;
    }

    #endregion

    #region IDisposable Support

    bool disposed;

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

    public Cast()
    {
      actors = new Dictionary<string,Actor>();
    }

    #endregion
  }
}
