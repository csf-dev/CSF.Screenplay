using System;
namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Base class for implementations of <see cref="IObjectFormatter"/>.
  /// </summary>
  public abstract class GenericObjectFormatter<T> : IObjectFormatter
  {
    /// <summary>
    /// Gets a formatted name for the given input.
    /// </summary>
    /// <param name="obj">Object.</param>
    protected abstract string GetFormattedName(T obj);

    /// <summary>
    /// Gets a priority for formatting an object which matches the generic type of this class.
    /// This should return a value greater than zero.
    /// </summary>
    /// <returns>The formatting priority.</returns>
    /// <param name="obj">Object.</param>
    protected virtual int GetFormattingPriority(T obj) => 1;

    /// <summary>
    /// Gets a priority for formatting an object.  A formatter which returns a higher priority will be used over one
    /// which returns a lower priority.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It is advised to return a negative priority (-1 is fine) if the formatter cannot format the given
    /// object.  Otherwise return a priority of 1 or higher depending upon how likely the formatter is to
    /// provide a useful result.
    /// </para>
    /// <para>
    /// Priority zero should be considered reserved for the <see cref="T:CSF.Screenplay.Reporting.DefaultObjectFormatter" />.
    /// </para>
    /// </remarks>
    /// <returns>The formatting priority.</returns>
    /// <param name="obj">Object.</param>
    public virtual int GetFormattingPriority(object obj)
    {
      if(ReferenceEquals(obj, null))
        return -1;

      var objectType = obj.GetType();

      if(!typeof(T).IsAssignableFrom(objectType))
        return -2;

      return GetFormattingPriority((T) obj);
    }

    string IObjectFormatter.GetFormattedName(object obj) => GetFormattedName((T) obj);
  }
}
