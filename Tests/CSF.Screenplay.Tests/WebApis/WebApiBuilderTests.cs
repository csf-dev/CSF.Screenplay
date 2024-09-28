using System.Net;
using System.Net.Http;
using static CSF.Screenplay.WebApis.WebApiBuilder;
using static CSF.Screenplay.PerformanceStarter;
using System.Net.Http.Json;

namespace CSF.Screenplay.WebApis;

[TestFixture,Parallelizable]
public class WebApiBuilderTests
{
    [Test,AutoMoqData]
    public async Task SendTheHttpRequestShouldGetAnActionForAnEndpoint([SendsMockHttpRequests] Actor actor)
    {
        var endpoint = new Endpoint("api/foo/bar", HttpMethod.Post);
        var sut = SendTheHttpRequest(endpoint);

        await sut.PerformAsAsync(actor);

        var makeWebApiRequests = actor.GetAbility<MakeWebApiRequests>();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        Mock.Get(makeWebApiRequests.DefaultClient)
            .Verify(x => x.SendAsync(It.Is<HttpRequestMessage>(m => m.RequestUri.OriginalString == "api/foo/bar" && m.Method == HttpMethod.Post),
                                     It.IsAny<CancellationToken>()),
                    Times.Once);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    [Test,AutoMoqData]
    public async Task SendTheHttpRequestShouldGetAnActionForAGenericEndpoint([SendsMockHttpRequests] Actor actor)
    {
        var endpoint = new Endpoint<string>("api/foo/bar", HttpMethod.Post);
        var sut = SendTheHttpRequest(endpoint);

        await sut.PerformAsAsync(actor);

        var makeWebApiRequests = actor.GetAbility<MakeWebApiRequests>();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        Mock.Get(makeWebApiRequests.DefaultClient)
            .Verify(x => x.SendAsync(It.Is<HttpRequestMessage>(m => m.RequestUri.OriginalString == "api/foo/bar" && m.Method == HttpMethod.Post),
                                     It.IsAny<CancellationToken>()),
                    Times.Once);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    [Test,AutoMoqData]
    public async Task SendTheHttpRequestShouldGetAnActionForAParameterizedEndpoint([SendsMockHttpRequests] Actor actor)
    {
        var endpoint = new IntegerParameterizedEndpoint("api/foo/bar", HttpMethod.Post);
        var sut = SendTheHttpRequest(endpoint, 5);

        await sut.PerformAsAsync(actor);

        var makeWebApiRequests = actor.GetAbility<MakeWebApiRequests>();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        Mock.Get(makeWebApiRequests.DefaultClient)
            .Verify(x => x.SendAsync(It.Is<HttpRequestMessage>(m => m.RequestUri.OriginalString == "api/foo/bar" && m.Method == HttpMethod.Post && ((StringContent) m.Content!).ReadAsStringAsync().Result == "5"),
                                     It.IsAny<CancellationToken>()),
                    Times.Once);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    [Test,AutoMoqData]
    public async Task SendTheHttpRequestShouldGetAnActionForAGenericParameterizedEndpoint([SendsMockHttpRequests] Actor actor)
    {
        var endpoint = new IntegerGenericParameterizedEndpoint("api/foo/bar", HttpMethod.Post);
        var sut = SendTheHttpRequest(endpoint, 5);

        await sut.PerformAsAsync(actor);

        var makeWebApiRequests = actor.GetAbility<MakeWebApiRequests>();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        Mock.Get(makeWebApiRequests.DefaultClient)
            .Verify(x => x.SendAsync(It.Is<HttpRequestMessage>(m => m.RequestUri.OriginalString == "api/foo/bar" && m.Method == HttpMethod.Post && ((StringContent) m.Content!).ReadAsStringAsync().Result == "5"),
                                     It.IsAny<CancellationToken>()),
                    Times.Once);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    [Test,AutoMoqData]
    public async Task GetTheJsonResultShouldGetAnActionWhichYieldsTheExpectedPayload([Frozen] SerializableObject expectedPayload,
                                                                                     [SendsMockHttpRequests] Actor actor)
    {
        var endpoint = new Endpoint<SerializableObject>("api/json");
        var ability = actor.GetAbility<MakeWebApiRequests>();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        Mock.Get(ability.DefaultClient)
            .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = JsonContent.Create(expectedPayload) }));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        var result = await When(actor).AttemptsTo(GetTheJsonResult(endpoint));

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.SameAs(expectedPayload), "Result and expected payload are not the same object (it has been through a serialization roundtrip)");
            Assert.That(result, Is.EqualTo(expectedPayload), "Result is equal to expected payload");
        });
    }

    [Test,AutoMoqData]
    public async Task GetTheJsonResultWithParametersShouldGetAnActionWhichYieldsTheExpectedPayload([Frozen] SerializableObject expectedPayload,
                                                                                                   [SendsMockHttpRequests] Actor actor,
                                                                                                   string parameters)
    {
        var endpoint = new JsonEndpoint<string, SerializableObject>("api/json");
        var ability = actor.GetAbility<MakeWebApiRequests>();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        Mock.Get(ability.DefaultClient)
            .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = JsonContent.Create(expectedPayload) }));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        var result = await When(actor).AttemptsTo(GetTheJsonResult(endpoint, parameters));

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.SameAs(expectedPayload), "Result and expected payload are not the same object (it has been through a serialization roundtrip)");
            Assert.That(result, Is.EqualTo(expectedPayload), "Result is equal to expected payload");
        });
    }
}

#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
public class IntegerParameterizedEndpoint(string relativeUri, HttpMethod? method = null) : ParameterizedEndpoint<int>(relativeUri, method)
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
{
    public override HttpRequestMessageBuilder GetHttpRequestMessageBuilder(int parameters)
    {
        return new HttpRequestMessageBuilder
        {
            RequestUri = new Uri(relativeUri, UriKind.Relative),
            Method = method ?? HttpMethod.Get,
            Content = new StringContent(parameters.ToString()),
        };
    }
}

#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
public class IntegerGenericParameterizedEndpoint(string relativeUri, HttpMethod? method = null) : ParameterizedEndpoint<int,string>(relativeUri, method)
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
{
    public override HttpRequestMessageBuilder<string> GetHttpRequestMessageBuilder(int parameters)
    {
        return new HttpRequestMessageBuilder<string>
        {
            RequestUri = new Uri(relativeUri, UriKind.Relative),
            Method = method ?? HttpMethod.Get,
            Content = new StringContent(parameters.ToString()),
        };
    }
}

