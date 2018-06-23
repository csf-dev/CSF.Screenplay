using System;
using System.IO;
using System.Linq;
using CSF.Screenplay.Reporting.Adapters;
using CSF.Screenplay.Reporting.Models;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// An <see cref="IReportWriter"/> which writes the report to a <c>System.IO.TextWriter</c>, be that a text file or
  /// an output stream.  This writer uses a simple text-based format to render the results of the report in a
  /// human-readable format.
  /// </summary>
  public class TextReportWriter : IReportWriter
  {
    const char INDENT_CHAR = ' ';
    const int INDENT_WIDTH = 4, RESULT_OR_FAILURE_INDENT_WIDTH = 6;

    readonly TextWriter writer;
    readonly IObjectFormattingService formattingService;

    /// <summary>
    /// Write the specified report to the text writer.
    /// </summary>
    /// <param name="report">Report model.</param>
    public void Write(IReport report)
    {
      var hierarchy = new HierarchicalReportAdapter(report);

      foreach(var scenario in hierarchy.GetFeatures().SelectMany(x => x.Scenarios))
      {
        WriteScenario(scenario);
      }
    }

    void WriteScenario(IScenario scenario)
    {
      if(scenario == null)
        throw new ArgumentNullException(nameof(scenario));

      var scenarioWithMetadata = new ScenarioMetadataAdapter(scenario);
      WriteScenarioHeader(scenarioWithMetadata);

      foreach(var reportable in scenario.Reportables)
      {
        WriteReportable(reportable);
      }
    }

    void WriteScenarioHeader(IProvidesScenarioMetadata scenario)
    {
      writer.WriteLine();

      var featureText = $"Feature:  {scenario.GetPrintableFeatureName()}";
      if(featureText != null)
        writer.WriteLine(featureText);

      writer.WriteLine($"Scenario: {scenario.GetPrintableScenarioName()}");

      WriteScenarioOutcome(scenario);
    }

    void WriteReportable(Models.IReportable reportable, int currentIndentLevel = 0)
    {
      if(reportable == null)
        throw new ArgumentNullException(nameof(reportable));

      var performances = new [] {
        ReportableType.Success,
        ReportableType.SuccessWithResult,
        ReportableType.Failure,
        ReportableType.FailureWithError,
      };

      if(reportable.Type == ReportableType.GainAbility)
        WriteGainAbility(reportable, currentIndentLevel);
      else if(performances.Contains(reportable.Type))
        WritePerformance(reportable, currentIndentLevel);
      else
        throw new ArgumentException(Resources.ExceptionFormats.ReportableMustBeGainAbilityOrPerformance);
    }

    void WriteGainAbility(Models.IReportable reportable, int currentIndentLevel)
    {
      WriteIndent(currentIndentLevel);
      WritePerformanceType(reportable, currentIndentLevel);

      writer.Write(reportable.Report);
      writer.WriteLine();
    }

    void WritePerformance(Models.IReportable reportable, int currentIndentLevel)
    {
      WriteIndent(currentIndentLevel);
      WritePerformanceType(reportable, currentIndentLevel);

      writer.Write(reportable.Report);
      writer.WriteLine();

      if(reportable.Type == ReportableType.SuccessWithResult)
        WriteResult(reportable, currentIndentLevel);
      else if(reportable.Type == ReportableType.Failure)
        WriteFailure(reportable, currentIndentLevel);
      else if(reportable.Type == ReportableType.FailureWithError && !reportable.Reportables.Any())
        WriteFailure(reportable, currentIndentLevel);

      foreach(var child in reportable.Reportables)
      {
        WriteReportable(child, currentIndentLevel + 1);
      }
    }

    string GetPerformanceTypeString(ReportableCategory type, int currentIndentLevel)
    {
      if(type == 0)
        return String.Empty;

      // Don't keep writing the performance type after the base indent level
      if(currentIndentLevel > 0)
        return String.Empty;

      return type.ToString();
    }

    void WriteScenarioOutcome(IProvidesScenarioMetadata scenario)
    {
      string outcome;

      if(scenario.IsSuccess) outcome = "Success";
      else if(scenario.IsFailure) outcome = "Failure";
      else outcome = "Inconclusive";

      writer.WriteLine($"**** {outcome} ****");
    }

    void WriteIndent(int currentLevel)
    {
      writer.Write(GetIndent(currentLevel));
    }

    void WriteResultOrFailureIndent(int currentLevel)
    {
      WriteIndent(currentLevel);
      writer.Write(new String(INDENT_CHAR, RESULT_OR_FAILURE_INDENT_WIDTH));
    }

    void WritePerformanceType(Models.IReportable reportable, int currentIndentLevel)
    {
      writer.Write("{0,5} ", GetPerformanceTypeString(reportable.Category, currentIndentLevel));
    }

    void WriteResult(Models.IReportable reportable, int currentIndentLevel)
    {
      WriteResultOrFailureIndent(currentIndentLevel);
      writer.WriteLine("Result: {0}", Format(reportable.Result));
    }

    void WriteFailure(Models.IReportable reportable, int currentIndentLevel)
    {
      WriteResultOrFailureIndent(currentIndentLevel);

      if(reportable.Error == null)
      {
        writer.WriteLine("FAILED");
        return;
      }

      writer.WriteLine("FAILED: {0}", reportable.Error);
    }

    string Format(object obj) => formattingService.Format(obj);

    string GetIndent(int currentLevel) 
      => String.Join(String.Empty, Enumerable.Range(0, currentLevel).Select(x => new String(INDENT_CHAR, INDENT_WIDTH)));

    /// <summary>
    /// Initializes a new instance of the <see cref="TextReportWriter"/> class.
    /// </summary>
    /// <param name="writer">Writer.</param>
    public TextReportWriter(TextWriter writer)
    {
      if(writer == null)
        throw new ArgumentNullException(nameof(writer));

      this.writer = writer;
      formattingService = ObjectFormattingService.Default;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.TextReportWriter"/> class.
    /// </summary>
    /// <param name="writer">Writer.</param>
    /// <param name="formattingService">Formatting service.</param>
    public TextReportWriter(TextWriter writer, IObjectFormattingService formattingService)
      : this(writer)
    {
      this.formattingService = formattingService ?? ObjectFormattingService.Default;
    }

    /// <summary>
    /// Write the report to a file path.
    /// </summary>
    /// <param name="report">The report.</param>
    /// <param name="path">Destination file path.</param>
    /// <param name="formattingService">Object formatting service.</param>
    public static void WriteToFile(Report report, string path, IObjectFormattingService formattingService = null)
    {
      using(var writer = new StreamWriter(path))
      {
        var reportWriter = new TextReportWriter(writer, formattingService);
        reportWriter.Write(report);
        writer.Flush();
      }
    }
  }
}
