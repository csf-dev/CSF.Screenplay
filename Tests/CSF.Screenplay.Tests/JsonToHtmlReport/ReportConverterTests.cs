using System.IO;
using System.Text;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.ReportModel;
using NUnit.Framework.Legacy;

namespace CSF.Screenplay.JsonToHtmlReport;

[TestFixture, Parallelizable]
public class ReportConverterTests
{
    [Test, AutoMoqData]
    public async Task ConvertAsyncShouldWriteTheReportUsingATemplate([NoAutoProperties] ScreenplayReport report,
                                                                     [Frozen] IDeserializesReport deserializer,
                                                                     [Frozen] ISerializesReport serializer,
                                                                     [Frozen] IGetsHtmlTemplate templateReader,
                                                                     [Frozen] IEmbedsReportAssets assetsEmbedder,
                                                                     ReportConverter sut)
    {
        var options = new ReportConverterOptions()
        {
            ReportPath = Path.Combine(Environment.CurrentDirectory, "SampleReport.json"),
            OutputPath = Path.Combine(Environment.CurrentDirectory, "ReportConverterTestsOutput.html")
        };
        if(File.Exists(options.OutputPath))
            File.Delete(options.OutputPath);
        
        Mock.Get(deserializer).Setup(x => x.DeserializeAsync(It.IsAny<Stream>())).Returns(Task.FromResult(report));
        Mock.Get(assetsEmbedder).Setup(x => x.EmbedReportAssetsAsync(report, options)).Returns(Task.CompletedTask);
        var bytes = Encoding.UTF8.GetBytes("SAMPLE REPORT");
        using var stream = new MemoryStream(bytes);
        Mock.Get(serializer).Setup(x => x.SerializeAsync(report)).Returns(Task.FromResult<Stream>(stream));
        Mock.Get(templateReader).Setup(x => x.ReadTemplate()).Returns(Task.FromResult("<html><script><!-- REPORT_PLACEHOLDER --></script></html>"));
        
        await sut.ConvertAsync(options);

        using var scope = Assert.EnterMultipleScope();
            Assert.That(options.ReportPath, Does.Exist, "Report exists");
            using var reader = new StreamReader(File.Open(options.OutputPath, FileMode.Open));
            var reportContent = await reader.ReadToEndAsync();
            Assert.That(reportContent, Is.EqualTo("<html><script>SAMPLE REPORT</script></html>"), "Report has correct content");
    }
}