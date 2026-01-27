using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Actions;

namespace CSF.Screenplay.Selenium.Builders;

[TestFixture, Parallelizable]
public class UnnamedWaitBuilderTests
{
    [Test, AutoMoqData]
    public void GetPerformableShouldGetAnObjectWithTheCorrectConfiguration()
    {
        var sut = new UnnamedWaitBuilder(wd => true);
        sut
            .Named("My wait")
            .ForAtMost(TimeSpan.FromSeconds(10))
            .WithPollingInterval(TimeSpan.FromMilliseconds(500))
            .IgnoringTheseExceptionTypes(typeof(InvalidOperationException), typeof(ArgumentException));

        var performable = (Wait) ((IGetsPerformable) sut).GetPerformable();

        Assert.Multiple(() =>
        {
            Assert.That(performable, Has.PrivateFieldEqualTo("timeout", TimeSpan.FromSeconds(10)));
            Assert.That(performable, Has.PrivateFieldEqualTo("pollingInterval", TimeSpan.FromMilliseconds(500)));
            Assert.That(performable, Has.PrivateFieldEqualTo("ignoredExceptionTypes", new [] { typeof(InvalidOperationException), typeof(ArgumentException) }));
        });
    }
}