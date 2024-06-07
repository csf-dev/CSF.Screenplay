using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.ReportFormatting
{
  /// <summary>
  /// Implementation of <see cref="IObjectFormatingStrategyRegistry"/> which uses an in-memory collection.
  /// </summary>
  public class ObjectFormattingStrategyRegistry : IObjectFormatingStrategyRegistry
  {
    readonly ISet<IHasObjectFormattingStrategyInfo> formatters;

    /// <summary>
    /// Adds a formatter to the collection.
    /// </summary>
    /// <param name="formatter">Formatter.</param>
    public void Add(IHasObjectFormattingStrategyInfo formatter)
    {
      if(formatter == null)
        throw new ArgumentNullException(nameof(formatter));

      formatters.Add(formatter);
    }

    /// <summary>
    /// Removes a formatting strategy from the collection.
    /// </summary>
    /// <param name="formatter">A formatting strategy.</param>
    public void Remove(IHasObjectFormattingStrategyInfo formatter)
    {
      if(formatter == null)
        throw new ArgumentNullException(nameof(formatter));

      formatters.Remove(formatter);
    }

    /// <summary>
    /// Gets a collection of all of the registered formatters.
    /// </summary>
    /// <returns>The all formatters.</returns>
    public IReadOnlyCollection<IHasObjectFormattingStrategyInfo> GetAll() => formatters.ToArray();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.ObjectFormatterRegistry"/> class.
    /// </summary>
    public ObjectFormattingStrategyRegistry()
    {
      formatters = new HashSet<IHasObjectFormattingStrategyInfo>();
    }

    /// <summary>
    /// Creates a default registry instance, which already contains the <see cref="DefaultObjectFormattingStrategy"/>.
    /// </summary>
    /// <returns>A default registry.</returns>
    public static IObjectFormatingStrategyRegistry CreateDefault()
    {
      var registry = new ObjectFormattingStrategyRegistry();
      registry.Add(new DefaultObjectFormattingStrategy());
      return registry;
    }
  }
}
