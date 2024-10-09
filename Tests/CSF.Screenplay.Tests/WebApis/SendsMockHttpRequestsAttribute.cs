using System.Net;
using System.Net.Http;
using System.Reflection;
using AutoFixture;

namespace CSF.Screenplay.WebApis;

public class SendsMockHttpRequestsAttribute : CustomizeAttribute
{
    public override ICustomization GetCustomization(ParameterInfo parameter) => new SendsMockHttpRequestsCustomization();
}

public class SendsMockHttpRequestsCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<Actor>(c => c.Do(actor =>
        {
            var client = new Mock<HttpClient>();
            client
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));
            var makeWebApiRequests = new MakeWebApiRequests
            {
                DefaultClient = client.Object,
            };
            actor.IsAbleTo(makeWebApiRequests);
        }));
    }
}

public record SerializableObject(string StringProperty, int NumericProperty);