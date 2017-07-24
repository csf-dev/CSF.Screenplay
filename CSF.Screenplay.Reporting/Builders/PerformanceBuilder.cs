using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Reporting.Models;

namespace CSF.Screenplay.Reporting.Builders
{
  /// <summary>
  /// Builder type which creates a <see cref="Performance"/> instance.
  /// </summary>
  public class PerformanceBuilder
  {
    readonly IList<Reportable> reportables;

    /// <summary>
    /// Gets or sets the <see cref="PerformanceType"/>.
    /// </summary>
    /// <value>The type of the performance.</value>
    public PerformanceType PerformanceType { get; set; }

    /// <summary>
    /// Gets or sets the actor.
    /// </summary>
    /// <value>The actor.</value>
    public INamed Actor { get; set; }

    /// <summary>
    /// Gets or sets the performable.
    /// </summary>
    /// <value>The performable.</value>
    public IPerformable Performable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this
    /// <see cref="PerformanceBuilder"/> has a result.
    /// </summary>
    /// <value><c>true</c> if has result; otherwise, <c>false</c>.</value>
    public bool HasResult { get; set; }

    /// <summary>
    /// Gets or sets the result.
    /// </summary>
    /// <value>The result.</value>
    public object Result { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this
    /// <see cref="PerformanceBuilder"/> is a failure.
    /// </summary>
    /// <value><c>true</c> if is failure; otherwise, <c>false</c>.</value>
    public bool IsFailure { get; set; }

    /// <summary>
    /// Gets or sets the exception.
    /// </summary>
    /// <value>The exception.</value>
    public Exception Exception { get; set; }

    /// <summary>
    /// Gets a collection of child reportables.
    /// </summary>
    /// <value>The reportables.</value>
    public IList<Reportable> Reportables => reportables;

    /// <summary>
    /// Builds and returns a <see cref="Performance"/> from the state of the current instance.
    /// </summary>
    /// <returns>The performance.</returns>
    public Performance GetPerformance()
    {
      return new Performance(Actor,
                             GetOutcome(),
                             Performable,
                             PerformanceType,
                             Result,
                             Exception,
                             Reportables.ToArray());
    }

    Outcome GetOutcome()
    {
      if(IsFailure && Exception != null)
        return Outcome.FailureWithException;

      if(IsFailure)
        return Outcome.Failure;

      if(HasResult)
        return Outcome.SuccessWithResult;

      return Outcome.Success;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PerformanceBuilder"/> class.
    /// </summary>
    public PerformanceBuilder()
    {
      reportables = new List<Reportable>();
    }
  }
}
