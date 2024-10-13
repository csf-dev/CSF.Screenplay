using CSF.Screenplay.Reporting;

namespace CSF.Screenplay;

[TestFixture,Parallelizable]
public class ReportFragmentFormatterTests
{
    [Test,AutoMoqData]
    public void FormatShouldReturnCorrectReportFragment([Frozen] IGetsReportFormat formatProvider,
                                                        [Frozen] IGetsValueFormatter formatterProvider,
                                                        ReportFragmentFormatter sut,
                                                        IValueFormatter formatter)
    {
        var template = "{Actor} washes {Count} dishes";
        object[] values = ["Joe", 5];
        var expectedFormat = "{0} washes {1} dishes";
        Mock.Get(formatProvider)
            .Setup(x => x.GetReportFormat(template, values))
            .Returns(new ReportFormat(template, expectedFormat, [new ("Actor", "Joe"), new ("Count", 5)]));
        Mock.Get(formatter).Setup(x => x.FormatForReport(It.IsAny<object>())).Returns((object obj) => obj.ToString()!);
        Mock.Get(formatterProvider).Setup(x => x.GetValueFormatter(It.IsAny<object>())).Returns(formatter);

        var result = sut.Format(template, values);

        Assert.Multiple(() =>
        {
            Assert.That(result.OriginalTemplate, Is.EqualTo(template), "Original template is correct");
            Assert.That(result.FormattedFragment, Is.EqualTo("Joe washes 5 dishes"), "Formatted fragment is correct");
            Assert.That(result.PlaceholderValues, Has.Count.EqualTo(2), "Count of placeholder items");
            Assert.That(result.PlaceholderValues, Has.One.Matches<NameAndValue>(x => x.Name == "Actor" && Equals(x.Value, "Joe")), "First placeholder item");
            Assert.That(result.PlaceholderValues, Has.One.Matches<NameAndValue>(x => x.Name == "Count" && Equals(x.Value, 5)), "Second placeholder item");
        });
    }
}