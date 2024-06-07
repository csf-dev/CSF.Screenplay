using System;
namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Represents any actor or performer which can provide information about its name.
  /// </summary>
  public interface INamed
  {
    /// <summary>
    /// Gets the name of the actor.
    /// </summary>
    /// <value>The name.</value>
    string Name { get; }
  }
}
