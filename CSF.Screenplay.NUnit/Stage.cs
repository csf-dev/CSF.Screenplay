using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay.NUnit
{
  /// <summary>
  /// A service locator type for Screenplay-related objects.
  /// </summary>
  public static class Stage
  {
    static IReporter reporter;
    static ICast cast;

    /// <summary>
    /// Gets or sets the current reporter.
    /// </summary>
    /// <value>The current reporter.</value>
    public static IReporter Reporter
    {
      get { return reporter; }
      set {
        if(value == null)
          throw new ArgumentNullException(nameof(value));

        reporter = value;
      }
    }

    /// <summary>
    /// Gets or sets the current cast implementation.
    /// </summary>
    /// <value>The cast.</value>
    public static ICast Cast
    {
      get { return cast; }
      set {
        if(value == null)
          throw new ArgumentNullException(nameof(value));

        cast = value;
      }
    }

    /// <summary>
    /// Initializes the <see cref="Stage"/> class.
    /// </summary>
    static Stage()
    {
      reporter = new NoOpReporter();
      cast = new Cast();
    }
  }
}
