using System;
namespace CSF.Screenplay.Web.Models
{
  /// <summary>
  /// A one-dimensional distance measured in screen pixels.
  /// </summary>
  public struct PixelDistance : IEquatable<PixelDistance>
  {
    int distance;

    /// <summary>
    /// Gets the number of pixels.
    /// </summary>
    /// <value>The number of pixels.</value>
    public int Distance => distance;

    /// <summary>
    /// Returns a <c>System.String</c> that represents the current <see cref="PixelDistance"/>.
    /// </summary>
    /// <returns>A <c>System.String</c> that represents the current <see cref="PixelDistance"/>.</returns>
    public override string ToString()
    {
      return $"{Distance}px";
    }

    /// <summary>
    /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="PixelDistance"/>.
    /// </summary>
    /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="PixelDistance"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
    /// <see cref="PixelDistance"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj)
    {
      if(obj is PixelDistance)
      {
        return Equals((PixelDistance) obj);
      }

      return false;
    }

    /// <summary>
    /// Determines whether the specified <see cref="PixelDistance"/> is equal to the current <see cref="PixelDistance"/>.
    /// </summary>
    /// <param name="other">The <see cref="PixelDistance"/> to compare with the current <see cref="PixelDistance"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="PixelDistance"/> is equal to the current
    /// <see cref="PixelDistance"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(PixelDistance other)
    {
      return Distance == other.Distance;
    }

    /// <summary>
    /// Serves as a hash function for a <see cref="PixelDistance"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode()
    {
      return Distance.GetHashCode();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PixelDistance"/> struct.
    /// </summary>
    /// <param name="distance">Distance.</param>
    public PixelDistance(int distance)
    {
      this.distance = distance;
    }
  }
}
