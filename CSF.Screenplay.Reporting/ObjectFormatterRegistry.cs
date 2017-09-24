using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Implementation of <see cref="IObjectFormatterRegistry"/> which uses an in-memory collection.
  /// </summary>
  public class ObjectFormatterRegistry : IObjectFormatterRegistry
  {
    readonly ICollection<IObjectFormatter> formatters;

    /// <summary>
    /// Adds a formatter to the collection.
    /// </summary>
    /// <param name="formatter">Formatter.</param>
    public void AddFormatter(IObjectFormatter formatter)
    {
      if(formatter == null)
        throw new ArgumentNullException(nameof(formatter));

      formatters.Add(formatter);
    }

    /// <summary>
    /// Gets a collection of all of the registered formatters.
    /// </summary>
    /// <returns>The all formatters.</returns>
    public IReadOnlyCollection<IObjectFormatter> GetAllFormatters() => formatters.ToArray();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.ObjectFormatterRegistry"/> class.
    /// </summary>
    public ObjectFormatterRegistry()
    {
      formatters = new HashSet<IObjectFormatter>();
      formatters.Add(new DefaultObjectFormatter());
    }
  }
}
