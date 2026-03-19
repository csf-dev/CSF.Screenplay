using System.IO;
using System.Linq;
using System.Text;
using CSF.Screenplay.ReportModel;

namespace CSF.Screenplay.Reporting;

[TestFixture, Parallelizable]
public class ScreenplayReportSerializerTests
{
        const string reportJson = @"{""Metadata"": {""Timestamp"": ""2021-01-01T00:00:00Z"",""ReportVersion"": ""2.0.0""},
""Performances"": [
    {
        ""NamingHierarchy"": [
            {""Id"": ""1"",""Name"": ""First""},
            {""Id"": ""2"",""Name"": ""Second""}
        ],
        ""Reportables"": [
            {
                ""Kind"": ""PerformableReport"",
                ""Report"": ""This is a report string"",
                ""Actor"": ""Joe"",
                ""Type"": ""APerformableType"",
                ""Phase"": ""Given"",
                ""Result"": ""A result"",
                ""HasResult"": true,
                ""Assets"": [{""Path"": ""../a/file/path.txt"",""Summary"": ""This is a test asset""}],
                ""Reportables"": [
                    {
                        ""Kind"": ""ActorCreatedReport"",
                        ""Report"": ""This is a nested report string"",
                        ""Actor"": ""Joe""
                    }
                ]
            }
        ]
    }
]}";

    [Test, AutoMoqData]
    public void DeserializeAsyncShouldThrowIfStreamIsNull(ScreenplayReportSerializer sut)
    {
        Assert.That(() => sut.DeserializeAsync(null), Throws.ArgumentNullException);
    }

    [Test, AutoMoqData]
    public async Task DeserializeAsyncShouldDeserializeAStream(ScreenplayReportSerializer sut)
    {
        var bytes = Encoding.UTF8.GetBytes(@"{""Metadata"": { ""ReportVersion"": ""foo bar"" }}");
        var stream = new MemoryStream(bytes);
        var report = await sut.DeserializeAsync(stream);
        Assert.That(report.Metadata.ReportVersion, Is.EqualTo("foo bar"));
    }

    [Test, AutoMoqData]
    public async Task DeserializeAsyncShouldReturnAScreenplayReport(ScreenplayReportSerializer sut)
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(reportJson));
        var result = await sut.DeserializeAsync(stream);

        using var scope = Assert.EnterMultipleScope();
            Assert.That(result, Is.Not.Null, "The deserialized report should not be null.");
            Assert.That(result.Metadata.Timestamp, Is.EqualTo(new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero)), "The timestamp should be correct.");
            Assert.That(result.Metadata.ReportVersion, Is.EqualTo("2.0.0"), "The report format version should be correct.");
            Assert.That(result.Performances, Has.Count.EqualTo(1), "There should be one performance.");

            var firstPerformance = result.Performances.Single();
            Assert.That(firstPerformance.NamingHierarchy, Has.Count.EqualTo(2), "There should be two naming hierarchy items.");
            Assert.That(firstPerformance.NamingHierarchy.Select(x => x.Name), Is.EquivalentTo(new[] { "First", "Second" }), "The naming hierarchy names should be correct.");
            Assert.That(firstPerformance.Reportables, Has.Count.EqualTo(1), "There should be one reportable.");

            var firstReportable = firstPerformance.Reportables.Single();
            Assert.That(firstReportable.Report, Is.EqualTo("This is a report string"), "The report string should be correct.");
            Assert.That(firstReportable.Actor, Is.EqualTo("Joe"), "The actor name should be correct.");
            Assert.That(firstReportable, Is.InstanceOf<PerformableReport>(), "The reportable should be a PerformableReport.");

            var performableReport = (PerformableReport) firstReportable;
            Assert.That(performableReport.Type, Is.EqualTo("APerformableType"), "The performable type should be correct.");
            Assert.That(performableReport.Phase, Is.EqualTo("Given"), "The performance phase should be correct.");
            Assert.That(performableReport.Result, Is.EqualTo("A result"), "The result should be correct.");
            Assert.That(performableReport.HasResult, Is.True, "The HasResult flag should be correct.");
            Assert.That(performableReport.Assets, Has.Count.EqualTo(1), "There should be one asset.");
            Assert.That(performableReport.Assets.Single().Path, Is.EqualTo("../a/file/path.txt"), "The asset file path should be correct.");
            Assert.That(performableReport.Assets.Single().Summary, Is.EqualTo("This is a test asset"), "The asset file summary should be correct.");
            Assert.That(performableReport.Reportables, Has.Count.EqualTo(1), "There should be one nested reportable.");
    }

    [Test, AutoMoqData]
    public void SerializeAsyncShouldThrowIfReportIsNull(ScreenplayReportSerializer sut)
    {
        Assert.That(() => sut.SerializeAsync(null), Throws.ArgumentNullException);
    }

    [Test, AutoMoqData]
    public async Task SerializeAsyncShouldSerializeToAStream(ScreenplayReportSerializer sut)
    {
        var report = new ScreenplayReport() { Metadata = new () { ReportVersion = "foo bar", Timestamp = DateTimeOffset.UtcNow } };
        var stream = await sut.SerializeAsync(report);
        using var reader = new StreamReader(stream);
        var content = await reader.ReadToEndAsync();
        Assert.That(content, Does.Match(@"^\{""Metadata"":\{""Timestamp"":""[\d-]{10}T[\d:.]{10,16}\+00:00"",""ReportVersion"":""foo bar""\},""Performances"":\[\]\}$"));
    }
}