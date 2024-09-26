using System.Net.Http;
using System.Reflection;

namespace CSF.Screenplay.WebApis;

[TestFixture,Parallelizable]
public class MakeWebApiRequestsTests
{
    // Note that the following assignment could fail and break the test if Microsoft change their impl of HttpClient.
    static readonly FieldInfo disposedField = typeof(HttpClient).GetField("_disposed", BindingFlags.Instance | BindingFlags.NonPublic)!;

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
        ArgumentNullException.ThrowIfNull(client);
        return (bool) disposedField.GetValue(client)!;
    }
}