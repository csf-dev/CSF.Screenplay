using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Reporting.Models
{
  /// <summary>
  /// The root of the report model, representing the report for an entire test run.
  /// </summary>
  public class Report
  {
    readonly IReadOnlyList<Feature> features;
    readonly DateTime timestamp;

    /// <summary>
    /// Gets a collection of the scenarios in this report.
    /// </summary>
    /// <value>The scenarios.</value>
    public virtual IReadOnlyList<Feature> Features => features;

    /// <summary>
    /// Gets the count of features (total) in this report.
    /// </summary>
    /// <value>The feature count.</value>
    public virtual int TotalFeatureCount => Features.Count;

    /// <summary>
    /// Gets the count of successful features in this report.
    /// </summary>
    /// <value>The successful feature count.</value>
    public virtual int SuccessfulFeatureCount => Features.Count(x => x.IsSuccess);

    /// <summary>
    /// Gets the count of failing features in this report.
    /// </summary>
    /// <value>The failing feature count.</value>
    public virtual int FailingFeatureCount => Features.Count(x => x.HasFailures);

    /// <summary>
    /// Gets the scenarios in the current report instance.
    /// </summary>
    /// <value>The scenarios.</value>
    public virtual IReadOnlyList<Scenario> Scenarios => Features.SelectMany(x => x.Scenarios).ToArray();

    /// <summary>
    /// Gets the count of scenarios (total) in this report.
    /// </summary>
    /// <value>The scenario count.</value>
    public virtual int TotalScenarioCount => Scenarios.Count;

    /// <summary>
    /// Gets the count of successful scenarios in this report.
    /// </summary>
    /// <value>The successful scenario count.</value>
    public virtual int SuccessfulScenarioCount => Scenarios.Count(x => x.IsSuccess);

    /// <summary>
    /// Gets the count of failing scenarios in this report.
    /// </summary>
    /// <value>The failing scenario count.</value>
    public virtual int FailingScenarioCount => Scenarios.Count(x => x.IsFailure);

    /// <summary>
    /// Gets the timestamp for the creation of this report.
    /// </summary>
    /// <value>The timestamp.</value>
    public virtual DateTime Timestamp => timestamp;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Models.Report"/> class.
    /// </summary>
    /// <param name="features">Features.</param>
    public Report(IReadOnlyList<Feature> features)
    {
      if(features == null)
        throw new ArgumentNullException(nameof(features));
      
      this.features = features;
      timestamp = DateTime.UtcNow;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Models.Report"/> class.
    /// </summary>
    /// <param name="scenarios">Scenarios.</param>
    public Report(IReadOnlyList<Scenario> scenarios)
    {
      if(scenarios == null)
        throw new ArgumentNullException(nameof(scenarios));

      var feature = new Feature("Unspecified", "Unspecified", scenarios);
      this.features = new [] { feature };
      timestamp = DateTime.UtcNow;
    }
  }
}
