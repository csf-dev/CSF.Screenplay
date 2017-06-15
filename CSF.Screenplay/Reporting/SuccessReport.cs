using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Represents a report that an actor has successfully performed an action, task or asked a question.
  /// </summary>
  public class SuccessReport : PerformableReportBase, IPerformanceEnd
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SuccessReport"/> class.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable.</param>
    public SuccessReport(INamed actor, IPerformable performable) : base(actor, performable) {}
  }
}
