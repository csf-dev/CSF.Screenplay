using System;
using CSF.Screenplay.Web.Waits;

namespace CSF.Screenplay.Web.Models
{
  /// <summary>
  /// Builder type for a general-purpose wait.
  /// </summary>
  public class GeneralWaitBuilder
  {
    readonly int value;

    /// <summary>
    /// Builds the wait from milliseconds.
    /// </summary>
    public GeneralWait Milliseconds()
    {
      var timespan = TimeSpan.FromMilliseconds(value);
      return new GeneralWait(timespan);
    }

    /// <summary>
    /// Builds the wait from seconds.
    /// </summary>
    public GeneralWait Seconds()
    {
      var timespan = TimeSpan.FromSeconds(value);
      return new GeneralWait(timespan);
    }

    /// <summary>
    /// Builds the wait from minutes.
    /// </summary>
    public GeneralWait Minutes()
    {
      var timespan = TimeSpan.FromMinutes(value);
      return new GeneralWait(timespan);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Web.Models.GeneralWaitBuilder"/> class.
    /// </summary>
    /// <param name="value">Value.</param>
    public GeneralWaitBuilder(int value)
    {
      if(value < 0)
        throw new ArgumentOutOfRangeException(nameof(value), value, "Value must not be negative");

      this.value = value;
    }
  }
}
