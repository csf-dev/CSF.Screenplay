using System;
namespace CSF.Screenplay.Web.Models
{
  public class Option : IEquatable<Option>
  {
    public string Text { get; private set; }
    public string Value { get; private set; }

    public override int GetHashCode()
    {
      var textHash = (Text?? String.Empty).GetHashCode();
      var valueHash = (Value?? String.Empty).GetHashCode();

      return textHash ^ valueHash;
    }

    public override bool Equals(object obj)
    {
      if(ReferenceEquals(this, obj))
        return true;

      var other = obj as Option;

      if(ReferenceEquals(other, null))
        return false;

      return Equals(this, other);
    }

    public bool Equals(Option obj)
    {
      if(ReferenceEquals(obj, null))
        return false;

      return this.Text == obj.Text && this.Value == obj.Value;
    }

    public Option(string text, string value)
    {
      Text = text;
      Value = value;
    }
  }
}
