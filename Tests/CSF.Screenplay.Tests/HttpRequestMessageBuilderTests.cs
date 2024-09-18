using System.Net.Http;

namespace CSF.Screenplay;

[TestFixture,Parallelizable]
public class HttpRequestMessageBuilderTests
{
    [Test, AutoMoqData]
    public void IntendedUsageAsImmutableRecordShouldProvideExpectedResults()
    {
        // This test is more about proving the API and developer experience I want for this type
        // does actually work the way I'd like it to work.

        var baseValues = new HttpRequestMessageBuilder
        {
            RequestUri = new Uri("https://example.com"),
            Headers = new()
            {
                ["Cookie"] = "FooBar",
                ["Expires"] = "0",
            }
        };
        var modified = baseValues with { Headers = baseValues.Headers.WithItem("Content-Type", "text/json"), Method = HttpMethod.Post };

        Assert.That(modified, Is.EqualTo(new HttpRequestMessageBuilder
        {
            RequestUri = new Uri("https://example.com"),
            Headers = new()
            {
                ["Cookie"] = "FooBar",
                ["Expires"] = "0",
                ["Content-Type"] = "text/json",
            },
            Method = HttpMethod.Post,
        }));
    }
}