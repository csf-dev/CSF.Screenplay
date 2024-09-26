using System.Net.Http;
using System.Reflection;

namespace CSF.Screenplay.WebApis;

[TestFixture,Parallelizable]
public class MakeWebApiRequestsTests
{
    [Test,AutoMoqData]
    public void DisposeShouldDisposeEachClient([SendsMockHttpRequests] Actor actor)
    {
        var sut = actor.GetAbility<MakeWebApiRequests>();
        var client1 = sut.DefaultClient = new HttpClient();
        var client2 = new HttpClient();
        sut["Foo"] = client2;

        actor.Dispose();

        Assert.Multiple(() =>
        {
            Assert.That(IsDisposed(client1), Is.True, "Client 1 disposed");
            Assert.That(IsDisposed(client2), Is.True, "Client 2 disposed");
        });
    }

    static bool IsDisposed(HttpClient client)
    {
        if (client is null)
            throw new ArgumentNullException(nameof(client));
        // Note that the following line of code could break if Microsoft change their impl of HttpClient.
        var field = typeof(HttpClient).GetField("_disposed", BindingFlags.Instance | BindingFlags.NonPublic);
        return (bool) field!.GetValue(client)!;
    }
}