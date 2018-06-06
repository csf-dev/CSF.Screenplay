using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Reporting.Models
{
  /// <summary>
  /// Represents a single feature within a report (a logical grouping of scenarios - in NUnit terminology a TestFixture).
  /// </summary>
  public class Feature
  {
    readonly bool isIdGenerated;
    readonly string id, friendlyName;
    readonly IReadOnlyCollection<Scenario> scenarios;

    /// <summary>
    /// Gets the identity of this feature.
    /// </summary>
    public string Id => id;

    /// <summary>
    /// Gets a value which indicates whether the <see cref="Id"/> is auto-generated and thus meaningless.
    /// </summary>
    /// <value><c>true</c> if the feature identifier is generated; otherwise, <c>false</c>.</value>
    public bool IsIdGenerated => isIdGenerated;

    /// <summary>
    /// Gets the human-readable friendly-name of this feature.
    /// </summary>
    public string FriendlyName => friendlyName;

    /// <summary>
    /// Gets a collection of the scenarios in the current feature.
    /// </summary>
    public IReadOnlyCollection<Scenario> Scenarios => scenarios;

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="Feature"/> contains any scenarios which are themselves failures.
    /// </summary>
    /// <value><c>true</c> if this feature contains any failures; otherwise, <c>false</c>.</value>
    public bool HasFailures => Scenarios.Any(x => x.IsFailure);

    /// <summary>
    /// Gets or sets a value indicating whether every <see cref="Scenario"/> within this <see cref="Feature"/> is a success.
    /// </summary>
    /// <value><c>true</c> if the feature contains only successful scenarios; otherwise, <c>false</c>.</value>
    public bool IsSuccess => Scenarios.All(x => x.IsSuccess);

    /// <summary>
    /// Initialises a new instance of the <see cref="Feature"/> class.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="friendlyName"></param>
    /// <param name="scenarios"></param>
    /// <param name="isIdGenerated">Indicates whether the <paramref name="id"/> is auto-generated or not</param>
    public Feature(string id,
                   string friendlyName,
                   IReadOnlyCollection<Scenario> scenarios,
                   bool isIdGenerated = false)
    {
      if (id == null) throw new ArgumentNullException(nameof(id));
      if(scenarios == null) throw new ArgumentNullException(nameof(scenarios));

      this.id = id;
      this.friendlyName = friendlyName;
      this.scenarios = scenarios;
      this.isIdGenerated = isIdGenerated;
    }
  }
}