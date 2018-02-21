using System;
using CSF.Zpt.Rendering;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Implementation of <see cref="ISourceInfo"/> which represents information from a named stream.
  /// </summary>
  public class StreamSourceInfo : ISourceInfo
  {
    readonly string name;

    /// <summary>
    /// Gets the full name of the source.
    /// </summary>
    /// <value>The full name.</value>
    public string FullName => name;

    /// <summary>
    /// Determines whether the specified <see cref="ISourceInfo"/> is equal to the
    /// current <see cref="StreamSourceInfo"/>.
    /// </summary>
    /// <param name="other">The <see cref="ISourceInfo"/> to compare with the current <see cref="StreamSourceInfo"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="ISourceInfo"/> is equal to the current
    /// <see cref="StreamSourceInfo"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(ISourceInfo other)
    {
      return (other is StreamSourceInfo) && ((StreamSourceInfo) other).FullName == FullName;
    }

    /// <summary>
    /// Gets a parent/container object for the current source info.  This implementation always returns <c>null</c>.
    /// </summary>
    /// <returns>The container.</returns>
    public object GetContainer() => null;

    /// <summary>
    /// Gets a name for the current source, relative to a given root.
    /// </summary>
    /// <returns>The relative name.</returns>
    /// <param name="root">Root.</param>
    public string GetRelativeName(string root) => FullName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.StreamSourceInfo"/> class.
    /// </summary>
    /// <param name="name">Name.</param>
    public StreamSourceInfo(string name)
    {
      if(name == null)
        throw new ArgumentNullException(nameof(name));

      this.name = name;
    }
  }
}
