using System.IO;
using System.Linq;
using System.Text;
using CSF.Screenplay.ReportModel;

namespace CSF.Screenplay.Reporting
{
    [TestFixture,Parallelizable]
    public class JsonScreenplayReportReaderTests
    {
        const string reportJson = @"{""Metadata"": {""Timestamp"": ""2021-01-01T00:00:00Z"",""ReportFormatVersion"": ""2.0.0""},
""Performances"": [
    {
        ""NamingHierarchy"": [
            {""Identifier"": ""1"",""Name"": ""First""},
            {""Identifier"": ""2"",""Name"": ""Second""}
        ],
        ""Reportables"": [
            {
                ""Type"": ""PerformableReport"",
                ""Report"": ""This is a report string"",
                ""ActorName"": ""Joe"",
                ""PerformableType"": ""APerformableType"",
                ""PerformancePhase"": ""Given"",
                ""Result"": ""A result"",
                ""HasResult"": true,
                ""Assets"": [{""FilePath"": ""../a/file/path.txt"",""FileSummary"": ""This is a test asset""}],
                ""Reportables"": [
                    {
                        ""Type"": ""ActorCreatedReport"",
                        ""Report"": ""This is a nested report string"",
                        ""ActorName"": ""Joe""
                    }
                ]
            }
        ]
    }
]}";

        [Test, AutoMoqData]
        public async Task DeserializeAsyncShouldReturnAScreenplayReport(ScreenplayReportSerializer sut)
        {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(reportJson));
            var result = await sut.DeserializeAsync(stream);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null, "The deserialized report should not be null.");
                Assert.That(result.Metadata.Timestamp, Is.EqualTo(new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc)), "The timestamp should be correct.");
                Assert.That(result.Metadata.ReportFormatVersion, Is.EqualTo("2.0.0"), "The report format version should be correct.");
                Assert.That(result.Performances, Has.Count.EqualTo(1), "There should be one performance.");

                var firstPerformance = result.Performances.Single();
                Assert.That(firstPerformance.NamingHierarchy, Has.Count.EqualTo(2), "There should be two naming hierarchy items.");
                Assert.That(firstPerformance.NamingHierarchy.Select(x => x.Name), Is.EquivalentTo(new[] { "First", "Second" }), "The naming hierarchy names should be correct.");
                Assert.That(firstPerformance.Reportables, Has.Count.EqualTo(1), "There should be one reportable.");

                var firstReportable = firstPerformance.Reportables.Single();
                Assert.That(firstReportable.Report, Is.EqualTo("This is a report string"), "The report string should be correct.");
                Assert.That(firstReportable.ActorName, Is.EqualTo("Joe"), "The actor name should be correct.");
                Assert.That(firstReportable, Is.InstanceOf<PerformableReport>(), "The reportable should be a PerformableReport.");

                var performableReport = (PerformableReport) firstReportable;
                Assert.That(performableReport.PerformableType, Is.EqualTo("APerformableType"), "The performable type should be correct.");
                Assert.That(performableReport.PerformancePhase, Is.EqualTo("Given"), "The performance phase should be correct.");
                Assert.That(performableReport.Result, Is.EqualTo("A result"), "The result should be correct.");
                Assert.That(performableReport.HasResult, Is.True, "The HasResult flag should be correct.");
                Assert.That(performableReport.Assets, Has.Count.EqualTo(1), "There should be one asset.");
                Assert.That(performableReport.Assets.Single().FilePath, Is.EqualTo("../a/file/path.txt"), "The asset file path should be correct.");
                Assert.That(performableReport.Assets.Single().FileSummary, Is.EqualTo("This is a test asset"), "The asset file summary should be correct.");
                Assert.That(performableReport.Reportables, Has.Count.EqualTo(1), "There should be one nested reportable.");
            });
        }
    }
}