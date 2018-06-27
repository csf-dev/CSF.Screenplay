using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.ReportFormatting;
using CSF.Screenplay.ReportModel;

namespace CSF.Screenplay.Reporting.Builders
{
  /// <summary>
  /// Builder type which creates a <see cref="Reportable"/> instance.
  /// </summary>
  public class ReportableBuilder
  {
    readonly IList<Reportable> reportables;
    readonly IFormatsObjectForReport objectFormatter;

    /// <summary>
    /// Gets or sets the <see cref="PerformanceType"/>.
    /// </summary>
    /// <value>The type of the performance.</value>
    public ReportableCategory PerformanceType { get; set; }

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
    /// <see cref="ReportableBuilder"/> has a result.
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
    /// <see cref="ReportableBuilder"/> is a failure.
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
    /// Builds and returns a <see cref="Reportable"/> from the state of the current instance.
    /// </summary>
    /// <returns>The performance.</returns>
    public Reportable GetReportable()
    {
      return new Reportable {
        ActorName = Actor?.Name ?? "<actor name missing>",
        Category = PerformanceType,
        Error = (Exception != null)? objectFormatter.FormatForReport(Exception) : null,
        Result = (Result != null)? objectFormatter.FormatForReport(Result) : null,
        Report = Performable?.GetReport(Actor) ?? "<report data missing>",
        Reportables = Reportables,
        Type = GetOutcome(),
      };
    }

    ReportableType GetOutcome()
    {
      if(IsFailure && Exception != null)
        return ReportableType.FailureWithError;

      if(IsFailure)
        return ReportableType.Failure;

      if(HasResult)
        return ReportableType.SuccessWithResult;

      return ReportableType.Success;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportableBuilder"/> class.
    /// </summary>
    public ReportableBuilder(IFormatsObjectForReport objectFormatter)
    {
      if(objectFormatter == null)
        throw new ArgumentNullException(nameof(objectFormatter));
      
      reportables = new List<Reportable>();
      this.objectFormatter = objectFormatter;
    }
  }
}
