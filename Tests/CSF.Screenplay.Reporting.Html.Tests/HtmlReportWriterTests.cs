using NUnit.Framework;
using CSF.Screenplay.Reporting.Models;
using CSF.Screenplay.Reporting.Tests.Autofixture;
using System.IO;
using System.Linq;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Abilities;
using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Reporting.Tests;
using System.Text;

namespace CSF.Screenplay.Reporting.Html.Tests
{
  [TestFixture]
  public class HtmlReportWriterTests
  {
    [Test,AutoMoqData]
    public void Write_can_create_a_document_without_crashing([RandomReport] Report report,
                                                             [StringFormat] IObjectFormattingService formatService)
    {
      // Arrange
      using(var writer = GetReportOutput())
      {
        var sut = new HtmlReportWriter(writer, formatService);

        // Act & assert
        Assert.DoesNotThrow(() => sut.Write(report));
      }
    }

    [Test,AutoMoqData,Description("Reproduces #131")]
    public void Write_does_not_crash_when_using_ability_with_no_public_methods([RandomReport] Report report,
                                                                               [StringFormat] IObjectFormattingService formatService,
                                                                               IActor actor,
                                                                               ReportableType outcome)
    {
      // Arrange
      var ability = new AbilityWithOnlyExplicitInterfaceImplementations();
      report.Scenarios.First().Reportables.Add(new GainAbility(actor, outcome, ability));

      using(var writer = GetReportOutput())
      {
        var sut = new HtmlReportWriter(writer, formatService);

        // Act & assert
        Assert.DoesNotThrow(() => sut.Write(report));
      }
    }

    [Test,AutoMoqData,Description("Reproduces #131")]
    public void Write_does_not_crash_when_using_performance_with_no_public_methods([RandomReport] Report report,
                                                                                   [StringFormat] IObjectFormattingService formatService,
                                                                                   IActor actor,
                                                                                   ReportableType outcome)
    {
      // Arrange
      var performable = new PerformableWithOnlyExplicitInterfaceImplementations();
      report.Scenarios.First().Reportables.Add(new Performance(actor, outcome, performable));

      using(var writer = GetReportOutput())
      {
        var sut = new HtmlReportWriter(writer, formatService);

        // Act & assert
        Assert.DoesNotThrow(() => sut.Write(report));
      }
    }

    TextWriter GetReportOutput()
    {
      // Uncomment this line to write the report to a file instead of a throwaway string
      //return new StreamWriter("HtmlReportWriterTests.html");

      var sb = new StringBuilder();
      return new StringWriter(sb);
    }

    public class AbilityWithOnlyExplicitInterfaceImplementations : IAbility
    {
      void IDisposable.Dispose() { /* Intentional no-op */ }

      string IProvidesReport.GetReport(INamed actor) => "Sample string";
    }

    public class PerformableWithOnlyExplicitInterfaceImplementations : IPerformable
    {
      string IProvidesReport.GetReport(INamed actor) => "Sample string";

      void IPerformable.PerformAs(IPerformer actor) { /* Intentional no-op */ }
    }
  }
}
