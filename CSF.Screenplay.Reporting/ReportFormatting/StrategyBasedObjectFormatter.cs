using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.ReportFormatting
{
  /// <summary>
  /// An implementation of <see cref="IFormatsObjectForReport"/> which uses the strategy pattern to select an
  /// appropriate formatter object from a registry of available strategies.
  /// </summary>
  public class StrategyBasedObjectFormatter : IFormatsObjectForReport
  {
    readonly IObjectFormatingStrategyRegistry registry;

    /// <summary>
    /// Gets a formatted string representing the given object.
    /// </summary>
    /// <param name="obj">The object to format.</param>
    public string FormatForReport(object obj)
    {
      var strategy = GetFormattingStrategy(obj);
      if(strategy == null) return null;
      return strategy.FormatForReport(obj);
    }

    IFormatsObjectForReport GetFormattingStrategy(object obj)
    {
      return (from strategy in registry.GetAll()
              where strategy.CanFormat(obj)
              orderby strategy.GetFormattingPriority(obj) descending
              select strategy)
        .FirstOrDefault();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.ReportFormatting.StrategyBasedObjectFormatter"/> class.
    /// </summary>
    /// <param name="registry">Registry.</param>
    public StrategyBasedObjectFormatter(IObjectFormatingStrategyRegistry registry)
    {
      if(registry == null)
        throw new ArgumentNullException(nameof(registry));
      this.registry = registry;
    }
  }
}
