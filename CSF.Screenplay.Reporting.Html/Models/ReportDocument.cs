using System;
using System.Collections.Generic;
using CSF.Zpt;
using CSF.Zpt.Metal;

namespace CSF.Screenplay.Reporting.Models
{
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

    public Report Report => report;

    public IZptDocument Template => document;

    public IMetalMacroContainer GetMacros()
    {
      var output = Template.GetMacros();
      return output;
    }

    public string Format(object obj) => formattingService.Format(obj);

    public string GetOutcomeClass(Scenario scenario)
    {
      if(scenario == null) return String.Empty;
      var outcome = GetOutcomeClass(scenario.IsSuccess, scenario.IsFailure);
      return $"scenario {outcome}";
    }

    public string GetOutcomeClass(Feature feature)
    {
      if(feature == null) return String.Empty;
      var outcome = GetOutcomeClass(feature.IsSuccess, feature.HasFailures);
      return $"feature {outcome}";
    }

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

    public string GetReportableClass(Reportable reportable)
    {
      if(reportable == null) return String.Empty;

      if(reportable is GainAbility)
        return AbilityClass;

      if(reportable is Performance)
        return PerformanceClass;

      return String.Empty;
    }

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

    public ReportDocument(Report report, IObjectFormattingService formattingService, IZptDocument document)
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
