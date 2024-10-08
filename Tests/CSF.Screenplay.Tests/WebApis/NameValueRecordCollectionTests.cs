using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.WebApis;

[TestFixture,Parallelizable]
public class NameValueRecordCollectionTests
{
    [Test,AutoMoqData]
    public void IntendedUsageAsImmutableRecordShouldProvideExpectedResults()
    {
        // This test is more about proving the API and developer experience I want for this type
        // does actually work the way I'd like it to work.

        var baseValues = new NameValueRecordCollection<string, string>
        {
            ["foo"] = "bar",
            ["wibble"] = "wobble",
        };
        var addedValues = baseValues.WithItem("foo", null).WithItem("spong", "blah");

        Assert.That(addedValues.ToList(), Is.EquivalentTo(new KeyValuePair<string, string>[] { new("wibble", "wobble"), new("spong", "blah") }));
    }

    [Test,AutoMoqData]
    public void GetHashCodeShouldReturnTheSameValueIfUsedTwice()
    {
        var sut = new NameValueRecordCollection<string, string>
        {
            ["foo"] = "bar",
            ["wibble"] = "wobble",
        };

        var firstHashCode = sut.GetHashCode();
        var secondHashCode = sut.GetHashCode();

        Assert.That(firstHashCode, Is.EqualTo(secondHashCode));
    }

    [Test,AutoMoqData]
    public void GetFromIndexerShouldReturnExpectedResult()
    {
        var sut = new NameValueRecordCollection<string, string>
        {
            ["foo"] = "bar",
            ["wibble"] = "wobble",
        };

        Assert.That(sut["foo"], Is.EqualTo("bar"));
    }

    [Test,AutoMoqData]
    public void GetFromNonexistentIndexerShouldReturnExpectedResult()
    {
        var sut = new NameValueRecordCollection<string, string>
        {
            ["foo"] = "bar",
            ["wibble"] = "wobble",
        };

        Assert.That(sut["nope"], Is.Null);
    }
}