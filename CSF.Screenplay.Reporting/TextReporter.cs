using System;
using System.IO;
using System.Linq;
using CSF.Screenplay.Reporting.Models;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Implementation of <see cref="IReporter" /> which writes the report to a text-based format, via a
  /// <c>System.IO.TextWriter</c>.
  /// </summary>
  public class TextReporter : ReportBuildingReporter
  {
    const char INDENT_CHAR = ' ';
    const int INDENT_WIDTH = 4, RESULT_OR_FAILURE_INDENT_WIDTH = 6;

    readonly TextWriter writer;

    /// <summary>
    /// Indicates to the reporter that the test run has completed and that the report should be finalised.
    /// </summary>
    public override void CompleteTestRun()
    {
      var model = GetReportModel();
      foreach(var scenario in model.Scenarios)
      {
        WriteScenario(scenario);
      }
    }

    void WriteScenario(Scenario scenario)
    {
      if(scenario == null)
        throw new ArgumentNullException(nameof(scenario));
      
      writer.WriteLine();
      writer.WriteLine("{0,7} Scenario: {1}", GetOutcome(scenario), scenario.Name);

      foreach(var reportable in scenario.Reportables)
      {
        WriteReportable(reportable);
      }
    }

    void WriteReportable(Reportable reportable, int currentIndentLevel = 1)
    {
      if(reportable == null)
        throw new ArgumentNullException(nameof(reportable));

      if(reportable is GainAbility)
        WriteGainAbility((GainAbility) reportable, currentIndentLevel);
      else if(reportable is Performance)
        WritePerformance((Performance) reportable, currentIndentLevel);
      else
        throw new ArgumentException($"The reportable must be either a {typeof(GainAbility).Name} or a {typeof(Performance).Name}.");
    }

    void WriteGainAbility(GainAbility reportable, int currentIndentLevel)
    {
      WriteIndent(currentIndentLevel);
      WritePerformanceType(reportable);

      writer.Write(reportable.Ability.GetReport(reportable.Actor));
      writer.WriteLine();
    }

    void WritePerformance(Performance reportable, int currentIndentLevel)
    {
      WriteIndent(currentIndentLevel);
      WritePerformanceType(reportable);

      writer.Write(reportable.Performable.GetReport(reportable.Actor));
      writer.WriteLine();

      if(reportable.Outcome == Outcome.SuccessWithResult)
        WriteResult(reportable, currentIndentLevel);
      else if(reportable.Outcome == Outcome.Failure || reportable.Outcome == Outcome.FailureWithException)
        WriteFailure(reportable, currentIndentLevel);

      foreach(var child in reportable.Reportables)
      {
        WriteReportable(child, currentIndentLevel++);
      }
    }

    string GetPerformanceTypeString(PerformanceType type)
    {
      if(type == PerformanceType.Unspecified)
        return String.Empty;

      return type.ToString();
    }

    string GetOutcome(Scenario scenario) => scenario.IsSuccess? "SUCCESS" : "FAILURE";

    void WriteIndent(int currentLevel)
    {
      writer.Write(GetIndent(currentLevel));
    }

    void WriteResultOrFailureIndent(int currentLevel)
    {
      WriteIndent(currentLevel);
      writer.Write(new String(INDENT_CHAR, RESULT_OR_FAILURE_INDENT_WIDTH));
    }

    void WritePerformanceType(Reportable reportable)
    {
      writer.Write("{0,5} ", GetPerformanceTypeString(reportable.PerformanceType));
    }

    void WriteResult(Performance reportable, int currentIndentLevel)
    {
      WriteResultOrFailureIndent(currentIndentLevel);
      var result = reportable.Result?.ToString();
      writer.WriteLine("Result:{0}", result ?? "<null>");
    }

    void WriteFailure(Performance reportable, int currentIndentLevel)
    {
      WriteResultOrFailureIndent(currentIndentLevel);
      var exception = reportable.Exception?.ToString();
      if(exception != null)
        writer.WriteLine("FAILED\n{0}", exception);
      else
        writer.WriteLine("FAILED");
    }

    string GetIndent(int currentLevel) 
      => String.Join(String.Empty, Enumerable.Range(0, currentLevel).Select(x => new String(INDENT_CHAR, INDENT_WIDTH)));

    /// <summary>
    /// Initializes a new instance of the <see cref="TextReporter"/> class.
    /// </summary>
    /// <param name="writer">Writer.</param>
    public TextReporter(TextWriter writer)
    {
      if(writer == null)
        throw new ArgumentNullException(nameof(writer));

      this.writer = writer;
    }
  }
}
