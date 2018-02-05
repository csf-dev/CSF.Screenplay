using System;
namespace CSF.Screenplay.Selenium.Models
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
    /// <param name="otherBuilder">Other builder.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    internal static TimespanBuilder<T> Create<T>(int value, T otherBuilder)  where T : class
      => new TimespanBuilder<T>(value, otherBuilder);
  }
}
