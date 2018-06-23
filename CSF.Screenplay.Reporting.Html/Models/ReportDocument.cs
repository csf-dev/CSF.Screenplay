using System;
using CSF.Zpt;
using CSF.Zpt.Metal;

namespace CSF.Screenplay.Reporting.Models
{
  /// <summary>
  /// An object, which is essentially an MVC model, for a report.  This will be passed to the ZPT renderer
  /// as the model for the rendering operation.
  /// </summary>
  public class ReportDocument
  {
    readonly IObjectFormattingService formattingService;
    readonly IZptDocument document;
    readonly ReportModel report;

    /// <summary>
    /// Gets the report to be rendered.
    /// </summary>
    /// <value>The report.</value>
    public ReportModel Report => report;

    /// <summary>
    /// Gets a reference to the ZPT template being used to render the report.
    /// </summary>
    /// <value>The template.</value>
    public IZptDocument Template => document;

    /// <summary>
    /// Provides access to the styles used by the report.
    /// </summary>
    /// <value>The bundled styles.</value>
    public string BundledStyles => Views.ViewProvider.GetBundledStyles();

    /// <summary>
    /// Provides access to the scripts used by the report.
    /// </summary>
    /// <value>The bundled scripts.</value>
    public string BundledScripts => Views.ViewProvider.GetBundledScripts();

    /// <summary>
    /// Gets a collection of the ZPT METAL macros present in the template.
    /// </summary>
    /// <returns>The macros.</returns>
    public IMetalMacroContainer GetMacros()
    {
      var output = Template.GetMacros();
      return output;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Models.ReportDocument"/> class.
    /// </summary>
    /// <param name="report">The Screenplay report to render.</param>
    /// <param name="formattingService">A formatting service.</param>
    /// <param name="document">The ZPT document.</param>
    public ReportDocument(IReport report,
                          IObjectFormattingService formattingService,
                          IZptDocument document)
    {
      if(document == null)
        throw new ArgumentNullException(nameof(document));
      if(formattingService == null)
        throw new ArgumentNullException(nameof(formattingService));
      if(report == null)
        throw new ArgumentNullException(nameof(report));

      this.report = new ReportModel(report, formattingService);
      this.formattingService = formattingService;
      this.document = document;
    }
  }
}
