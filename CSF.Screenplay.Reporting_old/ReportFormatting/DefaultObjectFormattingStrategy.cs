using System;
namespace CSF.Screenplay.ReportFormatting
{
  /// <summary>
  /// A default object formatter which uses the object's <c>.ToString()</c> implementation.
  /// </summary>
  public class DefaultObjectFormattingStrategy : IHasObjectFormattingStrategyInfo
  {
    /// <summary>
    /// Gets a value which indicates whether or not the current instance is capable of providing a format for the given
    /// object.  If it is <c>false</c> then this formatter must not be used for that object.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This implementation will always return <c>true</c>.
    /// </para>
    /// </remarks>
    /// <returns>
    /// <c>true</c>, if the current instance can format the given object, <c>false</c> otherwise.</returns>
    /// <param name="obj">The object to be formatted.</param>
    public bool CanFormat(object obj) => true;

    /// <summary>
    /// Gets the priority by which this formatter should be used for formatting the given object.
    /// A higher numeric priority means that the formatter is more likely to be selected, where a lower priority
    /// means it is less likely.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This value is meaningless if <see cref="CanFormat(object)"/> returns <c>false</c> for the same object.
    /// This implementation will always return zero for any object.
    /// </para>
    /// </remarks>
    /// <returns>The formatting priority.</returns>
    /// <param name="obj">The object to be formatted.</param>
    public int GetFormattingPriority(object obj) => 0;

    /// <summary>
    /// Gets a formatted string representing the given object.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This implementation will always return the result of <c>Object.ToString()</c>, unless the input object is
    /// <c>null</c>, in which case it will return the string "&lt;null&gt;".
    /// </para>
    /// </remarks>
    /// <param name="obj">Object.</param>
    public string FormatForReport(object obj) => ReferenceEquals(obj, null)? "<null>" : obj.ToString();
  }
}
