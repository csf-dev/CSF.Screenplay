using System;
namespace CSF.Screenplay.Web.Models
{
  public struct Position : IEquatable<Position>
  {
    PixelDistance top, left;

    PixelDistance Top => top;
    PixelDistance Left => left;

    public override string ToString()
    {
      return $"top: {Top.ToString()}, left: {Left.ToString()}";
    }

    public override bool Equals(object obj)
    {
      if(obj is Position)
      {
        return Equals((Position) obj);
      }

      return false;
    }

    public bool Equals(Position other)
    {
      return Top.Equals(other.Top) && Left.Equals(other.Left);
    }

    public override int GetHashCode()
    {
      return Top.GetHashCode() ^ Left.GetHashCode();
    }

    public Position(PixelDistance top, PixelDistance left)
    {
      this.top = top;
      this.left = left;
    }

    public Position(decimal top, decimal left)
    {
      this.top = new PixelDistance(top);
      this.left = new PixelDistance(left);
    }
  }
}
