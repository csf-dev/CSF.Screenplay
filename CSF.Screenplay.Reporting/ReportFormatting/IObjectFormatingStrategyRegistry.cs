using System;
using System.Collections.Generic;

namespace CSF.Screenplay.ReportFormatting
{
  /// <summary>
  /// A registry of available object formatting strategies.
  /// </summary>
  public interface IObjectFormatingStrategyRegistry
  {
    /// <summary>
    /// Adds a formatting strategy to the collection.
    /// </summary>
    /// <param name="formatter">A formatting strategy.</param>
    void Add(IHasObjectFormattingStrategyInfo formatter);

    /// <summary>
    /// Removes a formatting strategy from the collection.
    /// </summary>
    /// <param name="formatter">A formatting strategy.</param>
    void Remove(IHasObjectFormattingStrategyInfo formatter);

    /// <summary>
    /// Gets a collection of all of the registered formatting strategies.
    /// </summary>
    /// <returns>The formatting strategies.</returns>
    IReadOnlyCollection<IHasObjectFormattingStrategyInfo> GetAll();
  }
}
