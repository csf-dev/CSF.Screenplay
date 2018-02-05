using System;
namespace CSF.Screenplay.Selenium.Models
{
  /// <summary>
  /// An object which may provide a human-readable name for a 'target'.
  /// </summary>
  /// <seealso cref="ITarget"/>
  public interface IHasTargetName
  {
    /// <summary>
    /// Gets the human-readable target name.
    /// </summary>
    /// <returns>The name.</returns>
    string GetName();
  }
}
