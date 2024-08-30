namespace CSF.Screenplay.Performables
{
  /// <summary>
  /// Static helper class for creating instances of <see cref="TimeSpanBuilder{TOtherBuilder}"/>.
  /// </summary>
  /// <remarks>
  /// <para>
  /// See the documentation for <see cref="TimeSpanBuilder{TOtherBuilder}"/> for more information
  /// about how this class is to be used.
  /// </para>
  /// </remarks>
  public static class TimeSpanBuilder
  {
    /// <summary>
    /// Creates and returns a <see cref="TimeSpanBuilder{TOtherBuilder}"/> which can hold time span information
    /// and then continue the building process associated with the other builder.
    /// </summary>
    /// <remarks>
    /// <para>
    /// See the documentation for <see cref="TimeSpanBuilder{TOtherBuilder}"/> for more information
    /// about how this method is to be used.
    /// </para>
    /// </remarks>
    /// <param name="otherBuilder">An instance of another performable builder</param>
    /// <param name="value">The absolute time span value, without any units</param>
    /// <typeparam name="TOtherBuilder">The type of the other performable builder</typeparam>
    public static TimeSpanBuilder<TOtherBuilder> Create<TOtherBuilder>(TOtherBuilder otherBuilder, int value)  where TOtherBuilder : class
      => new TimeSpanBuilder<TOtherBuilder>(otherBuilder, value);
  }
}
