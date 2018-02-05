using System;
using System.Collections.Generic;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Models;

namespace CSF.Screenplay.Selenium.Waits
{
  /// <summary>
  /// Represents a wait which operates upon the state of an <see cref="ITarget"/> .
  /// </summary>
  public interface ITargettedWait : IPerformable
  {
    /// <summary>
    /// Gets a collection of <c>System.Type</c> representing exception types which will be ignored during the wait
    /// operation.
    /// </summary>
    /// <value>The ignored exception types.</value>
    ISet<Type> IgnoredExceptionTypes { get; }
  }
}
