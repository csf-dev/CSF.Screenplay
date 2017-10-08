using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Reporting.Models
{
  /// <summary>
  /// Base type of the report model.
  /// </summary>
  public class Reportable
  {
    readonly INamed actor;
    readonly PerformanceOutcome outcome;
    readonly PerformanceType performanceType;

    /// <summary>
    /// Gets the actor.
    /// </summary>
    /// <value>The actor.</value>
    public virtual INamed Actor => actor;

    /// <summary>
    /// Gets the type of the performance.
    /// </summary>
    /// <value>The type of the performance.</value>
    public virtual PerformanceType PerformanceType => performanceType;

    /// <summary>
    /// Gets the outcome of the reported-upon action.
    /// </summary>
    /// <value>The outcome.</value>
    public virtual PerformanceOutcome Outcome => outcome;

    /// <summary>
    /// Initializes a new instance of the <see cref="Reportable"/> class.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="outcome">Outcome.</param>
    /// <param name="performanceType">Performance type.</param>
    public Reportable(INamed actor,
                      PerformanceOutcome outcome,
                      PerformanceType performanceType = PerformanceType.Unspecified)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));
      outcome.RequireDefinedValue(nameof(outcome));
      performanceType.RequireDefinedValue(nameof(performanceType));

      this.actor = actor;
      this.outcome = outcome;
      this.performanceType = performanceType;
    }
  }
}
