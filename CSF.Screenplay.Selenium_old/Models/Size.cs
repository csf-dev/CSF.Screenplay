using System;
namespace CSF.Screenplay.Selenium.Models
{
  /// <summary>
  /// A two-dimenstional set of pixel dimensions representing a two-dimensional size.
  /// </summary>
  public struct Size : IEquatable<Size>
  {
    PixelDistance height, width;

    /// <summary>
    /// Gets the height.
    /// </summary>
    /// <value>The height.</value>
    public PixelDistance Height => height;

    /// <summary>
    /// Gets the width.
    /// </summary>
    /// <value>The width.</value>
    public PixelDistance Width => width;

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current <see cref="Size"/>.
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="Size"/>.</returns>
    public override string ToString()
    {
      return $"height: {Height.ToString()}, width: {Width.ToString()}";
    }

    /// <summary>
    /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="Size"/>.
    /// </summary>
    /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="Size"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
    /// <see cref="Size"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj)
    {
      if(obj is Size)
      {
        return Equals((Size) obj);
      }

      return false;
    }

    /// <summary>
    /// Determines whether the specified <see cref="Size"/> is equal to the current <see cref="Size"/>.
    /// </summary>
    /// <param name="other">The <see cref="Size"/> to compare with the current <see cref="Size"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="Size"/> is equal to the current
    /// <see cref="Size"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(Size other)
    {
      return Height.Equals(other.Height) && Width.Equals(other.Width);
    }

    /// <summary>
    /// Serves as a hash function for a <see cref="Size"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode()
    {
      return Height.GetHashCode() ^ Width.GetHashCode();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Size"/> struct.
    /// </summary>
    /// <param name="height">Height.</param>
    /// <param name="width">Width.</param>
    public Size(PixelDistance height, PixelDistance width)
    {
      this.height = height;
      this.width = width;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Size"/> struct.
    /// </summary>
    /// <param name="height">Height.</param>
    /// <param name="width">Width.</param>
    public Size(int height, int width)
    {
      this.height = new PixelDistance(height);
      this.width = new PixelDistance(width);
    }
  }
}
