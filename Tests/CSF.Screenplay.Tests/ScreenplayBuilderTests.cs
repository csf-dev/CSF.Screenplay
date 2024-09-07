using CSF.Screenplay.Performances;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay;

[TestFixture,Parallelizable]
public class ScreenplayBuilderTests
{
    [Test,AutoMoqData]
    public void BuildScreenplayShouldReturnAScreenplay()
    {
        var screenplay = new ScreenplayBuilder().BuildScreenplay();
        Assert.That(screenplay, Is.Not.Null);
    }

    [Test,AutoMoqData]
    public void WithCastShouldSubstituteTheCastImplementation(ICast mockedCast)
    {
        var screenplay = new ScreenplayBuilder()
            .WithCast(_ => mockedCast)
            .BuildScreenplay();

        using var scope = screenplay.ServiceProvider.CreateScope();
        Assert.That(scope.ServiceProvider.GetRequiredService<ICast>(), Is.SameAs(mockedCast));
    }

    [Test,AutoMoqData]
    public void WithStageShouldSubstituteTheStageImplementation(IStage mockedStage)
    {
        var screenplay = new ScreenplayBuilder()
            .WithStage(_ => mockedStage)
            .BuildScreenplay();

        using var scope = screenplay.ServiceProvider.CreateScope();
        Assert.That(scope.ServiceProvider.GetRequiredService<IStage>(), Is.SameAs(mockedStage));
    }

    [Test,AutoMoqData]
    public void WithPerformanceFactoryShouldSubstituteThePerformanceFactoryImplementation(ICreatesPerformance mockedFactory)
    {
        var screenplay = new ScreenplayBuilder()
            .WithPerformanceFactory(_ => mockedFactory)
            .BuildScreenplay();

        Assert.That(screenplay.ServiceProvider.GetRequiredService<ICreatesPerformance>(), Is.SameAs(mockedFactory));
    }
}
