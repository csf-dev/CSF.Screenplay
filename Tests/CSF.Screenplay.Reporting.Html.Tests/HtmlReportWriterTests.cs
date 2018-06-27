using System.IO;
using System.Text;
using CSF.Screenplay.Reporting.Tests;
using CSF.Screenplay.Reporting.Tests.Autofixture;
using CSF.Screenplay.ReportModel;
using NUnit.Framework;

namespace CSF.Screenplay.Reporting.Html.Tests
{
  [TestFixture]
  public class HtmlReportWriterTests
  {
    [Test,AutoMoqData]
    public void Write_can_create_a_document_without_crashing([RandomReport] Report report)
    {
      // Arrange
      using(var writer = GetReportOutput())
      {
        var sut = new HtmlReportRenderer(writer);

        // Act & assert
        Assert.DoesNotThrow(() => sut.Render(report));
      }
    }

    TextWriter GetReportOutput()
    {
      // Uncomment this line to write the report to a file instead of a throwaway string
      //return new StreamWriter("HtmlReportWriterTests.html");

      var sb = new StringBuilder();
      return new StringWriter(sb);
    }
  }
}
