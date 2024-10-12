using CSF.Screenplay.Performances;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay;

[TestFixture,Parallelizable]
public class ScreenplayExtensionsTests
{
    [Test,AutoMoqData]
    public void ExecuteAsPerformanceShouldExecuteTheLogic([DefaultScreenplay] Screenplay sut)
    {
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
    public void ExecuteAsPerformanceShouldInvokeBeginPerformanceOnThePerformable([DefaultScreenplay] Screenplay sut)
    {
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
    public void ExecuteAsPerformanceShouldInvokeFinishPerformanceOnThePerformable([DefaultScreenplay] Screenplay sut)
    {
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
    public void ExecuteAsPerformanceShouldThrowIfTheTaskTakesTooLong([DefaultScreenplay] Screenplay sut)
    {
        bool? PerformanceLogic(IServiceProvider services)
        {
            Task.Delay(1000).Wait();
            return true;
        }
        
        Assert.That(() => sut.ExecuteAsPerformance(PerformanceLogic, timeoutMiliseconds: 50), Throws.InstanceOf<OperationCanceledException>());
    }

    [Test,AutoMoqData]
    public void ExecuteAsPerformanceGenericShouldExecuteThePerformanceHostLogic()
    {
        var sut = Screenplay.Create(s => s.AddSingleton<SamplePerformanceHost>(), o => o.ReportPath = null);

        sut.ExecuteAsPerformanceAsync<SamplePerformanceHost>();

        Assert.That(sut.ServiceProvider.GetRequiredService<SamplePerformanceHost>().HasExecuted, Is.True);
    }

    public class SamplePerformanceHost : IHostsPerformance
    {
        public bool HasExecuted { get; set; }
        
        
        public Task<bool?> ExecutePerformanceAsync(CancellationToken cancellationToken)
        {
            HasExecuted = true;
            return Task.FromResult<bool?>(true);
        }
    }
}
