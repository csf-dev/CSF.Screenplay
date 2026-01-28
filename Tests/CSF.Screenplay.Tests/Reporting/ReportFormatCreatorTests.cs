using System.Linq;

namespace CSF.Screenplay.Reporting;

[TestFixture,Parallelizable]
public class ReportFormatCreatorTests
{
    [TestCase("", "")]
    [TestCase("foo bar baz", "foo bar baz")]
    [TestCase("foo {{bar}} baz", "foo {{bar}} baz")]
    [TestCase("{{prefix{{{bar}}}suffix}}", "{{prefix{{{0}}}suffix}}")]
    [TestCase("{{prefix{{{foo}}}content{{{bar}}}suffix}}", "{{prefix{{{0}}}content{{{1}}}suffix}}")]
    [TestCase("foo {bar} baz {foo} {bar} {baz}", "foo {0} baz {1} {0} {2}")]
    [TestCase("{", "{")]
    [TestCase("}", "}")]
    [TestCase("{foo} {bar", "{0} {bar")]
    [TestCase("{{{{foo}}}}", "{{{{foo}}}}")]
    [TestCase("{Actor} opens their browser at {UriName}: {Uri}", "{0} opens their browser at {1}: {2}")]
    public void GetReportFormatShouldReturnTheCorrectFormattedTemplate(string original, string expected)
    {
        var sut = new ReportFormatCreator();
        var actual = sut.GetReportFormat(original, Array.Empty<object>());

        Assert.Multiple(() =>
        {
            Assert.That(actual?.OriginalTemplate, Is.EqualTo(original), "Original value is unchanged");
            Assert.That(actual?.FormatTemplate, Is.EqualTo(expected), "Expected value is correct");
        });
    }

    [TestCase("", "", "", "")]
    [TestCase("foo bar baz", "", "", "")]
    [TestCase("foo {{bar}} baz", "", "", "")]
    [TestCase("{{prefix{{{bar}}}suffix}}", "bar", "", "")]
    [TestCase("{{prefix{{{foo}}}content{{{bar}}}suffix}}", "foo", "bar", "")]
    [TestCase("foo {bar} baz {foo} {bar} {baz}", "bar", "foo", "baz")]
    [TestCase("{", "", "", "")]
    [TestCase("}", "", "", "")]
    [TestCase("{foo} {bar", "foo", "", "")]
    [TestCase("{{{{foo}}}}", "", "", "")]
    [TestCase("{Actor} opens their browser at {UriName}: {Uri}", "Actor", "UriName", "Uri")]
    public void GetReportFormatShouldReturnTheCorrectObjectNames(string template, string name1, string name2, string name3)
    {
        var expected = new[] { name1, name2, name3 }.Where(x => x.Length > 0).ToList();
        var sut = new ReportFormatCreator();
        var actual = sut.GetReportFormat(template, Array.Empty<object>());

        Assert.That(actual?.Values.Select(x => x.Name).ToList(), Is.EqualTo(expected));
    }

    [Test,AutoMoqData]
    public void GetReportFormatShouldReturnTheCorrectObjectsWithTheCorrectNames(ReportFormatCreator sut, object value1, object value2, object value3)
    {
        var template = "{foo} content {bar} content {baz}";
        var actual = sut.GetReportFormat(template, [value1, value2, value3]);
        Assert.Multiple(() =>
        {
            Assert.That(actual.Values, Has.Count.EqualTo(3), "Count");
            Assert.That(actual.Values, Has.One.Matches<NameAndValue>(x => x.Name == "foo" && x.Value == value1), "Value 1");
            Assert.That(actual.Values, Has.One.Matches<NameAndValue>(x => x.Name == "bar" && x.Value == value2), "Value 2");
            Assert.That(actual.Values, Has.One.Matches<NameAndValue>(x => x.Name == "baz" && x.Value == value3), "Value 3");
        });
    }

    [Test,AutoMoqData]
    public void GetReportFormatShouldReturnTheCorrectObjectsWithTheCorrectNamesIfMoreValuesThanPlaceholders(ReportFormatCreator sut, object value1, object value2, object value3)
    {
        var template = "{foo} content {bar} content";
        var actual = sut.GetReportFormat(template, [value1, value2, value3]);

        Assert.Multiple(() =>
        {
            Assert.That(actual.Values, Has.Count.EqualTo(2), "Count");
            Assert.That(actual.Values, Has.One.Matches<NameAndValue>(x => x.Name == "foo" && x.Value == value1), "Value 1");
            Assert.That(actual.Values, Has.One.Matches<NameAndValue>(x => x.Name == "bar" && x.Value == value2), "Value 2");
        });
    }

    [Test,AutoMoqData]
    public void GetReportFormatShouldReturnTheCorrectObjectsWithTheCorrectNamesIfMorePlaceholdersThanValues(ReportFormatCreator sut, object value1, object value2)
    {
        var template = "{foo} content {bar} content {baz}";
        var actual = sut.GetReportFormat(template, [value1, value2]);

        Assert.Multiple(() =>
        {
            Assert.That(actual.Values, Has.Count.EqualTo(3), "Count");
            Assert.That(actual.Values, Has.One.Matches<NameAndValue>(x => x.Name == "foo" && x.Value == value1), "Value 1");
            Assert.That(actual.Values, Has.One.Matches<NameAndValue>(x => x.Name == "bar" && x.Value == value2), "Value 2");
            Assert.That(actual.Values, Has.One.Matches<NameAndValue>(x => x.Name == "baz" && x.Value == null), "Value 3");
        });
    }
}