using NUnit.Framework;
using CSF.Screenplay.Reporting.Models;
using CSF.Screenplay.Reporting.Html.Tests.Autofixture;
using System.Text;
using System.IO;

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
      var sb = new StringBuilder();
      using(var writer = new StringWriter(sb))
      {
        var sut = new HtmlReportWriter(writer, formatService);

        // Act & assert
        Assert.DoesNotThrow(() => sut.Write(report));
      }
    }
  }
}
