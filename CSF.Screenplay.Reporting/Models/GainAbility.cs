using System;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Reporting.Models
{
  /// <summary>
  /// A report model item indicating that an actor has gained an ability.
  /// </summary>
  public class GainAbility : Reportable
  {
    /// <summary>
    /// Gets the report.
    /// </summary>
    /// <value>The report.</value>
    public string Report { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GainAbility"/> class.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="outcome">Outcome.</param>
    /// <param name="ability">Ability.</param>
    /// <param name="performanceType">Performance type.</param>
    public GainAbility(INamed actor,
                       PerformanceOutcome outcome,
                       IAbility ability,
                       PerformanceType performanceType = PerformanceType.Unspecified)
      : base(actor, outcome, performanceType)
    {
      if(ability == null)
        throw new ArgumentNullException(nameof(ability));

      Report = ability.GetReport(actor);
    }
  }
}
