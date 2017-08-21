using System;
namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Represents metdata about a registered Screenplay service.
  /// </summary>
  public class ServiceMetadata : IEquatable<ServiceMetadata>
  {
    /// <summary>
    /// Gets the service type.
    /// </summary>
    /// <value>The type.</value>
    public Type Type { get; private set; }

    /// <summary>
    /// Gets the service name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the lifetime of the service registration.
    /// </summary>
    /// <value>The lifetime.</value>
    public ServiceLifetime Lifetime { get; private set; }

    /// <summary>
    /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:CSF.Screenplay.Scenarios.ServiceMetadata"/>.
    /// </summary>
    /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:CSF.Screenplay.Scenarios.ServiceMetadata"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
    /// <see cref="T:CSF.Screenplay.Scenarios.ServiceMetadata"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj)
    {
      return Equals(obj as ServiceMetadata);
    }

    /// <summary>
    /// Determines whether the specified <see cref="CSF.Screenplay.Scenarios.ServiceMetadata"/> is equal to the current <see cref="T:CSF.Screenplay.Scenarios.ServiceMetadata"/>.
    /// </summary>
    /// <param name="other">The <see cref="CSF.Screenplay.Scenarios.ServiceMetadata"/> to compare with the current <see cref="T:CSF.Screenplay.Scenarios.ServiceMetadata"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="CSF.Screenplay.Scenarios.ServiceMetadata"/> is equal to the current
    /// <see cref="T:CSF.Screenplay.Scenarios.ServiceMetadata"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(ServiceMetadata other)
    {
      if(ReferenceEquals(other, null))
        return false;
      if(ReferenceEquals(other, this))
        return true;

      return (other.Type == Type
              && other.Name == Name
              && (Lifetime == ServiceLifetime.Any
                  || other.Lifetime == ServiceLifetime.Any
                  || Lifetime == other.Lifetime));
    }

    /// <summary>
    /// Serves as a hash function for a <see cref="T:CSF.Screenplay.Scenarios.ServiceMetadata"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode()
    {
      var nameHash = (Name != null)? Name.GetHashCode() : 0;

      return Type.GetHashCode() ^ nameHash;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Scenarios.ServiceMetadata"/> class.
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="name">Name.</param>
    public ServiceMetadata(Type type, string name, ServiceLifetime lifetime)
    {
      lifetime.RequireDefinedValue(nameof(lifetime));
      if(type == null)
        throw new ArgumentNullException(nameof(type));

      Type = type;
      Name = name;
      Lifetime = lifetime;
    }
  }
}
