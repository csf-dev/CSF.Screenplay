using System;
using System.Collections.Generic;

namespace CSF.Screenplay.Reporting.Models
{
  public class ReportDocument
  {
    public Report Report { get; private set; }

    public IReadOnlyList<Feature> Features { get; private set; }

    public string Format(object result)
    {
      // TODO: Write this implementation
      throw new NotImplementedException();
    }

    public string GetOutcomeClass(bool isSuccess, bool isFailure)
    {
      if(isSuccess) return "success";
      if(isFailure) return "failure";
      return "inconclusive";
    }

    public string GetOutcomeClass(Reportable reportable)
    {
      switch(reportable.Outcome)
      {
      case PerformanceOutcome.Success:
      case PerformanceOutcome.SuccessWithResult:
        return "success";

      default:
        return "failure";
      }
    }

    public string GetReportableClass(Reportable reportable)
    {
      if(reportable == null) return String.Empty;

      if(reportable is GainAbility)
        return "ability";

      if(reportable is Performance)
        return "performance";

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

    string GetMacroName(GainAbility reportable) => "ability";

    string GetMacroName(Performance reportable)
    {
      switch(reportable.Outcome)
      {
      case PerformanceOutcome.SuccessWithResult:
        return "performance_success_result";

      case PerformanceOutcome.FailureWithException:
        return "performance_failure_exception";

      default:
        return "performance";
      }
    }
  }
}
