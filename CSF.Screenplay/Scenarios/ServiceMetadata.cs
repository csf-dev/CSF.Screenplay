using System;
namespace CSF.Screenplay.Scenarios
{
  public class ServiceMetadata : IEquatable<ServiceMetadata>
  {
    public Type Type { get; private set; }
    public string Name { get; private set; }

    public override bool Equals(object obj)
    {
      return Equals(obj as ServiceMetadata);
    }

    public bool Equals(ServiceMetadata other)
    {
      if(ReferenceEquals(other, null))
        return false;
      if(ReferenceEquals(other, this))
        return true;

      return other.Type == Type && other.Name == Name;
    }

    public override int GetHashCode()
    {
      var nameHash = (Name != null)? Name.GetHashCode() : 0;

      return Type.GetHashCode() ^ nameHash;
    }

    public ServiceMetadata(Type type, string name)
    {
      if(type == null)
        throw new ArgumentNullException(nameof(type));

      Type = type;
      Name = name;
    }
  }
}
