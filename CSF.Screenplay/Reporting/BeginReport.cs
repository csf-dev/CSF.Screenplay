using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Represents a report that an actor is beginning to perform an action, task or ask a question.
  /// </summary>
  public class BeginReport : PerformableReportBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="BeginReport"/> class.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable.</param>
    public BeginReport(INamed actor, IPerformable performable) : base(actor, performable) {}
  }
}
