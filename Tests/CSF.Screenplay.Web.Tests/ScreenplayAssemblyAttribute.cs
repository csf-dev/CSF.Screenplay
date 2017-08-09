using System;
using System.IO;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Reporting.Models;
using CSF.Screenplay.Web.Abilities;

namespace CSF.Screenplay.Web.Tests
{
  public class ScreenplayAssemblyAttribute : NUnit.ScreenplayAssemblyAttribute
  {
    protected override IUriTransformer GetUriTransformer()
      => new RootUriPrependingTransformer("http://localhost:8080/");

    protected override void WriteReport(Report report)
    {
      using(var writer = new StreamWriter("screenplay-report.txt"))
      {
        var reportWriter = new TextReportWriter(writer);
        reportWriter.Write(report);
        writer.Flush();
      }
    }

    /// <summary>
    /// Gets a helper type which sets up reporting for the cast.
    /// </summary>
    /// <returns>The reporting helper.</returns>
    /// <param name="cast">Cast.</param>
    /// <param name="reporter">Reporter.</param>
    protected override NUnit.CastReportingHelper GetReportingHelper(ICast cast,
                                                                    IReporter reporter,
                                                                    ScreenplayContext context)
    => new CastReportingHelper(cast, reporter, context);
  }
}
