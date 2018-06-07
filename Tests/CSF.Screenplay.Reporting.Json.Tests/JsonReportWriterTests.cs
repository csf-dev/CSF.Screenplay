using NUnit.Framework;
using CSF.Screenplay.Reporting.Models;
using CSF.Screenplay.Reporting.Tests.Autofixture;
using System.IO;
using CSF.Screenplay.Reporting.Tests;
using System.Text;

namespace CSF.Screenplay.Reporting.Json.Tests
{
  [TestFixture]
  public class JsonReportWriterTests
  {
    [Test,AutoMoqData]
    public void Write_can_create_a_document_without_crashing([RandomReport] Report report)
    {
      // Arrange
      using(var writer = GetReportOutput())
      {
        var sut = new JsonReportWriter(writer);

        // Act & assert
        Assert.DoesNotThrow(() => sut.Write(report));
      }
    }

    TextWriter GetReportOutput()
    {
      // Uncomment this line to write the report to a file instead of a throwaway string
      // return new StreamWriter("JsonReportWriterTests.json");

      var sb = new StringBuilder();
      return new StringWriter(sb);
    }
  }
}
