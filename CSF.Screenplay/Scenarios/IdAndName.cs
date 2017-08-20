using System;
namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Represents an unique identifier and a corresponding human-readable name.
  /// </summary>
  public class IdAndName : IEquatable<IdAndName>
  {
    /// <summary>
    /// Gets the unique identifier.
    /// </summary>
    /// <value>The identity.</value>
    public string Identity { get; private set; }

    /// <summary>
    /// Gets the human-readable name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; private set; }

    /// <summary>
    /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:CSF.Screenplay.Scenarios.IdAndName"/>.
    /// </summary>
    /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:CSF.Screenplay.Scenarios.IdAndName"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
    /// <see cref="T:CSF.Screenplay.Scenarios.IdAndName"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj)
    {
      return Equals(obj as IdAndName);
    }

    /// <summary>
    /// Determines whether the specified <see cref="CSF.Screenplay.Scenarios.IdAndName"/> is equal to the current <see cref="T:CSF.Screenplay.Scenarios.IdAndName"/>.
    /// </summary>
    /// <param name="other">The <see cref="CSF.Screenplay.Scenarios.IdAndName"/> to compare with the current <see cref="T:CSF.Screenplay.Scenarios.IdAndName"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="CSF.Screenplay.Scenarios.IdAndName"/> is equal to the current
    /// <see cref="T:CSF.Screenplay.Scenarios.IdAndName"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(IdAndName other)
    {
      if(ReferenceEquals(other, null))
        return false;
      if(ReferenceEquals(other, this))
        return true;

      return other.Identity == Identity && other.Name == Name;
    }

    /// <summary>
    /// Serves as a hash function for a <see cref="T:CSF.Screenplay.Scenarios.IdAndName"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode()
    {
      var nameHash = (Name != null)? Name.GetHashCode() : 0;
      return Identity.GetHashCode() ^ nameHash;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Scenarios.IdAndName"/> class.
    /// </summary>
    /// <param name="identity">Identity.</param>
    /// <param name="name">Name.</param>
    public IdAndName(string identity, string name = null)
    {
      if(identity == null)
        throw new ArgumentNullException(nameof(identity));

      Identity = identity;
      Name = name;
    }
  }
}
