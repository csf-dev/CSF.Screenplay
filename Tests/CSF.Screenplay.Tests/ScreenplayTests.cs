using CSF.Screenplay.Performances;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay;

[TestFixture, Parallelizable]
public class ScreenplayTests
{
    [Test, AutoMoqData]
    public async Task ExecuteAsPerformanceAsyncShouldExecuteTheLogic([DefaultScreenplay] Screenplay sut)
    {
        bool logicExecuted = false;
        Task<bool?> PerformanceLogic(IServiceProvider services, CancellationToken token)
        {
            logicExecuted = true;
            return Task.FromResult<bool?>(true);
        }
        await sut.ExecuteAsPerformanceAsync(PerformanceLogic);
        Assert.That(logicExecuted, Is.True);
    }

    [Test, AutoMoqData]
    public async Task ExecuteAsPerformanceAsyncShouldInvokeBeginPerformanceOnThePerformable([DefaultScreenplay] Screenplay sut)
    {
        bool eventReceived = false;
        void OnPerformanceBegun(object? sender, PerformanceEventArgs ev) => eventReceived = true;
        var eventBus = sut.ServiceProvider.GetRequiredService<PerformanceEventBus>();
        Task<bool?> PerformanceLogic(IServiceProvider services, CancellationToken token) => Task.FromResult<bool?>(true);

        eventBus.PerformanceBegun += OnPerformanceBegun;
        await sut.ExecuteAsPerformanceAsync(PerformanceLogic);
        eventBus.PerformanceBegun -= OnPerformanceBegun;

        Assert.That(eventReceived, Is.True);
    }

    [Test, AutoMoqData]
    public async Task ExecuteAsPerformanceAsyncShouldInvokeFinishPerformanceOnThePerformable([DefaultScreenplay] Screenplay sut)
    {
        bool eventReceived = false;
        void OnFinishPerformance(object? sender, PerformanceFinishedEventArgs ev) => eventReceived = true;
        var eventBus = sut.ServiceProvider.GetRequiredService<PerformanceEventBus>();
        Task<bool?> PerformanceLogic(IServiceProvider services, CancellationToken token) => Task.FromResult<bool?>(true);

        eventBus.PerformanceFinished += OnFinishPerformance;
        await sut.ExecuteAsPerformanceAsync(PerformanceLogic);
        eventBus.PerformanceFinished -= OnFinishPerformance;

        Assert.That(eventReceived, Is.True);
    }
}