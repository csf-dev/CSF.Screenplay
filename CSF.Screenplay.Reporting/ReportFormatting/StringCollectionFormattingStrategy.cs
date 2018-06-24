using System;
using System.Collections.Generic;

namespace CSF.Screenplay.ReportFormatting
{
  /// <summary>
  /// A formatting strategy for collections of <c>System.String</c> which outputs the strings as a
  /// comma-separated list.
  /// </summary>
  public class StringCollectionFormattingStrategy : ObjectFormattingStrategy<IEnumerable<string>>
  {
    /// <summary>
    /// Gets a formatted string representing the given object.
    /// </summary>
    /// <param name="obj">Object.</param>
    public override string FormatForReport(IEnumerable<string> obj) => String.Join(", ", obj);
  }
}
