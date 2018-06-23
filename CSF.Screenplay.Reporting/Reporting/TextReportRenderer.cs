using System;
using System.IO;
using System.Linq;
using CSF.Screenplay.ReportModel;
using CSF.Screenplay.ReportModel.Adapters;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// An <see cref="IRendersReport"/> which writes the report to a <c>System.IO.TextWriter</c>, be that a text file or
  /// an output stream.  This writer uses a simple text-based format to render the results of the report in a
  /// human-readable format.
  /// </summary>
  public class TextReportRenderer : IRendersReport, IObservesScenarioCompletion, IDisposable
  {
    const char INDENT_CHAR = ' ';
    const int INDENT_WIDTH = 4, RESULT_OR_FAILURE_INDENT_WIDTH = 6;

    readonly object writeLock = new object();
    readonly TextWriter writer;
    readonly bool disposeWriter;

    /// <summary>
    /// Write the specified report to the text writer.
    /// </summary>
    /// <param name="report">Report model.</param>
    public void Render(IReport report)
    {
      var hierarchy = new HierarchicalReportAdapter(report);

      foreach(var scenario in hierarchy.GetFeatures().SelectMany(x => x.Scenarios))
      {
        WriteScenario(scenario);
      }
    }

    /// <summary>
    /// Subscribes to an object which exposes completed scenarios.
    /// </summary>
    /// <param name="scenarioProvider">Scenario provider.</param>
    public void Subscribe(IExposesCompletedScenarios scenarioProvider)
    {
      if(scenarioProvider == null)
        throw new ArgumentNullException(nameof(scenarioProvider));

      scenarioProvider.ScenarioCompleted += OnScenarioCompleted;
    }

    /// <summary>
    /// Unsubscribes from an object which exposes completed scenarios.
    /// </summary>
    /// <param name="scenarioProvider">Scenario provider.</param>
    public void Unsubscribe(IExposesCompletedScenarios scenarioProvider)
    {
      if(scenarioProvider == null)
        throw new ArgumentNullException(nameof(scenarioProvider));

      scenarioProvider.ScenarioCompleted -= OnScenarioCompleted;
    }

    void OnScenarioCompleted(object sender, EventArgs ev)
    {
      var args = ev as ScenarioCompletedEventArgs;
      if(args?.Scenario == null) return;

      WriteScenario(args?.Scenario);
    }

    void WriteScenario(IScenario scenario)
    {
      if(scenario == null)
        throw new ArgumentNullException(nameof(scenario));

      lock(writeLock)
      {
        WriteScenarioLocked(scenario);
        writer.Flush();
      }
    }

    void WriteScenarioLocked(IScenario scenario)
    {
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

    void WriteReportable(IReportable reportable, int currentIndentLevel = 0)
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

    void WriteGainAbility(IReportable reportable, int currentIndentLevel)
    {
      WriteIndent(currentIndentLevel);
      WritePerformanceType(reportable, currentIndentLevel);

      writer.Write(reportable.Report);
      writer.WriteLine();
    }

    void WritePerformance(IReportable reportable, int currentIndentLevel)
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

    void WritePerformanceType(IReportable reportable, int currentIndentLevel)
    {
      writer.Write("{0,5} ", GetPerformanceTypeString(reportable.Category, currentIndentLevel));
    }

    void WriteResult(IReportable reportable, int currentIndentLevel)
    {
      WriteResultOrFailureIndent(currentIndentLevel);
      writer.WriteLine("Result: " + reportable.Result);
    }

    void WriteFailure(IReportable reportable, int currentIndentLevel)
    {
      WriteResultOrFailureIndent(currentIndentLevel);

      if(reportable.Error == null)
      {
        writer.WriteLine("FAILED");
        return;
      }

      writer.WriteLine("FAILED: {0}", reportable.Error);
    }

    string GetIndent(int currentLevel) 
      => String.Join(String.Empty, Enumerable.Range(0, currentLevel).Select(x => new String(INDENT_CHAR, INDENT_WIDTH)));

    #region IDisposable Support
    bool disposedValue = false;

    /// <summary>
    /// Dispose of the current instance
    /// </summary>
    /// <param name="disposing">If set to <c>true</c> then this disposal is explicit.</param>
    protected virtual void Dispose(bool disposing)
    {
      if(!disposedValue)
      {
        if(disposing)
        {
          if(disposeWriter)
            writer.Dispose();
        }

        disposedValue = true;
      }
    }

    /// <summary>
    /// Releases all resource used by the <see cref="T:CSF.Screenplay.Reporting.TextReportWriter"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose()"/> when you are finished using the
    /// <see cref="T:CSF.Screenplay.Reporting.TextReportWriter"/>. The <see cref="Dispose()"/> method leaves the
    /// <see cref="T:CSF.Screenplay.Reporting.TextReportWriter"/> in an unusable state. After calling
    /// <see cref="Dispose()"/>, you must release all references to the
    /// <see cref="T:CSF.Screenplay.Reporting.TextReportWriter"/> so the garbage collector can reclaim the memory that
    /// the <see cref="T:CSF.Screenplay.Reporting.TextReportWriter"/> was occupying.</remarks>
    public void Dispose()
    {
      Dispose(true);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="TextReportRenderer"/> class.
    /// </summary>
    /// <param name="writer">Writer.</param>
    /// <param name="disposeWriter">Indicates whether or not the <paramref name="writer"/> should be diposed with this instance.</param>
    public TextReportRenderer(TextWriter writer, bool disposeWriter = true)
    {
      if(writer == null)
        throw new ArgumentNullException(nameof(writer));

      this.writer = writer;
      this.disposeWriter = disposeWriter;
    }

    /// <summary>
    /// Write the report to a file path.
    /// </summary>
    /// <param name="report">The report to write.</param>
    /// <param name="path">Destination file path.</param>
    public static void WriteToFile(Report report, string path)
    {
      using(var reportWriter = new TextReportRenderer(new StreamWriter(path)))
      {
        reportWriter.Render(report);
      }
    }

    /// <summary>
    /// Creates an instance of <see cref="TextReportRenderer"/> for writing to a given file path.
    /// </summary>
    /// <param name="path">Destination file path.</param>
    public static TextReportRenderer CreateForFile(string path)
    {
      var writer = new StreamWriter(path);
      return new TextReportRenderer(writer);
    }
  }
}
