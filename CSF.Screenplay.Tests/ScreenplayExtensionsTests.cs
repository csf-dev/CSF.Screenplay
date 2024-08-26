using CSF.Screenplay.Performances;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay;

[TestFixture,Parallelizable]
public class ScreenplayExtensionsTests
{
    [Test,AutoMoqData]
    public void ExecuteAsPerformanceShouldExecuteTheLogic()
    {
        var sut = new Screenplay();
        bool logicExecuted = false;
        bool? PerformanceLogic(IServiceProvider services)
        {
            logicExecuted = true;
            return true;
        }
        sut.ExecuteAsPerformance(PerformanceLogic);
        Assert.That(logicExecuted, Is.True);
    }

    [Test,AutoMoqData]
    public void ExecuteAsPerformanceShouldInvokeBeginPerformanceOnThePerformable()
    {
        var sut = new Screenplay();
        bool eventReceived = false;
        void OnPerformanceBegun(object? sender, PerformanceEventArgs ev) => eventReceived = true;
        var eventBus = sut.ServiceProvider.GetRequiredService<PerformanceEventBus>();
        bool? PerformanceLogic(IServiceProvider services) => true;

        eventBus.PerformanceBegun += OnPerformanceBegun;
        sut.ExecuteAsPerformance(PerformanceLogic);
        eventBus.PerformanceBegun -= OnPerformanceBegun;

        Assert.That(eventReceived, Is.True);
    }

    [Test,AutoMoqData]
    public void ExecuteAsPerformanceShouldInvokeFinishPerformanceOnThePerformable()
    {
        var sut = new Screenplay();
        bool eventReceived = false;
        void OnFinishPerformance(object? sender, PerformanceFinishedEventArgs ev) => eventReceived = true;
        var eventBus = sut.ServiceProvider.GetRequiredService<PerformanceEventBus>();
        bool? PerformanceLogic(IServiceProvider services) => true;

        eventBus.PerformanceFinished += OnFinishPerformance;
        sut.ExecuteAsPerformance(PerformanceLogic);
        eventBus.PerformanceFinished -= OnFinishPerformance;

        Assert.That(eventReceived, Is.True);
    }

    [Test,AutoMoqData]
    public void ExecuteAsPerformanceShouldThrowIfTheTaskTakesTooLong()
    {
        var sut = new Screenplay();
        bool? PerformanceLogic(IServiceProvider services)
        {
            Thread.Sleep(100);
            return true;
        }
        
        Assert.That(() => sut.ExecuteAsPerformance(PerformanceLogic, timeoutMiliseconds: 50), Throws.InstanceOf<OperationCanceledException>());
    }
}