using System;
namespace CSF.Screenplay.Context
{
  /// <summary>
  /// Represents information about a registration within an <see cref="IScreenplayContext"/>.
  /// </summary>
  public class ServiceRegistration : IEquatable<ServiceRegistration>
  {
    /// <summary>
    /// Gets the registration name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the registered service type.
    /// </summary>
    /// <value>The type.</value>
    public Type Type { get; private set; }

    /// <summary>
    /// Gets the lifetime of the registered service.
    /// </summary>
    /// <value>The service lifetime.</value>
    public ServiceLifetime Lifetime { get; private set; }

    /// <summary>
    /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:CSF.Screenplay.Context.ServiceRegistration"/>.
    /// </summary>
    /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:CSF.Screenplay.Context.ServiceRegistration"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
    /// <see cref="T:CSF.Screenplay.Context.ServiceRegistration"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj)
    {
      return Equals(obj as ServiceRegistration);
    }

    /// <summary>
    /// Serves as a hash function for a <see cref="T:CSF.Screenplay.Context.ServiceRegistration"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode()
    {
      var nameHash = (Name != null)? Name.GetHashCode() : 0;
      var typeHash = (Type != null)? Type.GetHashCode() : 0;
      var lifetimeHash = Lifetime.GetHashCode();

      return (nameHash ^ typeHash ^ lifetimeHash);
    }

    /// <summary>
    /// Determines whether the specified <see cref="CSF.Screenplay.Context.ServiceRegistration"/> is equal to the
    /// current <see cref="T:CSF.Screenplay.Context.ServiceRegistration"/>.
    /// </summary>
    /// <param name="other">The <see cref="CSF.Screenplay.Context.ServiceRegistration"/> to compare with the current <see cref="T:CSF.Screenplay.Context.ServiceRegistration"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="CSF.Screenplay.Context.ServiceRegistration"/> is equal to the current
    /// <see cref="T:CSF.Screenplay.Context.ServiceRegistration"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(ServiceRegistration other)
    {
      if(ReferenceEquals(other, null))
        return false;

      if(ReferenceEquals(other, this))
        return true;

      return (Name == other.Name
              && Type == other.Type
              && Lifetime == other.Lifetime);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Context.ServiceRegistration"/> class.
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="name">Name.</param>
    /// <param name="lifetime">Lifetime.</param>
    public ServiceRegistration(Type type, string name = null, ServiceLifetime lifetime = ServiceLifetime.PerTestRun)
    {
      if(type == null)
        throw new ArgumentNullException(nameof(type));
      lifetime.RequireDefinedValue(nameof(lifetime));

      Type = type;
      Name = name;
      Lifetime = lifetime;
    }
  }
}
