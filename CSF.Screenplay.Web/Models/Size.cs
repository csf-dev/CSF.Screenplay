using System;
namespace CSF.Screenplay.Web.Models
{
  public struct Size : IEquatable<Size>
  {
    PixelDistance height, width;

    PixelDistance Height => height;
    PixelDistance Width => width;

    public override string ToString()
    {
      return $"height: {Height.ToString()}, width: {Width.ToString()}";
    }

    public override bool Equals(object obj)
    {
      if(obj is Size)
      {
        return Equals((Size) obj);
      }

      return false;
    }

    public bool Equals(Size other)
    {
      return Height.Equals(other.Height) && Width.Equals(other.Width);
    }

    public override int GetHashCode()
    {
      return Height.GetHashCode() ^ Width.GetHashCode();
    }

    public Size(PixelDistance height, PixelDistance width)
    {
      this.height = height;
      this.width = width;
    }

    public Size(decimal height, decimal width)
    {
      this.height = new PixelDistance(height);
      this.width = new PixelDistance(width);
    }
  }
}
