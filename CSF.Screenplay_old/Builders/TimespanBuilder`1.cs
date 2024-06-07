using System;

namespace CSF.Screenplay.Builders
{
  /// <summary>
  /// Builder for <c>System.TimeSpan</c> instances.
  /// </summary>
  public class TimespanBuilder<TBuilder> : IProvidesTimespan
    where TBuilder : class
  {
    readonly TBuilder otherBuilder;
    readonly int value;
    TimeSpan timespan;

    TimeSpan IProvidesTimespan.GetTimespan() => timespan;

    /// <summary>
    /// Sets the timespan to be measures in milliseconds and returns the contained builder.
    /// </summary>
    public TBuilder Milliseconds()
    {
      timespan = TimeSpan.FromMilliseconds(value);
      return otherBuilder;
    }

    /// <summary>
    /// Sets the timespan to be measures in seconds and returns the contained builder.
    /// </summary>
    public TBuilder Seconds()
    {
      timespan = TimeSpan.FromSeconds(value);
      return otherBuilder;
    }

    /// <summary>
    /// Sets the timespan to be measures in minutes and returns the contained builder.
    /// </summary>
    public TBuilder Minutes()
    {
      timespan = TimeSpan.FromMinutes(value);
      return otherBuilder;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:TimespanBuilder{TBuilder}"/> class.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <param name="otherBuilder">Other builder.</param>
    internal TimespanBuilder(int value, TBuilder otherBuilder)
    {
      if(otherBuilder == null)
        throw new ArgumentNullException(nameof(otherBuilder));
      if(value < 0)
        throw new ArgumentOutOfRangeException(nameof(value), value, "Value must not be negative");

      this.value = value;
      this.otherBuilder = otherBuilder;
    }
  }
}
