using System.Collections.Generic;
using CSF.Screenplay.Performances;

namespace CSF.Screenplay;

[TestFixture,Parallelizable]
public class PerformanceTests
{
    [Test,AutoMoqData]
    public void BeginPerformanceShouldInvokePerformanceBegunOnTheEventBus([Frozen] IRelaysPerformanceEvents performanceEventBus,
                                                                          [AutofixtureServices] IServiceProvider services)
    {
        var sut = new Performance(services, Array.Empty<IdentifierAndName>());
        sut.BeginPerformance();
        Mock.Get(performanceEventBus)
            .Verify(x => x.InvokePerformanceBegun(sut), Times.Once);
    }

    [Test,AutoMoqData]
    public void BeginPerformanceShouldThrowIfExecutedTwice([AutofixtureServices] IServiceProvider services)
    {
        var sut = new Performance(services, Array.Empty<IdentifierAndName>());
        sut.BeginPerformance();
        Assert.That(() => sut.BeginPerformance(), Throws.InvalidOperationException);
    }

    [Test,AutoMoqData]
    public void FinishPerformanceShouldThrowIfExecutedBeforeBeginPerformance([AutofixtureServices] IServiceProvider services, bool? success)
    {
        var sut = new Performance(services, Array.Empty<IdentifierAndName>());
        Assert.That(() => sut.FinishPerformance(success), Throws.InvalidOperationException);
    }

    [Test,AutoMoqData]
    public void FinishPerformanceShouldInvokePerformanceFinishedOnTheEventBus([Frozen] IRelaysPerformanceEvents performanceEventBus,
                                                                              [AutofixtureServices] IServiceProvider services,
                                                                              bool? success)
    {
        var sut = new Performance(services, Array.Empty<IdentifierAndName>());
        sut.BeginPerformance();
        sut.FinishPerformance(success);
        Mock.Get(performanceEventBus)
            .Verify(x => x.InvokePerformanceFinished(sut, success), Times.Once);
    }

    [Test,AutoMoqData]
    public void FinishPerformanceShouldThrowIfExecutedTwice([AutofixtureServices] IServiceProvider services, bool? success)
    {
        var sut = new Performance(services, Array.Empty<IdentifierAndName>());
        sut.BeginPerformance();
        sut.FinishPerformance(success);
        Assert.That(() => sut.FinishPerformance(success), Throws.InvalidOperationException);
    }

    [Test,AutoMoqData]
    public void PerformanceStateShouldReturnNotStartedBeforeThePerformanceBegins([AutofixtureServices] IServiceProvider services)
    {
        var sut = new Performance(services, Array.Empty<IdentifierAndName>());
        Assert.That(sut.PerformanceState, Is.EqualTo(PerformanceState.NotStarted));
    }

    [Test,AutoMoqData]
    public void PerformanceStateShouldReturnInProgressBeforeThePerformanceFinishes([AutofixtureServices] IServiceProvider services)
    {
        var sut = new Performance(services, Array.Empty<IdentifierAndName>());
        sut.BeginPerformance();
        Assert.That(sut.PerformanceState, Is.EqualTo(PerformanceState.InProgress));
    }

    [Test,AutoMoqData]
    public void PerformanceStateShouldReturnSuccessIfThePerformanceFinishesWithSuccess([AutofixtureServices] IServiceProvider services)
    {
        var sut = new Performance(services, Array.Empty<IdentifierAndName>());
        sut.BeginPerformance();
        sut.FinishPerformance(true);
        Assert.That(sut.PerformanceState, Is.EqualTo(PerformanceState.Success));
    }

    [Test,AutoMoqData]
    public void PerformanceStateShouldReturnFailedIfThePerformanceFinishesWithFailure([AutofixtureServices] IServiceProvider services)
    {
        var sut = new Performance(services, Array.Empty<IdentifierAndName>());
        sut.BeginPerformance();
        sut.FinishPerformance(false);
        Assert.That(sut.PerformanceState, Is.EqualTo(PerformanceState.Failed));
    }

    [Test,AutoMoqData]
    public void PerformanceStateShouldReturnCompletedIfThePerformanceFinishesWithNoResult([AutofixtureServices] IServiceProvider services)
    {
        var sut = new Performance(services, Array.Empty<IdentifierAndName>());
        sut.BeginPerformance();
        sut.FinishPerformance(null);
        Assert.That(sut.PerformanceState, Is.EqualTo(PerformanceState.Completed));
    }

    [Test,AutoMoqData]
    public void DisposeShouldUnsubscribeFromAllActors([Frozen] IRelaysPerformanceEvents eventBus,
                                                      [AutofixtureServices, Frozen] IServiceProvider services,
                                                      Performance sut)
    {
        sut.Dispose();
        Mock.Get(eventBus).Verify(x => x.UnsubscribeFromAllActors(sut.PerformanceIdentity), Times.Once);
    }
}