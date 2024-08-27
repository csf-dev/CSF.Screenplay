using System;
namespace CSF.Screenplay.ReportFormatting
{
  /// <summary>
  /// Base class for implementations of <see cref="IHasObjectFormattingStrategyInfo"/> which provides default
  /// strategy information based upon the type of the object to be formatted.
  /// </summary>
  public abstract class ObjectFormattingStrategy<T> : IHasObjectFormattingStrategyInfo
  {
    /// <summary>
    /// Gets a formatted string representing the given object.
    /// </summary>
    /// <param name="obj">Object.</param>
    public abstract string FormatForReport(T obj);

    /// <summary>
    /// Gets a formatted string representing for the given object.
    /// </summary>
    /// <param name="obj">The object to format.</param>
    public string FormatForReport(object obj) => FormatForReport((T) obj);

    /// <summary>
    /// Gets a priority for formatting an object which matches the generic type of this class.
    /// This should return a value greater than zero (since we already know that we can format the object).
    /// </summary>
    /// <remarks>
    /// <para>
    /// The default implementation returns 2 if the object is an exact type match for <typeparamref name="T"/>, or
    /// 1 if it simply derives from <typeparamref name="T"/>, or if it is <c>null</c>.
    /// </para>
    /// </remarks>
    /// <returns>The formatting priority.</returns>
    /// <param name="obj">Object.</param>
    public virtual int GetFormattingPriority(T obj)
    {
      if(ReferenceEquals(obj, null))
        return 1;

      if(obj.GetType() == typeof(T)) return 2;

      return 1;
    }

    /// <summary>
    /// Gets the priority by which this formatter should be used for formatting the given object.
    /// A higher numeric priority means that the formatter is more likely to be selected, where a lower priority
    /// means it is less likely.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If <see cref="CanFormat(object)"/> returns <c>false</c> then this will always return <c>Int32.MinValue</c>.
    /// Otherwise it will return the result of executing <see cref="GetFormattingPriority(T)"/>.
    /// </para>
    /// </remarks>
    /// <returns>The formatting priority.</returns>
    /// <param name="obj">The object to be formatted.</param>
    public int GetFormattingPriority(object obj)
    {
      if(!CanFormat(obj)) return int.MinValue;
      return GetFormattingPriority((T) obj);
    }

    /// <summary>
    /// Gets a value which indicates whether or not the current instance is capable of providing a format for the given
    /// object.  If it is <c>false</c> then this formatter must not be used for that object.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This implementation is controlled by two factors: <see cref="CanFormatNulls"/> and <see cref="CanFormat(T)"/>.
    /// If the <paramref name="obj"/> is <c>null</c> then the result of this method is determined by
    /// <see cref="CanFormatNulls"/>.  If not then if the object type does not derive from <typeparamref name="T"/>
    /// then the result will be <c>false</c>.  If it does derive from <typeparamref name="T"/> then the result of this
    /// method is determined by <see cref="CanFormat(T)"/>.
    /// </para>
    /// </remarks>
    /// <returns>
    /// <c>true</c>, if the current instance can format the given object, <c>false</c> otherwise.</returns>
    /// <param name="obj">The object to be formatted.</param>
    public bool CanFormat(object obj)
    {
      if(ReferenceEquals(obj, null)) return CanFormatNulls;

      if(!typeof(T).IsAssignableFrom(obj.GetType())) return false;

      return CanFormat((T) obj);
    }

    /// <summary>
    /// Gets a value indicating whether this formatting strategy can format a given object.
    /// </summary>
    /// <remarks>
    /// <para>
    /// By default this implementation returns <c>true</c>.
    /// </para>
    /// </remarks>
    /// <returns><c>true</c>, if the object can be formatted, <c>false</c> otherwise.</returns>
    /// <param name="obj">Object.</param>
    public virtual bool CanFormat(T obj) => true;

    /// <summary>
    /// Gets a value indicating whether this formatting strategy can format nulls.
    /// </summary>
    /// <remarks>
    /// <para>
    /// By default this implementation returns <c>false</c>.
    /// </para>
    /// </remarks>
    /// <value><c>true</c> if it can format nulls; otherwise, <c>false</c>.</value>
    public virtual bool CanFormatNulls => false;
  }
}
