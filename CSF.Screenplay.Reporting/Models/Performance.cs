using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Reporting.Models
{
  /// <summary>
  /// A report model item indicating that an actor has performed some kind of interaction.
  /// </summary>
  public class Performance : Reportable
  {
    readonly IPerformable performable;
    readonly object result;
    readonly Exception exception;
    readonly IList<Reportable> children;

    /// <summary>
    /// Gets the contained reportables.
    /// </summary>
    /// <value>The reportables.</value>
    public virtual IList<Reportable> Reportables => children;

    /// <summary>
    /// Gets the performable associated with the current instance.
    /// </summary>
    /// <value>The performable.</value>
    public virtual IPerformable Performable => performable;

    /// <summary>
    /// Gets the result received from the performable.
    /// </summary>
    /// <value>The result.</value>
    public virtual object Result => result;

    /// <summary>
    /// Gets an exception raised by the performable.
    /// </summary>
    /// <value>The exception.</value>
    public virtual Exception Exception => exception;

    /// <summary>
    /// Initializes a new instance of the <see cref="Performance"/> class.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="outcome">Outcome.</param>
    /// <param name="performable">Performable.</param>
    /// <param name="performanceType">Performance type.</param>
    /// <param name="result">Result.</param>
    /// <param name="exception">Exception.</param>
    /// <param name="children">Children.</param>
    public Performance(INamed actor,
                       Outcome outcome,
                       IPerformable performable,
                       PerformanceType performanceType = PerformanceType.Unspecified,
                       object result = null,
                       Exception exception = null,
                       IList<Reportable> children = null) : base(actor, outcome, performanceType)
    {
      if(performable == null)
        throw new ArgumentNullException(nameof(performable));

      this.performable = performable;
      this.result = result;
      this.exception = exception;

      this.children = children?? new List<Reportable>();
    }
  }
}
