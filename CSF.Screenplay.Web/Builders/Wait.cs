using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Queries;
using CSF.Screenplay.Web.Waits;

namespace CSF.Screenplay.Web.Builders
{
  /// <summary>
  /// Builder type which creates targetted waits.
  /// </summary>
  public class Wait
  {
    static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(10);

    TimeSpan timeout;
    ITarget target;

    /// <summary>
    /// Gets a builder instance for a wait with a specified timeout.
    /// </summary>
    /// <returns>A wait builder.</returns>
    /// <param name="timeout">Timeout.</param>
    public static Wait ForAtMost(TimeSpan timeout)
    {
      return new Wait
      {
        timeout = timeout,
      };
    }

    /// <summary>
    /// Gets a builder for a given target, with the default 10-second timeout.
    /// </summary>
    /// <returns>A wait builder.</returns>
    /// <param name="target">Target.</param>
    public static Wait For(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));
      
      return new Wait
      {
        target = target,
        timeout = DefaultTimeout,
      };
    }

    /// <summary>
    /// Gets a wait builder for a given target.
    /// </summary>
    /// <returns>A wait builder.</returns>
    /// <param name="target">Target.</param>
    public Wait Until(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));
      if(this.target != null)
        throw new InvalidOperationException("You may not choose the target more than once.");

      this.target = target;
      return this;
    }

    /// <summary>
    /// Gets a 'wait' performable which completes when a specified text string is displayed in the target.
    /// </summary>
    /// <returns>The the text.</returns>
    /// <param name="text">Text.</param>
    public IPerformable ShowsTheText(string text)
    {
      if(text == null)
        throw new ArgumentNullException(nameof(text));
      
      return GetTargettedWait(new TextQuery(), x => x == text);
    }

    /// <summary>
    /// Gets a 'wait' performable which completes when a specified text substring is displayed in the target.
    /// </summary>
    /// <returns>The the text.</returns>
    /// <param name="text">Text.</param>
    public IPerformable ContainsTheText(string text)
    {
      if(text == null)
        throw new ArgumentNullException(nameof(text));
      
      return GetTargettedWait(new TextQuery(), x => x != null && x.Contains(text));
    }

    IPerformable GetTargettedWait<T>(IQuery<T> query, Func<T,bool> predicate)
    {
      if(query == null)
        throw new ArgumentNullException(nameof(query));
      if(predicate == null)
        throw new ArgumentNullException(nameof(predicate));
      if(target == null)
        throw new InvalidOperationException("You must choose a target.");

      return new TargettedWait<T>(target, query, predicate, timeout);
    }
  }
}
