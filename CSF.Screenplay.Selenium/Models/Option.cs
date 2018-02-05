using System;
namespace CSF.Screenplay.Web.Models
{
  /// <summary>
  /// Represents a single option item within an HTML <c>&lt;select&gt;</c> element.
  /// </summary>
  public class Option : IEquatable<Option>
  {
    /// <summary>
    /// Gets the option text.
    /// </summary>
    /// <value>The text.</value>
    public string Text { get; private set; }

    /// <summary>
    /// Gets the option value.
    /// </summary>
    /// <value>The value.</value>
    public string Value { get; private set; }

    /// <summary>
    /// Serves as a hash function for a <see cref="Option"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode()
    {
      var textHash = (Text?? String.Empty).GetHashCode();
      var valueHash = (Value?? String.Empty).GetHashCode();

      return textHash ^ valueHash;
    }

    /// <summary>
    /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="Option"/>.
    /// </summary>
    /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="Option"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
    /// <see cref="Option"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj)
    {
      if(ReferenceEquals(this, obj))
        return true;

      var other = obj as Option;

      if(ReferenceEquals(other, null))
        return false;

      return Equals(this, other);
    }

    /// <summary>
    /// Determines whether the specified <see cref="Option"/> is equal to the current <see cref="Option"/>.
    /// </summary>
    /// <param name="obj">The <see cref="Option"/> to compare with the current <see cref="Option"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="Option"/> is equal to the current
    /// <see cref="Option"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(Option obj)
    {
      if(ReferenceEquals(obj, null))
        return false;

      return this.Text == obj.Text && this.Value == obj.Value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Option"/> class.
    /// </summary>
    /// <param name="text">The option text.</param>
    /// <param name="value">The option value.</param>
    public Option(string text, string value)
    {
      Text = text;
      Value = value;
    }
  }
}
