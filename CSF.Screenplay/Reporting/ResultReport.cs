using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Represents a report that an actor has retrieved a result from an action, task or question.
  /// </summary>
  public class ResultReport : PerformableReportBase
  {
    /// <summary>
    /// Gets the result value.
    /// </summary>
    /// <value>The result.</value>
    public object Result { get; private set; }

    /// <summary>
    /// Returns a <c>System.String</c> that represents the current <see cref="ResultReport"/>.
    /// </summary>
    /// <returns>A <c>System.String</c> that represents the current <see cref="ResultReport"/>.</returns>
    public override string ToString()
    {
      var resultString = (Result != null)? Result.ToString() : "<null>";
      return $"RESULT:  {Performable.GetReport(Actor)} : {resultString}";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResultReport"/> class.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable.</param>
    /// <param name="result">The result value.</param>
    public ResultReport(INamed actor, IPerformable performable, object result)
      : base(actor, performable)
    {
      Result = result;
    }
  }
}
