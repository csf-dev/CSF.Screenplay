using CSF.Screenplay.Performances;

namespace CSF.Screenplay;

[TestFixture, Parallelizable]
public class InjectableParametersTests
{
    [Test, Screenplay]
    public void TheCastShouldBeInjectable(ICast cast)
    {
        var tess = cast.GetActor("Tess");
        var patricia = cast.GetActor("Patricia");

        Assert.That(() => cast.GetCastList(), Is.EquivalentTo(new [] {"Tess", "Patricia"}));
    }

    [Test, Screenplay]
    public void TheStageShouldBeInjectable(IStage stage, ICast cast)
    {
        var tess = cast.GetActor("Tess");
        var patricia = cast.GetActor("Patricia");

        stage.Spotlight(tess);

        Assert.That(() => stage.GetSpotlitActor(), Is.SameAs(tess));
    }

    [Test, Screenplay]
    public void ThePerformanceShouldBeInjectable(IPerformance performance)
    {
        Assert.That(() => performance.PerformanceState, Is.EqualTo(PerformanceState.InProgress));
    }

    [Test, Screenplay]
    public void TheCastShouldBeAnAdapter(ICast sut)
    {
        Assert.That(sut, Is.InstanceOf<CastAdapter>());
    }

    [Test, Screenplay]
    public void TheStageShouldBeAnAdapter(IStage sut)
    {
        Assert.That(sut, Is.InstanceOf<StageAdapter>());
    }

    [Test, Screenplay]
    public void ThePerformanceShouldBeAnAdapter(IPerformance sut)
    {
        Assert.That(sut, Is.InstanceOf<PerformanceAdapter>());
    }
}