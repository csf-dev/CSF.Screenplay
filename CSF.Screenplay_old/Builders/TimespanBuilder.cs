using System;
namespace CSF.Screenplay.Builders
{
  /// <summary>
  /// Static helper class for creating timespan builders.
  /// </summary>
  public static class TimespanBuilder
  {
    /// <summary>
    /// Static factory method creates instances of timespan builder.
    /// </summary>
    /// <param name="value">The timeout value.</param>
    /// <param name="otherBuilder">Another builder type.</param>
    /// <typeparam name="T">The type of the other builder.</typeparam>
    public static TimespanBuilder<T> Create<T>(int value, T otherBuilder)  where T : class
      => new TimespanBuilder<T>(value, otherBuilder);
  }
}
