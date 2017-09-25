using System;
using System.Linq;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Provides a wrapper around <see cref="IObjectFormatterRegistry"/>, selecting the best registered formatter
  /// and outputting the formatted name.
  /// </summary>
  public class ObjectFormattingService : IObjectFormattingService
  {
    static IObjectFormattingService defaultInstance;
    readonly IObjectFormatterRegistry registry;

    /// <summary>
    /// Format the specified object.
    /// </summary>
    /// <param name="obj">Object.</param>
    public string Format(object obj)
    {
      var formatter = SelectBestFormatter(obj);
      if(formatter == null)
        return null;

      return formatter.GetFormattedName(obj);
    }

    IObjectFormatter SelectBestFormatter(object obj)
      => registry.GetAllFormatters()
                 .OrderByDescending(x => x.GetFormattingPriority(obj))
                 .FirstOrDefault();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.ObjectFormattingService"/> class.
    /// </summary>
    /// <param name="registry">Registry.</param>
    public ObjectFormattingService(IObjectFormatterRegistry registry)
    {
      if(registry == null)
        throw new ArgumentNullException(nameof(registry));
      this.registry = registry;
    }

    /// <summary>
    /// Initializes the <see cref="T:CSF.Screenplay.Reporting.ObjectFormattingService"/> class.
    /// </summary>
    static ObjectFormattingService()
    {
      var defaultRegistry = new ObjectFormatterRegistry();
      defaultInstance = new ObjectFormattingService(defaultRegistry);
    }

    /// <summary>
    /// Gets a default/singleton instance which has no registered formatters except the default.
    /// </summary>
    /// <value>The default formatting service.</value>
    public static IObjectFormattingService Default => defaultInstance;
  }
}
