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

    /// <summary>
    /// Gets a value which indicates whether or not the current formatting service has explicit support
    /// for the given object or not.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If this method returns <c>false</c> then chances are that the output from <see cref="M:CSF.Screenplay.Reporting.IObjectFormattingService.Format(System.Object)" /> will be the
    /// built-in <c>Object.ToString</c>.
    /// </para>
    /// </remarks>
    /// <returns>
    /// <c>true</c>, if the formatter has explicit support for the given object, <c>false</c> otherwise.</returns>
    /// <param name="obj">Object.</param>
    public bool HasExplicitSupport(object obj)
    {
      var formatter = SelectBestFormatter(obj);
      return (formatter != null && formatter.GetType() != typeof(DefaultObjectFormatter));
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
