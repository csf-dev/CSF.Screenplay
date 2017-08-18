using System;
namespace CSF.Screenplay.Web.Builders
{
  public class TimespanBuilder<TBuilder> : IProvidesTimespan
    where TBuilder : class
  {
    readonly TBuilder otherBuilder;
    int value;
    TimeSpan timespan;

    TimeSpan IProvidesTimespan.GetTimespan() => timespan;

    public TBuilder Milliseconds()
    {
      timespan = TimeSpan.FromMilliseconds(value);
      return otherBuilder;
    }

    public TBuilder Seconds()
    {
      timespan = TimeSpan.FromSeconds(value);
      return otherBuilder;
    }

    public TBuilder Minutes()
    {
      timespan = TimeSpan.FromMinutes(value);
      return otherBuilder;
    }

    internal TimespanBuilder(int value, TBuilder otherBuilder)
    {
      if(otherBuilder == null)
        throw new ArgumentNullException(nameof(otherBuilder));
      if(value < 0)
        throw new ArgumentOutOfRangeException(nameof(value), value, "Value must not be negative");

      this.value = value;
    }
  }
}
