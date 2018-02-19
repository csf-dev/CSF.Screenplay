using System;
using System.Collections.Generic;
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
    const string
      SuccessClass = "success",
      FailureClass = "failure",
      InconclusiveClass = "inconclusive",
      AbilityClass = "ability",
      PerformanceClass = "performance",
      AbilityMacro = "ability",
      PerformanceWithResultMacro = "performance_success_result",
      PerformanceWithExceptionMacro = "performance_failure_exception",
      PerformanceMacro = "performance";

    readonly IObjectFormattingService formattingService;
    readonly IZptDocument document;
    readonly Report report;

    /// <summary>
    /// Gets the report to be rendered.
    /// </summary>
    /// <value>The report.</value>
    public Report Report => report;

    /// <summary>
    /// Gets a reference to the ZPT template being used to render the report.
    /// </summary>
    /// <value>The template.</value>
    public IZptDocument Template => document;

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
    /// Formats a given object using a formatting service.
    /// </summary>
    /// <param name="obj">Object.</param>
    public string Format(object obj) => formattingService.Format(obj);

    /// <summary>
    /// Gets a value which indicates whether the formatting service has an explicit formatter for the given object or not.
    /// </summary>
    /// <returns><c>true</c>, if the formatting service has an explicit format for the object, <c>false</c> otherwise.</returns>
    /// <param name="obj">Object.</param>
    public bool HasFormat(object obj) => formattingService.HasExplicitSupport(obj);

    /// <summary>
    /// Gets the HTML class attribute value indicating the outcome of a given scenario.
    /// </summary>
    /// <returns>The scenario outcome class.</returns>
    /// <param name="scenario">A test sscenario.</param>
    public string GetOutcomeClass(Scenario scenario)
    {
      if(scenario == null) return String.Empty;
      var outcome = GetOutcomeClass(scenario.IsSuccess, scenario.IsFailure);
      return $"scenario {outcome}";
    }

    /// <summary>
    /// Gets the HTML class attribute value indicating the outcome of a given feature.
    /// </summary>
    /// <returns>The feature outcome class.</returns>
    /// <param name="feature">A test feature.</param>
    public string GetOutcomeClass(Feature feature)
    {
      if(feature == null) return String.Empty;
      var outcome = GetOutcomeClass(feature.IsSuccess, feature.HasFailures);
      return $"feature {outcome}";
    }

    /// <summary>
    /// Gets the HTML class attribute value indicating the outcome of a given reportable step within a scenario.
    /// </summary>
    /// <returns>The reportable's outcome class.</returns>
    /// <param name="reportable">A reportable step, part of a scenario.</param>
    public string GetOutcomeClass(Reportable reportable)
    {
      
      switch(reportable.Outcome)
      {
      case PerformanceOutcome.Success:
      case PerformanceOutcome.SuccessWithResult:
        return GetOutcomeClass(true, false);

      default:
        return GetOutcomeClass(false, true);
      }
    }

    string GetOutcomeClass(bool success, bool failure)
    {
      if(success) return SuccessClass;
      if(failure) return FailureClass;
      return InconclusiveClass;
    }

    /// <summary>
    /// Gets an HTML class attribute value which describes a reportable step within a scenario.
    /// </summary>
    /// <returns>The reportable class.</returns>
    /// <param name="reportable">A reportable step, part of a scenario.</param>
    public string GetReportableClass(Reportable reportable)
    {
      if(reportable == null) return String.Empty;

      if(reportable is GainAbility)
        return AbilityClass;

      if(reportable is Performance)
        return PerformanceClass;

      return String.Empty;
    }

    /// <summary>
    /// Gets the name of the METAL macro to use for rendering the specified reportable.
    /// </summary>
    /// <returns>The macro name.</returns>
    /// <param name="reportable">A reportable step, part of a scenario.</param>
    public string GetMacroName(Reportable reportable)
    {
      if(reportable == null) return null;

      if(reportable is GainAbility)
        return GetMacroName((GainAbility) reportable);

      if(reportable is Performance)
        return GetMacroName((Performance) reportable);

      return null;
    }

    string GetMacroName(GainAbility reportable) => AbilityMacro;

    string GetMacroName(Performance reportable)
    {
      switch(reportable.Outcome)
      {
      case PerformanceOutcome.SuccessWithResult:
        return PerformanceWithResultMacro;

      case PerformanceOutcome.FailureWithException:
        return PerformanceWithExceptionMacro;

      default:
        return PerformanceMacro;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Models.ReportDocument"/> class.
    /// </summary>
    /// <param name="report">The Screenplay report to render.</param>
    /// <param name="formattingService">A formatting service.</param>
    /// <param name="document">The ZPT document.</param>
    public ReportDocument(Report report,
                          IObjectFormattingService formattingService,
                          IZptDocument document)
    {
      if(document == null)
        throw new ArgumentNullException(nameof(document));
      if(formattingService == null)
        throw new ArgumentNullException(nameof(formattingService));
      if(report == null)
        throw new ArgumentNullException(nameof(report));

      this.report = report;
      this.formattingService = formattingService;
      this.document = document;
    }
  }
}
