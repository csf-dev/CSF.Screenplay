using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Reporting.Models
{
  /// <summary>
  /// Represents a single scenrio within a report (equivalent to a single test case, if not using Cucumber terminology).
  /// </summary>
  public class Scenario
  {
    readonly string idName, friendlyName, featureName, featureId;
    readonly IList<Reportable> children;

    /// <summary>
    /// Gets the unique identifier name of the scenario.
    /// </summary>
    /// <value>The identifier.</value>
    public virtual string Id => idName;

    /// <summary>
    /// Gets the name of the feature.
    /// </summary>
    /// <value>The feature name.</value>
    public virtual string FeatureName => featureName;

    /// <summary>
    /// Gets the identifier for the feature.
    /// </summary>
    /// <value>The feature identifier.</value>
    public virtual string FeatureId => featureId;

    /// <summary>
    /// Gets the name of the scenario.
    /// </summary>
    /// <value>The scenario name.</value>
    public virtual string FriendlyName => friendlyName;

    /// <summary>
    /// Gets the contained reportables.
    /// </summary>
    /// <value>The reportables.</value>
    public virtual IList<Reportable> Reportables => children;

    /// <summary>
    /// Gets or sets the outcome for the test scenario.
    /// </summary>
    /// <value>The outcome.</value>
    public virtual bool? Outcome { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:CSF.Screenplay.Reporting.Models.Scenario"/>
    /// is inconclusive (neither success nor failure).
    /// </summary>
    /// <value><c>true</c> if this scenario is inconclusive; otherwise, <c>false</c>.</value>
    public virtual bool IsInconclusive => !Outcome.HasValue;

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:CSF.Screenplay.Reporting.Models.Scenario"/>
    /// is a failure.
    /// </summary>
    /// <value><c>true</c> if this scenario is a failure; otherwise, <c>false</c>.</value>
    public virtual bool IsFailure => Outcome.HasValue && !Outcome.Value;

    /// <summary>
    /// Gets a value indicating whether this <see cref="T:CSF.Screenplay.Reporting.Models.Scenario"/> is a success.
    /// </summary>
    /// <value><c>true</c> if is success; otherwise, <c>false</c>.</value>
    public virtual bool IsSuccess => Outcome.HasValue && Outcome.Value;

    IEnumerable<Reportable> FindReportables()
    {
      return children.ToArray()
        .Union(children.SelectMany(x => FindReportables(x)))
        .ToArray();
    }

    IEnumerable<Reportable> FindReportables(Reportable current)
    {
      var performance = current as Performance;
      if(performance == null)
        return Enumerable.Empty<Reportable>();

      return performance.Reportables.ToArray()
        .Union(performance.Reportables.SelectMany(x => FindReportables(x)))
        .ToArray();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Scenario"/> class.
    /// </summary>
    /// <param name="friendlyName">The friendly scenario name.</param>
    /// <param name="featureName">The feature name.</param>
    /// <param name="idName">The uniquely identifying name for the test.</param>
    /// <param name="featureId">The uniquely identifying name for the feature.</param>
    public Scenario(string idName, string friendlyName = null, string featureName = null, string featureId = null)
    {
      if(idName == null)
        throw new ArgumentNullException(nameof(idName));

      this.idName = idName;
      this.friendlyName = friendlyName;
      this.featureName = featureName;
      this.featureId = featureId;

      children = new List<Reportable>();
    }
  }
}
