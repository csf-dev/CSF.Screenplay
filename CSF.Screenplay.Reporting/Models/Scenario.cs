using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Reporting.Models
{
  /// <summary>
  /// Represents a single scenrio within a report (equivalent to a single test case, if not using Gherkin terminology).
  /// </summary>
  public class Scenario : IScenario
  {
    IList<Reportable> reportables;
    IdAndName name;
    Feature feature;

    /// <summary>
    /// Gets or sets naming information for this scenario.
    /// </summary>
    /// <value>The scenario name.</value>
    public IdAndName Name
    {
      get { return name; }
      set { name = value ?? new IdAndName(); }
    }

    /// <summary>
    /// Gets or sets the associated feature.
    /// </summary>
    /// <value>The feature.</value>
    public Feature Feature
    {
      get { return feature; }
      set { feature = value ?? new Feature(); }
    }

    IFeature IScenario.Feature => Feature;

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
    /// Gets or sets the outcome for the test scenario.
    /// </summary>
    /// <value>The outcome.</value>
    public bool? Outcome { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Models.Scenario"/> class.
    /// </summary>
    public Scenario()
    {
      reportables = new List<Reportable>();
      name = new IdAndName();
      feature = new Feature();
    }
  }
}
