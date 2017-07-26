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
    readonly string name;
    readonly IList<Reportable> children;

    /// <summary>
    /// Gets the name of the scenario.
    /// </summary>
    /// <value>The name.</value>
    public virtual string Name => name;

    /// <summary>
    /// Gets the contained reportables.
    /// </summary>
    /// <value>The reportables.</value>
    public virtual IList<Reportable> Reportables => children;

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:CSF.Screenplay.Reporting.Models.Scenario"/>
    /// is a failure.
    /// </summary>
    /// <value><c>true</c> if this scenario is a failure; otherwise, <c>false</c>.</value>
    public virtual bool IsFailure { get; set; }

    /// <summary>
    /// Gets a value indicating whether this <see cref="T:CSF.Screenplay.Reporting.Models.Scenario"/> is a success.
    /// </summary>
    /// <value><c>true</c> if is success; otherwise, <c>false</c>.</value>
    public virtual bool IsSuccess => !IsFailure;

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
    /// <param name="name">Name.</param>
    public Scenario(string name)
    {
      if(name == null)
        throw new ArgumentNullException(nameof(name));

      this.name = name;

      children = new List<Reportable>();
    }
  }
}
