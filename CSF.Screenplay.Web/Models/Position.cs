using System;
namespace CSF.Screenplay.Web.Models
{
  /// <summary>
  /// A two-dimenstional set of pixel coordinates representing a position on the screen.
  /// </summary>
  public struct Position : IEquatable<Position>
  {
    PixelDistance top, left;

    /// <summary>
    /// Gets the number of pixels from the top of the screen.
    /// </summary>
    /// <value>The top coordinate.</value>
    public PixelDistance Top => top;

    /// <summary>
    /// Gets the number of pixels from the left of the screen.
    /// </summary>
    /// <value>The left coordinate.</value>
    public PixelDistance Left => left;

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current <see cref="Position"/>.
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="Position"/>.</returns>
    public override string ToString()
    {
      return $"top: {Top.ToString()}, left: {Left.ToString()}";
    }

    /// <summary>
    /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="Position"/>.
    /// </summary>
    /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="Position"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
    /// <see cref="Position"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj)
    {
      if(obj is Position)
      {
        return Equals((Position) obj);
      }

      return false;
    }

    /// <summary>
    /// Determines whether the specified <see cref="Position"/> is equal to the current <see cref="Position"/>.
    /// </summary>
    /// <param name="other">The <see cref="Position"/> to compare with the current <see cref="Position"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="Position"/> is equal to the current
    /// <see cref="Position"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(Position other)
    {
      return Top.Equals(other.Top) && Left.Equals(other.Left);
    }

    /// <summary>
    /// Serves as a hash function for a <see cref="Position"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode()
    {
      return Top.GetHashCode() ^ Left.GetHashCode();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Position"/> struct.
    /// </summary>
    /// <param name="top">Top.</param>
    /// <param name="left">Left.</param>
    public Position(PixelDistance top, PixelDistance left)
    {
      this.top = top;
      this.left = left;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Position"/> struct.
    /// </summary>
    /// <param name="top">Top.</param>
    /// <param name="left">Left.</param>
    public Position(int top, int left)
    {
      this.top = new PixelDistance(top);
      this.left = new PixelDistance(left);
    }
  }
}
