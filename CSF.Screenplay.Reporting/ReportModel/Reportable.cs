using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.ReportModel
{
  /// <summary>
  /// Base type of the report model.
  /// </summary>
  public class Reportable : IReportable
  {
    IList<Reportable> reportables;

    /// <summary>
    /// Gets or sets the name of the actor.
    /// </summary>
    /// <value>The name of the actor.</value>
    public string ActorName { get; set; }

    /// <summary>
    /// Gets or sets the report.
    /// </summary>
    /// <value>The report.</value>
    public string Report { get; set; }

    /// <summary>
    /// Gets the category (given/when/then) of the reportable represented by the current instance.
    /// </summary>
    /// <value>The reportable category.</value>
    public ReportableCategory Category { get; set; }

    /// <summary>
    /// Gets type of reportable represented by the current instance.
    /// </summary>
    /// <value>The reportable type.</value>
    public ReportableType Type { get; set; }

    /// <summary>
    /// Gets the contained reportables.
    /// </summary>
    /// <value>The reportables.</value>
    public IList<Reportable> Reportables
    {
      get { return reportables; }
      set { reportables = value ?? new List<Reportable>(); }
    }

    IReadOnlyList<IReportable> IProvidesReportables.Reportables
      => Reportables.Cast<IReportable>().ToArray();

    /// <summary>
    /// Gets or sets the result.
    /// </summary>
    /// <value>The result.</value>
    public string Result { get; set; }

    /// <summary>
    /// Gets or sets the error.
    /// </summary>
    /// <value>The error.</value>
    public string Error { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Models.Reportable"/> class.
    /// </summary>
    public Reportable()
    {
      reportables = new List<Reportable>();
    }
  }
}
