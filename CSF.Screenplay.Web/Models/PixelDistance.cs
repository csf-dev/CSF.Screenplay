using System;
namespace CSF.Screenplay.Web.Models
{
  public struct PixelDistance : IEquatable<PixelDistance>
  {
    decimal distance;

    decimal Distance => distance;

    public override string ToString()
    {
      return $"{Distance}px";
    }

    public override bool Equals(object obj)
    {
      if(obj is PixelDistance)
      {
        return Equals((PixelDistance) obj);
      }

      return false;
    }

    public bool Equals(PixelDistance other)
    {
      return Distance == other.Distance;
    }

    public override int GetHashCode()
    {
      return Distance.GetHashCode();
    }

    public PixelDistance(decimal distance)
    {
      this.distance = distance;
    }
  }
}
